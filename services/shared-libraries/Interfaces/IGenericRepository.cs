
using shared_libraries.Interfaces.Shared;
using System.Linq.Expressions;

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
        Task<T?> GetByPublicIdAsync<T>(string publicId) where T : class, IHasPublicId;
        Task<T> GetEntityByPredicateFirstOrDefaultAsync<T>(Expression<Func<T, bool>> predicate) where T : class;
        Task<T> GetWithIncludeAsync<T, TProperty>(Expression<Func<T, TProperty>> includeExpression, Expression<Func<T, bool>> predicate) where T : class;
        Task<List<T1>> GetWithIncludeAsync<T1, TProperty>(Expression<Func<T1, TProperty>> includeExpression) where T1 : class;
    }
}
