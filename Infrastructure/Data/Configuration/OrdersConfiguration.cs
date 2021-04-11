using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class OrdersConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasOne(x => x.Store)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StoreId)
                .OnDelete(DeleteBehavior.NoAction);
            builder.HasOne(x => x.Status)
                .WithMany(x => x.Orders)
                .HasForeignKey(x => x.StatusId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}