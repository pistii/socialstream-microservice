using Confluent.Kafka;
using gateway.Realtime.Connection;
using Microsoft.EntityFrameworkCore;
using shared_libraries.Auth;
using shared_libraries.Interfaces;
using shared_libraries.Models.Cloud;
using shared_libraries.Models;
using static GrpcServices.Protos.Chat;

namespace gateway.Realtime
{
    public class ChatHub : ConnectionHandler<IChatClient>
    {
        private readonly ConnectionHandler<IChatClient> _connectionHandler;
        private readonly IMapConnections _connections;
        private readonly IFriendRepository _friendRepo;

        public ChatHub(IJwtUtils utils, IMapConnections mapConnections, IFriendRepository friendRepository)
        : base(utils, mapConnections) // Öröklés a szülőosztályból, meg kell hívni a konstruktorát
        {
            _connectionHandler = this;
            _connections = mapConnections;
            _friendRepo = friendRepository;
        }

        public async Task ReceiveMessage(int fromId, int userId, string message, FileUpload? fileUpload)
        {
            await Clients.User(userId.ToString()).ReceiveMessage(fromId, userId, message, fileUpload);
        }

        public async Task SendMessage(int fromId, int userId, string message)
        {
            foreach (var user in _connections.GetConnectionsById(userId))
            {
                await Clients.Client(user).ReceiveMessage(fromId, userId, message);
            }
        }

        public async Task SendStatusInfo(int messageId, int userId, int status)
        {
            foreach (var user in _connections.GetConnectionsById(userId))
            {
                await Clients.Client(user).SendStatusInfo(messageId, status);
            }
        }


        public async Task ReceiveOnlineFriends(string userId)
        {
            var userIdd = int.Parse(Context.UserIdentifier);
            List<PersonalIsOnlineDto> onlineFriends = new List<PersonalIsOnlineDto>();
            var friends = await _friendRepo.GetAllFriendAsync(userIdd);

            foreach (var friend in friends)
            {
                if (_connections.ContainsUser(friend.id))
                {
                    //Ha engedélyezte az online státuszt                    
                    if (friend.User.isOnlineEnabled)
                    {
                        PersonalIsOnlineDto dto = new PersonalIsOnlineDto(friend, friend.User.isOnlineEnabled);
                        onlineFriends.Add(dto);
                    }
                }
            }
            foreach (var user in _connections.GetConnectionsById(userIdd))
            {
                await _connectionHandler.Clients.Client(user).ReceiveOnlineFriends(onlineFriends);
            }
        }
    }
}
