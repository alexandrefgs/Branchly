using MediatR;
using System;

namespace Branchly.Application.Commands
{
    public class EditUserCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
        public string Username { get; set; }
    }
}
