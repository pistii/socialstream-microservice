using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServices.Mappers;
using GrpcServices.Protos;
using Microsoft.AspNetCore.Mvc;
using shared_libraries.Controllers;
using shared_libraries.DTOs;
using shared_libraries.Interfaces;
using shared_libraries.Models;

namespace GrpcServices.Services
{
    public class ChatService : Chat.ChatBase
    {
        private readonly IChatRepository _chatRepository;
        private readonly IChatController _chatController;
        public ChatService(
            IChatRepository chatRepository, 
            IChatController chatController)
        {
            _chatRepository = chatRepository;
            _chatController = chatController;
        }

        //public override async Task<ChatRoomResponse> ChatRoomRequest(GetChatRoom request, ServerCallContext context)
        //{
        //    var chatrooms = await _chatRepository.GetChatRoomById(request.RoomId);

        //    var resp = new ChatRoomResponse()
        //    {
        //        ReceiverId = chatrooms.ReceiverPublicId,
        //        RoomId = chatrooms.chatRoomId,
        //        //SenderId = chatrooms,
        //        EndedDateTime = Timestamp.FromDateTime(chatrooms.endedDateTime),
        //        StartedDateTime = Timestamp.FromDateTime(chatrooms.startedDateTime),

        //    };

        //    resp.ChatContent.AddRange(new ChatContent() { AuthorId = c});
        //}
        
        public async override Task<ChatContentResponse?> SendMessageRequest(CreateChatContentRequest request, ServerCallContext context)
        {
            var controllerTask = _chatController.Send(
                new ChatDto()
                {
                    message = request.Message,
                    receiverId = request.ReceiverId,
                    receiverPublicId = request.ReceiverPublicId,
                    status = shared_libraries.Models.Status.Sent
                });

            var grpcResponse = await GrpcControllerHelper.HandleControllerResponse(controllerTask, async value =>
            {
                var controllerResult = await controllerTask;
                Console.WriteLine("Start of SendMessageRequest------------------------------------------------------------------------------------");
                Console.WriteLine("DEBUG : Controller result: ");
                Console.WriteLine(controllerResult);
                Console.WriteLine("controller type: " + controllerResult.GetType());

                if (controllerResult is ObjectResult objectResult)
                {
                    Console.WriteLine("DEBUG : Controller is okobject: ");
                    Console.WriteLine(objectResult.Value);

                    var dto = objectResult.Value as ChatContent;
                    if (dto is null) return null;

                    return new ChatContentResponse
                    {
                        AuthorId = dto.AuthorId,
                        IsAuthor = false,
                        Message = dto.message,
                        MessagePublicId = dto.publicId,
                        MessageId = dto.MessageId,
                        SentDate = Timestamp.FromDateTime(dto.sentDate.ToUniversalTime()),
                        Status = Protos.Status.Sent,
                    };
                }
                else
                {
                    return null;
                }
            });
            return grpcResponse.Result;            
        }

        public async override Task<ChatContentResponse?> UpdateChatContent(UpdateChatContentRequest request, ServerCallContext context)
        {
            var controllerTask = _chatController.UpdateMessage(
                request.PublicId,
                request.AuthorId,
                request.Message);

            var controllerResult = await controllerTask;
            if (controllerResult is NoContentResult)
            {
                return new ChatContentResponse()
                {
                    Success = true
                };
            }
            else {
                return new ChatContentResponse()
                {
                    Success = false
                };
            }
        }


        public async override Task<ChatRoomResponse?> GetChatRoomById(GetChatRoom request, ServerCallContext context)
        {
            var result = await _chatRepository.GetChatRoomById(request.RoomId);

            if (result != null)
            {
                var grpcRequest = ObjectMapper.Map<ChatRoom, ChatRoomResponse>(result);
                return grpcRequest;
            }
            else
            {
                return null;
                //throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Unexpected controller response or error."));
            }
        }

        public async override Task<RepeatedChatRoomDtoResponse> GetAllChatRoomAsQueryRequest(GetUserChatRoomsRequest request, ServerCallContext context)
        {
            Console.WriteLine("Request received in grpc.");
            var result = await _chatRepository.GetAllChatRoomAsQuery(request.PublicUserId, request.UserId);
            Console.WriteLine("request found from repo:" + result);
            Console.WriteLine("request type:" + result.GetType());


            if (result != null)
            {
                var grpcRequest = ObjectMapper.Map<List<shared_libraries.DTOs.ChatRoomDto>, RepeatedChatRoomDtoResponse>(result);
                grpcRequest.Found = result.Count > 0;
                grpcRequest.Success = true;
                return grpcRequest;
            }
            else
            {
                throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Unexpected controller response or error."));
            }
        }

    }
}
