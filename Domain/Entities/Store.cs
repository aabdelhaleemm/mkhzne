using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Store : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int OwnerId { get; set; }
        public User Owner { get; set; }
        
        public ICollection<OrderStatus> OrderStatusCollection { get; set; }
        public ICollection<Category> Categories { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}