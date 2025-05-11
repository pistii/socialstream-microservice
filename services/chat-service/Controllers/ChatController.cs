using chat_service.Helpers;
using shared_libraries.DTOs;
using shared_libraries.Interfaces;
using shared_libraries.Models;
using chat_service.Repository;
using chat_service.Storage;
using Microsoft.AspNetCore.Mvc;
using Serilog;
using shared_libraries.Controllers;

namespace chat_service.Controllers
{
    //[Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class ChatController : BaseController, IChatController
    {
        private readonly IChatRepository _chatRepository;
        private readonly IStorageRepository _storageRepository;
        private readonly IChatStorage _chatStorage;

        public ChatController(
          IChatRepository chatRepository,
          IStorageRepository storageController,
          IChatStorage chatStorage)
        {
            _chatRepository = chatRepository;
            _storageRepository = storageController;
            _chatStorage = chatStorage;
        }

        [HttpGet("room/{id}")]
        public async Task<IActionResult> GetChatRoom(int id)
        {
            var room = await _chatRepository.GetByIdAsync<ChatRoom>(id);
            if (room != null) { return Ok(room); }
            return BadRequest();
        }


        //[HttpGet("conversation/{receiverPrivateId}")]
        //public async Task<IActionResult> GetConversationByUserId(int receiverPrivateId, string receivePublicId)
        //{

        //    var senderId = GetUserId();
        //    var room = await _chatRepository.GetChatRoomByUser(senderId, receiverPrivateId);

        //    if (room == null) return NoContent();

        //    //Map the original chatContent object to ChatContentDto. This way the ChatFile will contain the audio object.
        //    var sortedChatContents = _chatRepository
        //        .GetSortedChatContent(room.chatRoomId)
        //        .Select(c => new ChatContentDto(senderUser.PublicId, c.AuthorId == senderId, c))
        //        .ToList();


        //    var totalMessages = sortedChatContents.Count;
        //    var totalPages = (int)Math.Ceiling((double)totalMessages / 20);

        //    var returnValue = _chatRepository.Paginator(sortedChatContents);
        //    returnValue.Reverse();

        //    //Check if any of the chatContent has a file
        //    bool hasFile = sortedChatContents.Any(x => x.ChatFile != null);
        //    if (hasFile)
        //    {
        //        //TODO: Should return file
        //        //returnValue = await _chatRepository.GetChatFile(returnValue);
        //    }
        //    List<string> participants = new() { senderUser.PublicId, receivePublicId };
        //    return Ok(new ContentDto<ChatContentDto>(returnValue, participants, totalPages, 1, room.chatRoomId));
        //}

        

        [HttpGet("{roomid}/{currentPage}")]
        [HttpGet("{roomid}")]
        public async Task<IActionResult> GetChatContent(
        int roomid,
        int messagesPerPage = 20,
        int currentPage = 1)
        {
            var room = await _chatRepository.GetChatRoomById(roomid);

            if (room == null) return NotFound();

            var currentUserId = GetUserId();
            var senderUser = await _chatRepository.GetByIdAsync<User>(room.senderId);
            if (senderUser == null) return BadRequest();
            //Map the original chatContent object to ChatContentDto. This way the ChatFile will contain the audio object.
            var content = _chatRepository.GetSortedChatContent(roomid).Select(c => new ChatContentDto(senderUser.publicId, c.AuthorId == currentUserId, c)).Reverse().ToList();

            var totalMessages = content.Count;
            var totalPages = (int)Math.Ceiling((double)totalMessages / messagesPerPage);

            var returnValue = _chatRepository.Paginator<ChatContentDto>(content, currentPage, messagesPerPage).ToList();

            //Check if any of the chatContent has a file
            bool hasFile = content.Any(x => x.ChatFile != null);
            if (hasFile)
            {
                //Collect all the tokens
                IEnumerable<string> fileTokens = returnValue
                .Where(c => c.ChatFile != null && c.ChatFile.FileToken != null)
                .Select(c => c.ChatFile!.FileToken);

                try
                {
                    foreach (var token in fileTokens)
                    {
                        var file = await _storageRepository.GetFileAsByte(token);

                        var contentWithFile = returnValue.Find(x => x.ChatFile != null && x.ChatFile.FileToken == token);
                        if (contentWithFile != null)
                        {
                            returnValue.First(x => x.chatRoomId == contentWithFile.chatRoomId)
                            .ChatFile!.FileData = file;
                        }

                    }
                }
                catch (Exception ex)
                {
                    Log.Error("Error while downloading file: " + ex);
                }
            }

            List<string> participants = new() { senderUser.publicId, "HARDCODED PUBLIC ID" };
            return Ok(new ChatRoomWithContentDto<ChatContentDto>(returnValue, participants, totalPages, currentPage, roomid));
        }


        [HttpPost("newChat")]
        public async Task<IActionResult> SendMessage([FromBody] ChatDto chatDto)
        {
            return await Send(chatDto);
        }

        [HttpPost("file")]
        public async Task<IActionResult> SendMessageWithFile([FromForm] ChatDto chatDto)
        {
            return await Send(chatDto);
        }

        public async Task<IActionResult> Send(ChatDto chatDto)
        {
            //var senderId =(int)GetUserId();
            var senderId = 1;
            ChatRoom? room = await _chatRepository.ChatRoomExists(senderId, chatDto.receiverId);
            if (room == null)
            {
                room = await _chatRepository.CreateChatRoom(senderId, chatDto.receiverId, chatDto.receiverPublicId);
            }

            var chatContent = new ChatContent()
            {
                message = chatDto.message,
                status = chatDto.status,
                //ReceiverPublicId = chatDto.receiverPublicId,
                AuthorId = senderId,
                chatContentId = room.chatRoomId,
            };

            room.endedDateTime = DateTime.Now;
            room.ChatContents.Add(chatContent);
            await _chatRepository.SaveAsync();

            var toUserId = chatDto.receiverId;


            if (chatDto.chatFile != null && chatDto.chatFile.File != null)
            {
                var fileObj = chatDto.chatFile;
                if (FileHandlerService.FormatIsValid(fileObj.Type!) &&
                FileHandlerService.FileSizeCorrect(fileObj.File, fileObj.Type!))
                {
                    FileUploadDto fileUpload = new FileUploadDto(fileObj.Name ?? "", fileObj.Type!, fileObj.File);
                    var savedName = await _storageRepository.AddFile(fileUpload);
                    if (savedName != null) //Upload file only if corresponds to the requirements.
                    {
                        ChatFile chatFile = new()
                        {
                            FileToken = savedName,
                            ChatContentId = chatContent.MessageId,
                            FileType = chatDto.chatFile.Type!,
                            FileSize = (int)chatDto.chatFile.File.Length,
                        };

                        await _chatRepository.InsertSaveAsync<ChatFile>(chatFile);
                        return Ok(chatContent);
                    }
                }

                return BadRequest("File size exceeded or format not accepted.");
            }
            
            return Ok(chatContent);
        }

        [HttpPut("/update")]
        public async Task<IActionResult> UpdateMessage(int messageId, int updateToUser, string msg)
        {

            //var user = await _chatRepository.GetByIdAsync<User>(updateToUser);
            var message = await _chatRepository.GetByIdAsync<ChatContent>(messageId);

            if (message == null)
            {
                return BadRequest("Unable to find message or user, maybe it's deleted?");
            }
            message.status = Status.Read;
            message.message = msg;
            await _chatRepository.UpdateThenSaveAsync(message);

            return NoContent();
        }

    }
}
