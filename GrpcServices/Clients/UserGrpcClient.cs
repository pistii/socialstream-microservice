using GrpcServices.Interfaces;

namespace GrpcServices.Clients
{
    public class UserGrpcClient : IUserGrpcClient
    {
        private readonly User.UserClient _userClient;

        public UserGrpcClient(User.UserClient client)
        {
            _userClient = client;
        }

        public async Task<GetAllUserResponse> GetAllUserByIdAsync(GetAllUserRequest request)
        {
            return await _userClient.GetAllUserByIdAsync(request);
        }

        public async Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserIdRequestAsync(RepeatedChatPartnerIdsRequest request)
        {
            return await _userClient.GetMessagePartnersByUserIdAsync(request);
        }

        public async Task<UserResponse> GetUserAsync(UserRequest request)
        {
            return await _userClient.GetUserAsync(request);
        }

        public async Task<UserResponse> GetUserByIdRequestAsync(UserRequestByPrivateId request)
        {
            return await _userClient.GetUserByIdRequestAsync(request);
        }
    }
}
