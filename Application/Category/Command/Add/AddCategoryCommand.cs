using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Category.Command.Add
{
    public class AddCategoryCommand : IRequest<bool>
    {
        public int StoreId { get; set; }
        public string Name { get; set; }
    }

    public class AddCategoryCommandHandler : IRequestHandler<AddCategoryCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICheckOwnerService _checkOwnerService;

        public AddCategoryCommandHandler(IApplicationDbContext applicationDbContext,
            ICheckOwnerService checkOwnerService)
        {
            _applicationDbContext = applicationDbContext;
            _checkOwnerService = checkOwnerService;
        }

        public async Task<bool> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
        {
            var store = await _applicationDbContext.Stores.FindAsync(request.StoreId);
            _checkOwnerService.CheckOwner(store);
            
            await _applicationDbContext.Categories.AddAsync(new Domain.Entities.Category
            {
                Name = request.Name,
                StoreId = request.StoreId
            }, cancellationToken);
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("Cannot add entity please try again later");
        }
    }
}