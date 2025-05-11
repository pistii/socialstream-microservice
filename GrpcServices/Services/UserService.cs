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
                    u => ObjectMapper.ExcludeNullMapper<dbModel.User, UserResponse>(u)
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
                    Console.WriteLine("Mapping data for GetUser: " + response);
                    var mappedData = ObjectMapper.Map<dbModel.User, UserResponse>(response);
                    Console.WriteLine("Data mapped and ready to return: " + mappedData);
                    mappedData.Success = true;
                    mappedData.UserId = response.userId;
                    return mappedData;
                }
                catch (Exception)
                {
                    return new UserResponse() { Success = false };
                }

            }
            return new UserResponse() { Success = false };
        }

        public async override Task<UserResponse> GetUserByIdRequest(UserRequestByPrivateId request, ServerCallContext context) 
        {
            var response = await _userRepository.GetByIdAsync<dbModel.User>(request.UserId);
            if (response != null)
            {
                try
                {
                    var mappedData = ObjectMapper.Map<dbModel.User, UserResponse>(response);
                    mappedData.Personal = ObjectMapper.Map<dbModel.Personal, PersonalResponse>(response.personal);

                    Console.WriteLine("DEBUG: Mapped data result: " + mappedData);

                    mappedData.Success = true;
                    return mappedData;
                }
                catch (Exception)
                {
                    return new UserResponse() { Success = false };
                }

            }
            return new UserResponse() { Success = false };

        }

        public async override Task<RepeatedChatPartnersResponse> GetMessagePartnersByUserId(RepeatedChatPartnerIdsRequest request, ServerCallContext context)
        {
            var userIds = new List<string>();
            userIds.AddRange(request.ChatPartnerIds);
            Console.WriteLine("requesting users..." + userIds);
            var db_response = await _userRepository.GetMessagePartnersById(userIds, request.UserId);
            return ObjectMapper.Map<List<Personal>, RepeatedChatPartnersResponse>(db_response.ToList());
        }

        public async override Task<UserDetailsDtoResponse> GetUserByPublicId(UserRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetByPublicIdAsync<dbModel.User>(request.PublicId);
            Console.WriteLine("DEBUG: |GetUserByPublicId from userService| GetByPublicIdAsync user: " + user);

            if (user != null)
            {
                return new UserDetailsDtoResponse()
                {
                    Avatar = user.personal.avatar ?? "",
                    FirstName = user.personal.firstName,
                    MiddleName = user.personal.middleName ?? "",
                    LastName = user.personal.lastName,
                    PublicId = user.publicId,

                    UserFound = true,
                    Success = true
                };
            }
            return new UserDetailsDtoResponse()
            {
                Success = false,
                UserFound = false
            };
        }

        public async override Task<ReturnsUserPrivateId> GetUserPrivateIdByPublicId(UserRequest request, ServerCallContext context)
        {
            var user = await _userRepository.GetByPublicIdAsync<dbModel.User>(request.PublicId);
            if (user != null)
            {
                return new ReturnsUserPrivateId()
                {
                    UserId = user.userId,
                    UserFound = true
                };
            }
            else
            {
                return new ReturnsUserPrivateId() { UserFound = false };
            }
        }

        public async override Task<FoundUsersResponse> GetAllUserTest(UserRequest request, ServerCallContext context)
        {
            var foundUsers = await _userRepository.GetAllUserTest();
            Console.WriteLine($"DEBUG: Found users in GetAllUserTest: {foundUsers.Count} ");
            return new FoundUsersResponse() { FoundUsers = foundUsers.Count };
        }
    }
}
