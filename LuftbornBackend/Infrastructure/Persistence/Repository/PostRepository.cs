using Application.Interfaces.Repository;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Persistence.Repository
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        private readonly BlogDbContext _context;
        private readonly DbSet<Post> _dbSet;

        public PostRepository(BlogDbContext context) : base(context)
        {
            _context = context;
            _dbSet = context.Set<Post>();
        }

        public async Task<Post> GetByTitleAsync(string title)
        {
            return await _dbSet.FirstOrDefaultAsync(p => p.Title == title) ?? throw new Exception("Post not found");
        }

        public async Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(Guid authorId)
        {
            return await _dbSet.Where(p => p.AuthorId == authorId).ToListAsync();
        }
    }
}
