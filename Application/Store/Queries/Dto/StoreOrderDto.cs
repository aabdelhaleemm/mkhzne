using System;

namespace Application.Store.Queries.Dto
{
    public class StoreOrderDto
    {
        public int Id { get; set; }

        // public string City { get; set; }
        // public string Address { get; set; }
        public string CustomerName { get; set; }
        public int CustomerPhoneNumber { get; set; }
        public string Notes { get; set; }
        public DateTime Time { get; set; }
        public int StatusId { get; set; }
        public StoreOrderStatusDto Status { get; set; }
    }
}