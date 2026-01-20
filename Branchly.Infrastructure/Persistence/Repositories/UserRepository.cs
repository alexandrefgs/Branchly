using Branchly.Domain.Users;
using Branchly.Domain.Users.Repositories;
using Branchly.Domain.Users.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Branchly.Infrastructure.Persistence.Repositories;

public sealed class UserRepository : IUserRepository
{
    private readonly BranchlyDbContext _context;

    public UserRepository(BranchlyDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(UserId id, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Id == id.Value, cancellationToken);
    }

    public async Task<User?> GetByUsernameAsync(string username, CancellationToken cancellationToken = default)
    {
        return await _context.Users
            .FirstOrDefaultAsync(u => u.Username.Value == username.ToLower(), cancellationToken);
    }

    public async Task AddAsync(User user, CancellationToken cancellationToken = default)
    {
        await _context.Users.AddAsync(user, cancellationToken);
    }
}
