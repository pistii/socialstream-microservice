using shared_libraries.Models;

namespace shared_libraries.Interfaces
{
    public interface IPersonalRepository : IGenericRepository
    {
        IQueryable<Personal> FilterPersons(int userId);
    }
}
