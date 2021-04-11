using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Extensions;
using Application.Common.Interfaces;
using Domain.Entities;
using MediatR;

namespace Application.Order.Command.Add
{
    public class AddOrderCommand : IRequest<bool>
    {
        public string City { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string Notes { get; set; }
        public int StatusId { get; set; }
        public int StoreId { get; set; }

    }
    public class AddOrderCommandHandler : IRequestHandler<AddOrderCommand,bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICheckOwnerService _checkOwnerService;

        public AddOrderCommandHandler(IApplicationDbContext applicationDbContext, ICheckOwnerService checkOwnerService)
        {
            _applicationDbContext = applicationDbContext;
            _checkOwnerService = checkOwnerService;
        }
        public async Task<bool> Handle(AddOrderCommand request, CancellationToken cancellationToken)
        {
            var store =await _applicationDbContext.Stores.FindAsync(request.StoreId);
            _checkOwnerService.CheckOwner(store);
            
            await _applicationDbContext.Orders.AddAsync(new Domain.Entities.Order()
            {
                City = request.City,
                Address = request.Address,
                CustomerPhoneNumber = request.CustomerPhoneNumber,
                CustomerName = request.CustomerName,
                Notes = request.Notes,
                StatusId = request.StatusId,
                StoreId = request.StoreId
            }, cancellationToken);

            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("something went wrong try again later");

        }
    }
}