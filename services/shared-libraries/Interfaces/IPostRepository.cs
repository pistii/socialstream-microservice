using shared_libraries.DTOs;
using shared_libraries.Models;
using shared_libraries.Models.Cloud;

namespace shared_libraries.Interfaces
{
    public interface IPostRepository : IGenericRepository
    {
        Task UploadFile(FileUpload mediaContent, Post postdto);
        Task<PersonalPost> GetPostByTokenAsync(string token);
        Task<Post> GetPostWithReactionsByTokenAsync(string token);
        Task<List<PostDto>> GetImagesAsync(int userId, string publicId);
        Task RemovePostAsync(Post post);
        Task<Post> Create(CreatePostDto postDto, Personal author, int postedToUserId);
        Task<bool> UserCanCreatePost(int postAuthorId, int postedToUserId);
        Task<ContentDto<PostDto>> GetAllPost(int profileId, string publicUserId, int currentPage = 1, int itemPerRequest = 10);
        Task LikePost(int postId, int userId, ReactionType reactionType);
        Task DislikePost(int postId, int userId, ReactionType reactionType);
    }
}
