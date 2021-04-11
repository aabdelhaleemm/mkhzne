using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Category : AuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public IEnumerable<Product> Products { get; set; }
    }
}