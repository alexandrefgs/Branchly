using Branchly.Domain.Abstractions;

namespace Branchly.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly BranchlyDbContext _context;

    public UnitOfWork(BranchlyDbContext context)
    {
        _context = context;
    }

    public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return _context.SaveChangesAsync(cancellationToken);
    }
}
