using System.Threading;
using System.Threading.Tasks;
using Application.Product.Command.Add;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mkhzne.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("")]
        public async Task<IActionResult> AddProduct(AddProductCommand command , CancellationToken cancellationToken)
        {
            var product = await _mediator.Send(command, cancellationToken);
            return Ok();
        }
    }
}