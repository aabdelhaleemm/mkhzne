using System;
using System.Collections.Generic;
using Domain.Common;

namespace Domain.Entities
{
    public class Order : AuditableEntity
    {
        public int Id { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string CustomerName { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
        public int StatusId { get; set; }
        public OrderStatus Status { get; set; }
        public int StoreId { get; set; }
        public Store Store { get; set; }
        public ICollection<OrderProducts> OrderProducts { get; set; }
    }
}