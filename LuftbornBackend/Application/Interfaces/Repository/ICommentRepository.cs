using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface ICommentRepository : IRepository<Comment>
    {
        Task<IEnumerable<Comment>> GetCommentsByPostIdAsync(Guid postId);
    }
}
