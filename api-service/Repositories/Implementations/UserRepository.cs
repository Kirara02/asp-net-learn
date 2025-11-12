using ApiService.Data;
using ApiService.Models.Entities;
using ApiService.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ApiService.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;

        public UserRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _db.Users.FindAsync(id);
        }

        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users
                .AsNoTracking()
                .OrderBy(u => u.Id)
                .ToListAsync();
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public void Update(User user)
        {
            _db.Users.Update(user);
        }

        public void Delete(User user)
        {
            _db.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }
    }
}
