using Microsoft.EntityFrameworkCore;
using shared_libraries.DTOs;
using shared_libraries.Interfaces;
using shared_libraries.Models;
using shared_libraries.Repositories;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace user_service.Repositories
{
    public class UserRepository : GenericRepository<UserDbContext>, IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) : base(context)
        {
            Console.WriteLine("UserDbContext instance userrepoban: " + context.GetHashCode());

            _context = context;
            Console.WriteLine("users: ");
            foreach (var item in _context.User.ToList())
            {
                Console.WriteLine(item);
            }

            Console.WriteLine("personals: ");
            foreach (var item in _context.Personal.ToList())
            {
                Console.WriteLine(item);
            }
        }


        public async Task<List<User>> GetAllUserTest()
        {
            return await _context.User.ToListAsync();
        }


        public async Task<List<Personal>> GetAllPersonalTest()
        {
            return await _context.Personal.ToListAsync();
        }

        public async Task<User> GetUserByIdForKafka(int userId)
        {
            var user = await _context.User.FirstOrDefaultAsync(p => p.userId == userId);
            return user;
        }

        public async Task<List<Personal>> GetAllPersonalWithId(List<int> userIds)
        {
            var users = await _context.Personal
                .Include(p => p.User)
                .Where(u => userIds.Contains(u.id))
                .ToListAsync();
            return users;
        }
        public async Task<List<User>> GetAllUserWithId(List<int> userIds)
        {
            var users = await _context.User
                .Include(p => p.personal)
                .Where(u => userIds.Contains(u.userId))
                .ToListAsync();
            //Console.WriteLine("DEBUG: Users found in GetAllUserWithId user repository test.");
            //users.ForEach(user => Console.WriteLine(JsonSerializer.Serialize(users, new JsonSerializerOptions() { WriteIndented = true,
            //    ReferenceHandler = ReferenceHandler.Preserve
            //})));
            return users;
        }

        public Task<bool> CanUserRequestMoreActivatorToday(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User?> GetByPublicId(string publicId)
        {
            var users = await _context.User.Include(p => p.personal).Where(p => p.publicId != "").ToListAsync();
            Console.WriteLine("------- all user result: ");
            foreach (var item in users)
            {
                Console.WriteLine(item);
            }

            var user = await _context.User.Include(p => p.personal).FirstOrDefaultAsync(p => p.publicId == publicId);
            return user;
        }

        public async Task<IEnumerable<Personal>> GetMessagePartnersById(List<string> partnerIds, string userId)
        {
            var result = await _context.Personal
                .Include(u => u.User)
                .Where(person => partnerIds.Contains(person.User!.publicId))
                .ToListAsync();
            return result;
        }

        public Task<Personal?> GetPersonalWithSettingsAndUserAsync(string userId)
        {
            throw new NotImplementedException();
        }

        public Task<Personal?> GetPersonalWithSettingsAndUserAsync(int userId)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailAsync(string email, bool withPersonal = true)
        {
            throw new NotImplementedException();
        }

        public Task<User?> GetUserByEmailOrPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task SendActivationEmail(string email, User user)
        {
            throw new NotImplementedException();
        }
    }
}
