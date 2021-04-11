using System.Threading;
using System.Threading.Tasks;
using Application.Category.Queries.GetOwnerIdByCategoryId;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.ValueObjects;
using MediatR;

namespace Application.Product.Command.Add
{
    public class AddProductCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public double Price { get; set; }
        public int Count { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
    }

    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMediator _mediator;

        public AddProductCommandHandler(IApplicationDbContext applicationDbContext, IMediator mediator,
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _mediator = mediator;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var storeOwnerId =
                await _mediator.Send(new GetOwnerIdByCategoryIdQuery(request.CategoryId), cancellationToken);
            if (storeOwnerId == 0)
                throw new NotFoundException("Cant find the category");
            if (storeOwnerId != _currentUserService.UserId)
                throw new UnAuthorizedRequest("You are not authorized to add this product for this store");

            await _applicationDbContext.Products.AddAsync(new Domain.Entities.Product
            {
                CategoryId = request.CategoryId,
                Name = request.Name,
                Price = request.Price,
                Count = Stock.From(request.Count),
                Color = request.Color
            }, cancellationToken);

            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("Something went wrong please try again later ");
        }
    }
}