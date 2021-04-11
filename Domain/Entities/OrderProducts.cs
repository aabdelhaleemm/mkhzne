using Domain.Common;

namespace Domain.Entities
{
    public class OrderProducts : AuditableEntity
    {
        public int Id { get; set; }
        public string Notes { get; set; }
        public int ProductId { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}