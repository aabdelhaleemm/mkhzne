using System.Threading;
using System.Threading.Tasks;
using Application.Order.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mkhzne.Controllers
{
    [Authorize]
    public class OrderController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("")]
        public async Task<IActionResult> AddOrder(AddOrderCommand command, CancellationToken cancellationToken)
        {
            var order = await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}