
namespace shared_libraries.Interfaces
{
    public interface IGenericRepository
    {
        List<T> Paginator<T>(List<T> sortable, int currentPage = 1, int messagePerPage = 20) where T : class;
        Task<T?> GetByIdAsync<T>(int id) where T : class;
        Task SaveAsync();
        Task InsertSaveAsync<T>(T entity) where T : class;
        void Remove<T>(T entity) where T : class;
        Task RemoveThenSaveAsync<T>(T entity) where T : class;
        Task UpdateThenSaveAsync<T>(T entity) where T : class;
        Task InsertAsync<T1>(T1 entity) where T1 : class;

    }
}
