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
            PlaceOfResidence = personal.PlaceOfResidence;
            avatar = personal.avatar;
            DateOfBirth = personal.DateOfBirth;
            Profession = personal.Profession;
            Workplace = personal.Workplace;
            PublicId = personal.User!.PublicId;
        }
        public int Score { get; set; }
        public string PublicId { get; set; }
    }

    public class RecommendedPosts : PostDto, IScoreRatio
    {
        public int Score { get; set; }
    }

    public interface IScoreRatio { int Score { get; set; } }



}