using GrpcServices.Protos;

namespace GrpcServices.Interfaces
{
    public interface IChatGrpcClient
    {
        Task<RepeatedChatRoomDtoResponse> GetAllChatRoomAsQueryRequestAsync(GetUserChatRoomsRequest request);
        Task<ChatRoomResponse> GetChatRoomByIdAsync(GetChatRoom request);
        Task<ChatContentResponse> SendMessageRequestAsync(CreateChatContentRequest request);
        Task<ChatContentResponse> UpdateChatContentAsync(UpdateChatContentRequest request);
    }
}
