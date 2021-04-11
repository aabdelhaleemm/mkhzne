using System.Threading;
using System.Threading.Tasks;
using Application.Store.Command.Add;
using Application.Store.Queries.GetAll;
using Application.Store.Queries.GetById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace mkhzne.Controllers
{
    [Authorize]
    public class StoreController : BaseController
    {
        private readonly IMediator _mediator;

        public StoreController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetStoreById(int id, CancellationToken cancellationToken)
        {
            var storeDto = await _mediator.Send(new GetStoreByIdQuery(id), cancellationToken);
            if (storeDto == null) return NotFound();

            return Ok(storeDto);
        }

        [HttpPost("")]
        public async Task<IActionResult> AddStore(AddStoreCommand command, CancellationToken cancellationToken)
        {
            var store = await _mediator.Send(command, cancellationToken);
            if (!store) return BadRequest();

            return Ok();
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllUserStores(CancellationToken cancellationToken)
        {
            var userStores = await _mediator.Send(new GetAllStoresQuery(), cancellationToken);
            return Ok(userStores);
        }
    }
}