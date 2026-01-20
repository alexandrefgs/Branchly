using Branchly.Domain.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Branchly.Domain.Users.Repositories
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<IEnumerable<User>> GetAllAsync();
        Task AddAsync(User user);
        Task RemoveAsync(User user);
    }
}
