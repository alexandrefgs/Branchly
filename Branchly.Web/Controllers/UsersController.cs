using Branchly.Application.Commands;
using Branchly.Domain.Abstractions;
using Branchly.Domain.Users.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Branchly.Web.Controllers
{
    [Route("Users")]
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UsersController(IMediator mediator, IUserRepository userRepository, IUnitOfWork unitOfWork)
        {
            _mediator = mediator;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            Guid userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpPut("Edit")]
        public async Task<IActionResult> Edit([FromBody] EditUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest("Dados inválidos.");

            Guid userId = await _mediator.Send(command);
            return Ok(userId);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                return NotFound("Usuário não encontrado.");

            await _userRepository.RemoveAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return Ok(new { Message = "Usuário deletado com sucesso!", Id = id });
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepository.GetAllAsync();

            var result = users.Select(u => new
            {
                u.Id,
                Username = u.Username.Value
            });

            return Json(result);
        }
    }
}
