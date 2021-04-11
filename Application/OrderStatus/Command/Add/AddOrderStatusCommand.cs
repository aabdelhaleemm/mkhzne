using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.OrderStatus.Command.Add
{
    public class AddOrderStatusCommand : IRequest<bool>
    {
        public string Status { get; set; }
        public int StoreId { get; set; }
    }

    public class AddOrderStatusCommandHandler : IRequestHandler<AddOrderStatusCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICheckOwnerService _checkOwnerService;
        private readonly ICurrentUserService _currentUserService;

        public AddOrderStatusCommandHandler(IApplicationDbContext applicationDbContext,
            ICheckOwnerService checkOwnerService)
        {
            _applicationDbContext = applicationDbContext;
            _checkOwnerService = checkOwnerService;
        }

        public async Task<bool> Handle(AddOrderStatusCommand request, CancellationToken cancellationToken)
        {
            var store = await _applicationDbContext.Stores.FindAsync(request.StoreId);
            _checkOwnerService.CheckOwner(store);

            await _applicationDbContext.OrderStatus.AddAsync(new Domain.Entities.OrderStatus()
            {
                StoreId = store.Id,
                Status = request.Status
            }, cancellationToken);
           
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("something went wrong please try again later");
        }
    }
}