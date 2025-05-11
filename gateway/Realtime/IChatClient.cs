using shared_libraries.Models.Cloud;
using shared_libraries.Models;

namespace gateway.Realtime
{
    public interface IChatClient
    {
        Task ReceiveMessage(int fromUserId, int toUserId, object obj, FileUpload fileUpload = null);
        Task SendStatusInfo(int messageId, int userId);
        Task GetOnlineUsers(int userId);
        Task ReceiveOnlineFriends(List<PersonalIsOnlineDto> onlineFriends);
    }
}
