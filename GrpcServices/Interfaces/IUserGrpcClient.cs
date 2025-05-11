namespace GrpcServices.Interfaces
{
    public interface IUserGrpcClient
    {
        Task<UserResponse> GetUserAsync(UserRequest request);
        Task<UserResponse> GetUserByIdRequestAsync(UserRequestByPrivateId request);
        Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserIdRequestAsync(RepeatedChatPartnerIdsRequest request);
        Task<GetAllUserResponse> GetAllUserByIdAsync(GetAllUserRequest request);
        Task<UserDetailsDtoResponse> GetUserByPublicId(UserRequest request);
        Task<ReturnsUserPrivateId> GetUserPrivateIdByPublicId(UserRequest request);
        Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserId(RepeatedChatPartnerIdsRequest request);

        Task<FoundUsersResponse> GetAllUserTest(UserRequest request);
    }
}
