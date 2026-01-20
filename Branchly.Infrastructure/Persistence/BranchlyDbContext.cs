using Branchly.Domain.Users;
using Microsoft.EntityFrameworkCore;

namespace Branchly.Infrastructure.Persistence
{
    public class BranchlyDbContext : DbContext
    {
        public BranchlyDbContext(DbContextOptions<BranchlyDbContext> options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<User>(b =>
            {
                b.HasKey(u => u.Id);

                b.OwnsOne(u => u.Username, n =>
                {
                    n.Property(p => p.Value).HasColumnName("Username").IsRequired();
                });

            });
        }
    }
}
