using Branchly.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace Branchly.Web.Controllers
{
    public class UsersController : Controller
    {
        private readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateUserCommand command)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            Guid userId = await _mediator.Send(command);
            return Ok(userId);
        }

    }
}
