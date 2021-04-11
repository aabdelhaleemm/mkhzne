using System.Threading;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Domain.Entities.Store> Stores { get; set; }
        public DbSet<Domain.Entities.Product> Products { get; set; }
        public DbSet<Domain.Entities.Order> Orders { get; set; }
        public DbSet<Domain.Entities.Category> Categories { get; set; }
        public DbSet<OrderProducts> OrderProducts { get; set; }
        public DbSet<Domain.Entities.OrderStatus> OrderStatus { get; set; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}