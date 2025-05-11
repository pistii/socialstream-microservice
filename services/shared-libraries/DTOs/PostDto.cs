using shared_libraries.Models;
using shared_libraries.Models.Cloud;

namespace shared_libraries.DTOs
{
    public class PostDto
    {
        public PostDto()
        {
            
        }
        public PostDto(Personal author, string postedToUser, Post post, int? commentsQty = 0)
        {
            Post = post;
            PostAuthor = new PostAuthor(author.avatar!, author.firstName, author.middleName!, author.lastName, author.User!.publicId);
            IsAuthor = author.User!.publicId == postedToUser;
            PostedToUserId = postedToUser;
            CommentsQty = commentsQty;
        }

        public Post Post { get; set; } = null!;
        public PostAuthor PostAuthor { get; set; } = null!;
        public bool IsAuthor { get; set; }
        public string PostedToUserId { get; set; } = null!;
        public int? CommentsQty { get; set; }
    }


    public class PostAuthor
    {
        public PostAuthor()
        {
            
        }
        public PostAuthor(string avatar, string firstName, string middleName, string lastName, string authorId)
        {
            Avatar = avatar;
            FirstName = firstName;
            MiddleName = middleName;
            LastName = lastName;
            AuthorId = authorId;
        }

        public string? Avatar { get; set; }
        public string FirstName { get; set; } = null!;
        public string? MiddleName { get; set; }
        public string LastName { get; set; } = null!;
        public string AuthorId { get; set; } = null!;

    }
    public class CreatePostDto
    {
        public CreatePostDto()
        {
            
        }
        public CreatePostDto(string postedToUserId, string message, FileUpload file)
        {
            Message = message;
            PostedToUserId = postedToUserId;
            FileUpload = file;
        }

        public FileUpload? FileUpload { get; set; }
        public string? Message { get; set; }
        public string PostedToUserId { get; set; } = null!;
    }

    public class UpdatePostDto
    {
        public UpdatePostDto()
        {

        }
        public UpdatePostDto(string token, string message, FileUpload file)
        {
            Message = message;
            FileUpload = file;
        }
        public string Token { get; set; } = null!;
        public FileUpload? FileUpload { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
