using Branchly.Application.Commands;
using Branchly.Domain.Abstractions;
using Branchly.Domain.Users;
using Branchly.Domain.Users.Repositories;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Branchly.Application.Handlers
{
    public class EditUserCommandHandler : IRequestHandler<EditUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EditUserCommandHandler(IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Guid> Handle(EditUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id);
            if (user == null) throw new Exception("Usuário não encontrado");

            user.UpdateUsername(request.Username);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return user.Id;
        }
    }
}
