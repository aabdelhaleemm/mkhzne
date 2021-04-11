using System.Threading;
using System.Threading.Tasks;
using Application.User.Command.Add;
using Application.User.Command.SignIn;
using MediatR;
using Microsoft.AspNetCore.Mvc;
namespace mkhzne.Controllers
{
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddUser(AddUserCommand command, CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }

        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(SignInCommand command,CancellationToken cancellationToken)
        {
            var token = await _mediator.Send(command, cancellationToken);
            return Ok(token);
        }
    }
}