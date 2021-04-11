using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Store.Queries.GetAll
{
    public class GetAllStoresQuery : IRequest<List<GetAllStoresDto>>
    {
        
    }
    public class GetAllStoresQueryHandler : IRequestHandler<GetAllStoresQuery,List<GetAllStoresDto>>
    {
        private readonly IApplicationDbContext _applicationDbContext;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetAllStoresQueryHandler(IApplicationDbContext applicationDbContext, 
            ICurrentUserService currentUserService,
            IMapper mapper)
        {
            _applicationDbContext = applicationDbContext;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }
        public async Task<List<GetAllStoresDto>> Handle(GetAllStoresQuery request, CancellationToken cancellationToken)
        {
            var stores = await _applicationDbContext.Stores.AsNoTracking()
                .Where(x => x.OwnerId == _currentUserService.UserId)
                .ProjectTo<GetAllStoresDto>(_mapper.ConfigurationProvider)
                .ToListAsync( cancellationToken);

            
            return stores;

        }
    }
}