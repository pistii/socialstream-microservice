using Microsoft.AspNetCore.Mvc;
using shared_libraries.DTOs;

namespace shared_libraries.Controllers
{
    public interface IChatController
    {
        Task<IActionResult> GetChatContent(int roomid, int messagesPerPage = 20, int currentPage = 1);
        Task<IActionResult> Send(ChatDto chatDto);
        Task<IActionResult> UpdateMessage(int messageId, int updateToUser, string msg);
    }
}
