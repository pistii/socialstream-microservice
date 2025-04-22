using shared_libraries.Interfaces;

namespace notification_service.Repository
{
    public class GenericRepository : IGenericRepository
    {

        private readonly NotificationDBContext _context;
        public GenericRepository(NotificationDBContext context)
        {
            _context = context;
        }

        public List<T1> Paginator<T1>(List<T1> sortable, int currentPage = 1, int messagePerPage = 20) where T1 : class
        {
            var result = sortable
             .Skip((currentPage - 1) * messagePerPage)
             .Take(messagePerPage).ToList();
            return result;
        }

        public static Task<int> GetTotalPages<T>(List<T> items, int itemPerRequest) where T : class
        {
            var totalItems = items.Count;
            var totalPages = (int)Math.Ceiling((double)totalItems / itemPerRequest);
            return Task.FromResult(totalPages);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public async Task InsertAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
        }

        public async Task InsertSaveAsync<T>(T entity) where T : class
        {
            await _context.Set<T>().AddAsync(entity);
            await SaveAsync();
        }

        public async Task<T?> GetByIdAsync<T>(int id) where T : class
        {
            return await _context.Set<T>().FindAsync(id);
        }


        public void Update<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
        }

        public async Task UpdateThenSaveAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Update(entity);
            await SaveAsync();
        }

        public void Remove<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
        }

        public async Task RemoveThenSaveAsync<T>(T entity) where T : class
        {
            _context.Set<T>().Remove(entity);
            await SaveAsync();
        }

        public async Task<bool> ExistsAsync<T1>(T1 entity) where T1 : class
        {
            var exists = await _context.Set<T1>().FindAsync(entity);
            return exists != null;
        }
    }
}
