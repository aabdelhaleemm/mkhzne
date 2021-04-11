using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class OrderStatus : AuditableEntity
    {
        public int Id { get; set; }
        public string Status { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public IEnumerable<Order> Orders { get; set; }
    }
}