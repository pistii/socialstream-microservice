
using shared_libraries.DTOs;
using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface INavigationRepository
    {
        public Task<List<RecommendedPerson>> SearchForPerson(string searchByValue, int page, int itemPerRequest);
        public Task<List<RecommendedPosts>> SearchForPost(int currentUser, string searchByValue, int page, int itemPerRequest);
    }
}