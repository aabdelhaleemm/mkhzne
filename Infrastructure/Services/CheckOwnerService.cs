using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;

namespace Infrastructure.Services
{
    public class CheckOwnerService : ICheckOwnerService
    {
        private readonly ICurrentUserService _currentUserService;

        public CheckOwnerService(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }
        public void CheckOwner(Store store)
        {
            if (store == null)
                throw new NotFoundException("Cant Find the store ");
            if (store.OwnerId != _currentUserService.UserId)
                throw new UnAuthorizedRequest("You cannot add to this store");
            
        }
    }
}