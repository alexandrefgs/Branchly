using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Branchly.Domain.Abstractions;
using Branchly.Domain.Users.ValueObjects;

namespace Branchly.Domain.Users;

public sealed class User : AggregateRoot
{
    public Username Username { get; private set; } = default!;

    private User() { }

    private User(UserId id, Username username)
        : base(id.Value)
    {
        Username = username;
    }

    public static User Create(Username username)
    {
        return new User(UserId.New(), username);
    }
}

