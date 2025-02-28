using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Infrastructure.Persistence.Repository
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<User> _dbSet;

        public UserRepository(BlogDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<User>();
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Email == email) ?? throw new Exception("User not found");
        }

        public async Task<User?> FindByUsername(string username)
        {
            return await _dbSet.FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
