
using Domain.Entities;

namespace Application.Interfaces.Repository
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User?> FindByUsername(string username);
    }
}
