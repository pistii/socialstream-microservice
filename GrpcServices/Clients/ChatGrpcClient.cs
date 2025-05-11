using GrpcServices.Interfaces;
using GrpcServices.Protos;

namespace GrpcServices.Clients
{
    public class ChatGrpcClient : IChatGrpcClient
    {
        private readonly Chat.ChatClient _chatClient;

        public ChatGrpcClient(Chat.ChatClient client)
        {
            _chatClient = client;
        }


        public async Task<RepeatedChatRoomDtoResponse> GetAllChatRoomAsQueryRequestAsync(GetUserChatRoomsRequest request)
        {
            return await _chatClient.GetAllChatRoomAsQueryRequestAsync(request);
        }

        public async Task<ChatRoomResponse> GetChatRoomByIdAsync(GetChatRoom request)
        {
            return await _chatClient.GetChatRoomByIdAsync(request);
        }

        public async Task<ChatContentResponse> SendMessageRequestAsync(CreateChatContentRequest request)
        {
            return await _chatClient.SendMessageRequestAsync(request);
        }

        public async Task<ChatContentResponse> UpdateChatContentAsync(UpdateChatContentRequest request)
        {
            return await _chatClient.UpdateChatContentAsync(request);
        }
    }
}
