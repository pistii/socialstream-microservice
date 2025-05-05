using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using GrpcServices;
using GrpcServices.Mappers;
using shared_libraries.DTOs;
using shared_libraries.Interfaces;
using shared_libraries.Models;
using dbModel = shared_libraries.Models;

namespace GrpcServices.Services
{
    public class UserService : User.UserBase
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async override Task<GetAllUserResponse> GetAllUserById(GetAllUserRequest request, ServerCallContext context)
        {
            var userIds = new List<int>();
            userIds.AddRange(request.UserIds);

            var users = await _userRepository.GetAllUserWithId(userIds);
            if (users.Any())
            {
                var response = new GetAllUserResponse()
                {
                    Success = true
                };
                response.Users.AddRange(users.Select(
                    u => ObjectMapper.Map<dbModel.User, UserResponse>(u)
                ));
                return response;
            }
            
            return new GetAllUserResponse() { Success = false };
        }


        public async override Task<UserResponse> GetUser (UserRequest request, ServerCallContext context)
        {
           
            var response = await _userRepository.GetByPublicId(request.PublicId);

            if (response != null)
            {
                try
                {
                    var mappedData = ObjectMapper.Map<dbModel.User, UserResponse>(response);
                    mappedData.Success = true;
                    return mappedData;
                }
                catch (Exception)
                {
                    throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Failed to map data to UserResponse"));
                }

            }
            throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Failed to receive user."));
        }

        public async override Task<UserResponse> GetUserByIdRequest(UserRequestByPrivateId request, ServerCallContext context) 
        {
            var response = await _userRepository.GetByIdAsync<dbModel.User>(request.UserId);
            if (response != null)
            {
                try
                {
                    Console.WriteLine("try to map data...");
                    var mappedData = new UserResponse() {
                        UserId = response.userID
                    };
                    //response.PublicId = user.PublicId ?? "";
                    //response.Email = user.Email ?? "";
                    //response.SecondaryEmailAddress =
                    //    string.IsNullOrEmpty(user.SecondaryEmailAddress)
                    //    ? null
                    //    : new StringValue { Value = user.SecondaryEmailAddress }; // <-- itt figyelj

                    //response.RegistrationDate = user.RegistrationDate != default
                    //    ? Timestamp.FromDateTime(user.RegistrationDate.ToUniversalTime())
                    //    : null;

                    //response.LastOnline = user.LastOnline != default
                    //    ? Timestamp.FromDateTime(user.LastOnline.ToUniversalTime())
                    //    : null;

                    //response.IsActivated = user.IsActivated;
                    //response.IsOnlineEnabled = user.IsOnlineEnabled;
                    //response.Success = true;

                    //var mappedData = ObjectMapper.Map<dbModel.User, UserResponse>(response);
                    Console.WriteLine("Mapped data: " + mappedData);

                    mappedData.Success = true;
                    return mappedData;
                }
                catch (Exception)
                {
                    throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Failed to map data to UserResponse"));
                }

            }
            throw new RpcException(new Grpc.Core.Status(StatusCode.Internal, "Failed to receive user."));
        }

        public async override Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserId(RepeatedChatPartnerIdsRequest request, ServerCallContext context)
        {
            var userIds = new List<string>();
            userIds.AddRange(request.ChatPartnerIds);
            Console.WriteLine("requesting users..." + userIds);
            var db_response = await _userRepository.GetMessagePartnersById(userIds, request.UserId);
            return ObjectMapper.Map<List<Personal>, RepeatedChatPartnersResponse>(db_response.ToList());
        }
    }
}
