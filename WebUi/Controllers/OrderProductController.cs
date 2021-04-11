using System.Threading;
using System.Threading.Tasks;
using Application.OrderProduct.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mkhzne.Controllers
{
    [Authorize]
    public class OrderProductController : BaseController
    {
        private readonly IMediator _mediator;

        public OrderProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddOrderProduct(AddOrderProductCommand command , CancellationToken cancellationToken)
        {
            await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}