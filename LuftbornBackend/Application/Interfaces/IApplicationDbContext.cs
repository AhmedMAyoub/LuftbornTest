using Domain.Entities;
using Microsoft.EntityFrameworkCore;


namespace Application.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    DbSet<Post> Posts { get; set; }
    DbSet<Comment> Comments { get; set; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}