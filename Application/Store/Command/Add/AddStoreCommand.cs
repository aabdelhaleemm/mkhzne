using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

namespace Application.Store.Command.Add
{
    public class AddStoreCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }

    public class AddStoreCommandHandler : IRequestHandler<AddStoreCommand, bool>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;

        public AddStoreCommandHandler(IApplicationDbContext applicationDbContext,
            ICurrentUserService currentUserService)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
        }

        public async Task<bool> Handle(AddStoreCommand request, CancellationToken cancellationToken)
        {
            await _applicationDbContext.Stores.AddAsync(new Domain.Entities.Store()
            {
                Name = request.Name,
                OwnerId = _currentUserService.UserId
            }, cancellationToken);
            
            return await _applicationDbContext.SaveChangesAsync(cancellationToken) > 0
                ? true
                : throw new CantAddEntityException("Something went wrong!");
        }
    }
}