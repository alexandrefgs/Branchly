using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Branchly.Domain.Abstractions;

namespace Branchly.Domain.Users.ValueObjects;

public sealed class Username : ValueObject
{
    public string Value { get; }

    public Username(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("Username é obrigatório.");

        if (value.Length < 3 || value.Length > 20)
            throw new ArgumentException("Username deve ter entre 3 e 20 caracteres.");

        Value = value.ToLowerInvariant();
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
