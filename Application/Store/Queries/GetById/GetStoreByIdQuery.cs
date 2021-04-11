using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Store.Queries.Dto;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Store.Queries.GetById
{
    public class GetStoreByIdQuery : IRequest<StoreDto>
    {
        public GetStoreByIdQuery(int storeId)
        {
            StoreId = storeId;
        }

        public int StoreId { get; }
    }


    public class GetStoreByOwnerIdQueryHandler : IRequestHandler<GetStoreByIdQuery, StoreDto>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetStoreByOwnerIdQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper
            , ICurrentUserService currentUserService
        )
        {
            _applicationDbContext = applicationDbContext;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<StoreDto> Handle(GetStoreByIdQuery request, CancellationToken cancellationToken)
        {
            var store = await _applicationDbContext.Stores.FindAsync(request.StoreId);

            if (store == null) throw new NotFoundException("Cant find store information");
            if (store.OwnerId != _currentUserService.UserId) throw new UnAuthorizedRequest("U cant access this store!");

            var storeDto = await _applicationDbContext.Stores.AsNoTracking()
                .Where(x => x.Id == request.StoreId)
                .ProjectTo<StoreDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(cancellationToken);

            return storeDto;
        }
    }
}