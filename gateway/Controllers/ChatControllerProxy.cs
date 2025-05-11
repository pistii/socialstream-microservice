using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServices;
using GrpcServices.Interfaces;
using GrpcServices.Mappers;
using GrpcServices.Protos;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.DTOs;
using shared_libraries.Helpers;
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace gateway.Controllers
{
    [ApiController]
    [Route("api/chat")]
    public class ChatControllerProxy : BaseController
    {
        private readonly IUserGrpcClient _userGrpcClient;
        private readonly IChatGrpcClient _chatGrpcClient;
        public ChatControllerProxy(IUserGrpcClient userGrpcClient, IChatGrpcClient chatGrpcClient)
        {
            _userGrpcClient = userGrpcClient;
            _chatGrpcClient = chatGrpcClient;
        }

        [HttpGet("rooms")]
        [HttpGet("rooms/{searchKey}")]
        public async Task<IActionResult> GetAllChatRoom(string? searchKey = null)
        {
            var userId = 1; //GetUserId();
            var currentUser = await _userGrpcClient.GetUserByIdRequestAsync(
                new GrpcServices.UserRequestByPrivateId()
                { UserId = userId  });

            var publicUserId = currentUser.PublicId;

            var grpcResult = await _chatGrpcClient.GetAllChatRoomAsQueryRequestAsync(
                new GetUserChatRoomsRequest() 
                { PublicUserId = publicUserId, UserId = userId });
            Console.WriteLine("grpcResult result: " + grpcResult);

            if (!grpcResult.Found) return NotFound("No chatrooms found");

            var result = grpcResult.ChatRoom.Select(x => new ChatRoomDto
            {
                chatRoomId = x.ChatRoomId,
                endedDateTime = x.EndedDateTime.ToDateTime(),
                receiverId = x.ReceiverId,
                senderId = x.SenderId,
                startedDateTime = x.StartedDateTime.ToDateTime(),
                ChatContents = x.ChatContents
                .Select(cc => ObjectMapper.Map<ChatContentDtoResponse, ChatContentDto>(cc))
                .ToList()
            });

            // if (searchKey != null)
            // {
            //     var filtered = query.Where(item => item.ChatContents.Any(i => i.message != null  && i.message.ToLower().Contains(searchKey)));
            //     query = filtered;
            // }

            var listed = result.ToList();

            var partnerIds = result
                .SelectMany(room => new[] { room.senderId, room.receiverId })
                .Where(id => id != publicUserId)
                .Distinct()
                .ToList();

            var request = new RepeatedChatPartnerIdsRequest();
            request.ChatPartnerIds.AddRange(partnerIds);
            var messagePartners = await _userGrpcClient.GetMessagePartnersByUserIdRequestAsync(request);
            var mappedData = ObjectMapper.Map<RepeatedChatPartnersResponse, List<Personal>>(messagePartners);
            Console.WriteLine("chatpartners found: " + messagePartners);
            Console.WriteLine("chatpartner types: " + messagePartners.GetType());


            var chatRooms = new List<KeyValuePair<ChatRoomDto, UserDetailsDto>>();

            foreach (var room in result)
            {
                var authorId = room.senderId == publicUserId ? room.receiverId : room.senderId;

                var messagePartner = mappedData.First(person => person.User!.publicId == authorId);

                if (messagePartner != null)
                {
                    var chatroom = result.First(_ => _.chatRoomId == room.chatRoomId);
                    UserDetailsDto dto = new UserDetailsDto(messagePartner);
                    chatRooms.Add(new KeyValuePair<ChatRoomDto, UserDetailsDto>(chatroom, dto));
                }
            }

            return Ok(chatRooms);
        }


        [HttpGet("{roomid}")]
        [HttpGet("{roomid}/{currentPage}")]
        public async Task<IActionResult> GetChatContent(
        int roomid,
        int messagesPerPage = 20,
        int currentPage = 1)
        {
            var request = new GetChatRoom() { RoomId = roomid };
            ChatRoomResponse? room = null;
            try
            {
                Console.WriteLine("--------------------GetChatContent-------------");
                Console.WriteLine("sending GetChatRoomByIdAsync request....");
                room = await _chatGrpcClient.GetChatRoomByIdAsync(request);
                Console.WriteLine("response tipusa: " + room.GetType());
                Console.WriteLine("room response: " + room);
            } catch (RpcException ex)
            {
                if (ex.StatusCode == Grpc.Core.StatusCode.Cancelled)
                {
                    return BadRequest(ex.Message);
                }
            }

            if (room == null) return NotFound();

            var userId = 1; //GetUserId();
            Console.WriteLine("sending GetUserByIdRequestAsync request....");
            var senderUser = await _userGrpcClient.GetUserByIdRequestAsync(
                new GrpcServices.UserRequestByPrivateId() 
                { UserId = userId });
            Console.WriteLine("response tipusa: " + senderUser.GetType());
            Console.WriteLine("senderUser response: " + senderUser);


            if (senderUser == null) return BadRequest();
            return Ok();
            //Map the original chatContent object to ChatContentDto. This way the ChatFile will contain the audio object.
            //var content = _chatRepository.GetSortedChatContent(roomid).Select(c => new ChatContentDto(senderUser.PublicId, c.AuthorId == userId, c)).Reverse().ToList();

            //var totalMessages = content.Count;
            //var totalPages = (int)Math.Ceiling((double)totalMessages / messagesPerPage);

            //var returnValue = _chatRepository.Paginator<ChatContentDto>(content, currentPage, messagesPerPage).ToList();

            ////Check if any of the chatContent has a file
            //bool hasFile = content.Any(x => x.ChatFile != null);
            //if (hasFile)
            //{
            //    //Collect all the tokens
            //    //IEnumerable<string> fileTokens = returnValue
            //    //.Where(c => c.ChatFile != null && c.ChatFile.FileToken != null)
            //    //.Select(c => c.ChatFile!.FileToken);

            //    //try
            //    //{
            //    //    foreach (var token in fileTokens)
            //    //    {
            //    //        var file = await _storageRepository.GetFileAsByte(token, BucketSelector.CHAT_BUCKET_NAME);

            //    //        var contentWithFile = returnValue.Find(x => x.ChatFile != null && x.ChatFile.FileToken == token);
            //    //        if (contentWithFile != null)
            //    //        {
            //    //            returnValue.First(x => x.chatRoomId == contentWithFile.chatRoomId)
            //    //            .ChatFile!.FileData = file;
            //    //        }

            //    //    }
            //    //}
            //    //catch (Exception ex)
            //    //{
            //    //    Log.Error("Error while downloading file: " + ex);
            //    //}
            //}

            //List<string> participants = new() { senderUser.PublicId, senderUser.PublicId };
            //return Ok(new ChatRoomWithContentDto<ChatContentDto>(returnValue, participants, totalPages, currentPage, roomid));
        }


        [HttpPost("newChat")]
        public async Task<IActionResult> SendMessage(ChatDto chatDto)
        {
            int userId = 1; //GetUserId();

            Console.WriteLine("--------------------SendMessage-------------");
            Console.WriteLine("sending SendMessageRequestAsync request....");
            //Chat service request
            var chatResponse = await _chatGrpcClient.SendMessageRequestAsync(new GrpcServices.Protos.CreateChatContentRequest()
            {
                Message = chatDto.message,
                ReceiverId = chatDto.receiverId,
                ReceiverPublicId = chatDto.receiverPublicId,
                SenderId = userId
            });

            var senderUser = await _userGrpcClient.GetUserByIdRequestAsync(new UserRequestByPrivateId() { UserId = userId });
            var receiverUser = await _userGrpcClient.GetUserAsync(new UserRequest() { PublicId = chatDto.receiverPublicId });

            Console.WriteLine("response tipusa: " + chatResponse.GetType());
            Console.WriteLine("response response: " + chatResponse);
            if (chatResponse.Success)
            {
                //var dto = new ChatContentDto(sender.PublicId, chatContent.AuthorId == senderId, chatContent);
                //var dtoToReceiver = new ChatContentDto(sender.PublicId, false, chatContent);
            }
            return Ok(chatResponse);
        }

        [HttpPut("update")]
        public async Task<IActionResult> UpdateMessage(UpdateMessageDto updateMessageDto)
        {
            Console.WriteLine("--------------------UpdateMessage-------------");
            Console.WriteLine("sending SendMessageRequestAsync request....");
            var userRequest = new GrpcServices.UserRequest() { PublicId = updateMessageDto.updateToUser };
            var user = await _userGrpcClient.GetUserAsync(userRequest);
            Console.WriteLine("user tipusa: " + user.GetType());
            Console.WriteLine("user response: " + user);

            var chatContentRequest = new UpdateChatContentRequest() { 
                PublicId = updateMessageDto.messageId,
                Message = updateMessageDto.msg,
                AuthorPublicId = updateMessageDto.updateToUser
            };
            Console.WriteLine("sending UpdateChatContentAsync request....");

            var isUpdated = await _chatGrpcClient.UpdateChatContentAsync(chatContentRequest);
            
            if (isUpdated.Success)
                return NoContent();
            return BadRequest("Failed to update message");
        }
    }
}
