using MediatR;

namespace Branchly.Application.Commands
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public string Username { get; set; }
        public string Email { get; set; }

        public CreateUserCommand(string username, string email)
        {
            Username = username;
            Email = email;
        }
    }
}
