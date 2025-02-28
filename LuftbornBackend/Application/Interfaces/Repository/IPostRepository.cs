using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface IPostRepository : IRepository<Post>
    {
        Task<IEnumerable<Post>> GetPostsByAuthorIdAsync(Guid authorId);
    }
}
