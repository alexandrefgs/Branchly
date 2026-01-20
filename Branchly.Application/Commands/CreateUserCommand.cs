using MediatR;

namespace Branchly.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; }

        public CreateUserCommand(string username)
        {
            Username = username;
        }
    }
}
