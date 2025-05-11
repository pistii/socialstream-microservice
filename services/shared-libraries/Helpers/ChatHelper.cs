using shared_libraries.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace shared_libraries.Helpers
{
    public static class ChatHelper
    {
        public static List<string> GetMessagePartnersById(List<ChatRoomDto> all, string userId)
        {
            var partnerIds = all
                .SelectMany(room => new[] { room.senderId, room.receiverId })
                .Where(id => id != userId)
                .Distinct()
                .ToList();

            return partnerIds;
        }
    }
}
