using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Category.Queries.GetOwnerIdByCategoryId
{
    public class GetOwnerIdByCategoryIdQuery : IRequest<int>
    {
        public GetOwnerIdByCategoryIdQuery(int categoryId)
        {
            CategoryId = categoryId;
        }

        public int CategoryId { get; set; }
    }

    public class GetOwnerIdByCategoryIdQueryHandler : IRequestHandler<GetOwnerIdByCategoryIdQuery, int>
    {
        private readonly IApplicationDbContext _applicationDbContext;

        public GetOwnerIdByCategoryIdQueryHandler(IApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<int> Handle(GetOwnerIdByCategoryIdQuery request, CancellationToken cancellationToken)
        {
            var storeOwnerId = await _applicationDbContext.Categories.AsNoTracking()
                .Where(x => x.Id == request.CategoryId)
                .Select(x => x.Store.OwnerId)
                .FirstOrDefaultAsync(cancellationToken);
            return storeOwnerId;
        }
    }
}