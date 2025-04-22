using shared_libraries.DTOs;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface ICommentRepository : IGenericRepository
    {
        public Task<CommentDto> Create(int postId, int authorId, string message);
        Task<Comment?> GetCommentByTokenAsync(string token);
        Task<ContentDto<CommentDto>> GetCommentsAsync(string userPublicId, int postId, int currentPage = 1, int itemPerPage = 20);
    }
}
