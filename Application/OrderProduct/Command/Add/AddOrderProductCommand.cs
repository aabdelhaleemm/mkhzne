using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.OrderProduct.Command.Add
{
    public class AddOrderProductCommand : IRequest<bool>
    {
        public IList<OrderProducts> OrderProductsList { get; set; }

        
    }
    public class AddOrderProductCommandHandler : IRequestHandler<AddOrderProductCommand,bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public AddOrderProductCommandHandler(IApplicationDbContext applicationDbContext , ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }
        public async Task<bool> Handle(AddOrderProductCommand request, CancellationToken cancellationToken)
        {
            var order = await _applicationDbContext.Orders
                .Select(x => new {x.Store.OwnerId , x.Id})
                .FirstOrDefaultAsync(cancellationToken: cancellationToken);
            if (order.Id == 0)
                throw new NotFoundException("Order not found!");
            if (order.OwnerId != _currentUserService.UserId)
                throw new UnAuthorizedRequest("you cannot access this store!");

            await _applicationDbContext.OrderProducts.AddRangeAsync(request.OrderProductsList, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("something went wrong please try again later!");
        }
    }
}
