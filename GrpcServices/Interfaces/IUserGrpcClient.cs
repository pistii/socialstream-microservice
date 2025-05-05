namespace GrpcServices.Interfaces
{
    public interface IUserGrpcClient
    {
        Task<UserResponse> GetUserAsync(UserRequest request);
        Task<UserResponse> GetUserByIdRequestAsync(UserRequestByPrivateId request);
        Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserIdRequestAsync(RepeatedChatPartnerIdsRequest request);
        Task<GetAllUserResponse> GetAllUserByIdAsync(GetAllUserRequest request);
    }
}
