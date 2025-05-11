using shared_libraries.Interfaces.Shared;
using shared_libraries.Models;

namespace shared_libraries.DTOs
{
    public class SearchResultDto
    {
        public SearchResultDto(List<RecommendedPerson> personal, List<RecommendedPosts> post)
        {
            Persons = personal;
            Posts = post;
        }

        public List<RecommendedPerson> Persons { get; set; }
        public List<RecommendedPosts> Posts { get; set; }
    }

    public class RecommendedPerson : Personal, IScoreRatio, IHasPublicId
    {
        public RecommendedPerson(Personal personal)
        {
            firstName = personal.firstName;
            middleName = personal.middleName;
            lastName = personal.lastName;
            isMale = personal.isMale;
            placeOfResidence = personal.placeOfResidence;
            avatar = personal.avatar;
            dateOfBirth = personal.dateOfBirth;
            profession = personal.profession;
            workplace = personal.workplace;
            publicId = personal.User!.publicId;
        }
        public int Score { get; set; }
        public string publicId { get; set; }
    }

    public class RecommendedPosts : PostDto, IScoreRatio
    {
        public int Score { get; set; }
    }

    public interface IScoreRatio { int Score { get; set; } }



}