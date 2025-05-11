using shared_libraries.DTOs;
using System.Text.Json;

namespace shared_libraries.Kafka.IServiceClient
{
    public interface IUserServiceClient
    {
        Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken);
    }

    public class UserServiceClient : IUserServiceClient
    {
        private readonly HttpClient _httpClient;

        public UserServiceClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<UserDto?> GetUserByIdAsync(int userId, CancellationToken cancellationToken)
        {
            Console.WriteLine("Get user with id from userService: " + userId);
            var response = await _httpClient.GetAsync($"api/user/getUser/{userId}", cancellationToken);
            Console.WriteLine("-----------------");
            Console.WriteLine("Response received from controller:");
            Console.WriteLine(response);
            if (!response.IsSuccessStatusCode)
                return null;

            var json = await response.Content.ReadAsStringAsync(cancellationToken);
            return JsonSerializer.Deserialize<UserDto>(json);
        }
    }

}
