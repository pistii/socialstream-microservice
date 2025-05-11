using Microsoft.Extensions.Logging;
using shared_libraries.Kafka.IServiceClient;
using shared_libraries.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_libraries.Kafka.Handlers
{
    public class FriendHandler : IKafkaConsumerHandler<Friend>
    {
        private readonly IUserServiceClient _userServiceClient;
        private readonly ILogger<FriendHandler> _logger;

        public FriendHandler(IUserServiceClient userServiceClient, ILogger<FriendHandler> logger)
        {
            _userServiceClient = userServiceClient;
            _logger = logger;
        }

        public async Task HandleAsync(Friend message, CancellationToken cancellationToken)
        {
            var user = await _userServiceClient.GetUserByIdAsync(message.UserId, cancellationToken);
            if (user != null)
            {
                Console.WriteLine($"User found: {user.userId}");
            }
            else
            {
                _logger.LogWarning("User not found.");
            }
        }
    }

}
