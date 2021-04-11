using System.Collections.Generic;
using Domain.Common;
using Domain.ValueObjects;

namespace Domain.Entities
{
    public class Product : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public Stock Count { get; set; }
        public string Color { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public IEnumerable<OrderProducts> Orders { get; set; }
    }
}