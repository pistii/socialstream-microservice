using Grpc.Core;
using GrpcServices;

namespace GrpcServices.Services
{
    public class UserService : User.UserBase
    {
        //private readonly IUserRepository _userRepository;
        public UserService()//IUserRepository notificationRepository)
        {
            //_userRepository = notificationRepository;
        }

        public override Task<UserResponse> GetUser (UserRequest request, ServerCallContext context)
        {
            Console.WriteLine($"New User request in grpc: {request.PublicId}");
            return Task.FromResult(new UserResponse { Success = true });
        }

    }
}
