using Branchly.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Branchly.Infrastructure.Persistence;

public sealed class BranchlyDbContext : DbContext
{
    public BranchlyDbContext(DbContextOptions<BranchlyDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users => Set<User>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(BranchlyDbContext).Assembly);
    }
}
