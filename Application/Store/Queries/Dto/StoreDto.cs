using System.Collections.Generic;

namespace Application.Store.Queries.Dto
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StoreCategoryDto> Categories { get; set; }
        public IEnumerable<StoreOrderDto> Orders { get; set; }
    }
}