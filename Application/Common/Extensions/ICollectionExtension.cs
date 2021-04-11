using System.Collections.Generic;
using System.Linq;
using Domain.Entities;

namespace Application.Common.Extensions
{
    public static class CollectionExtension
    {
        public static void AddRange<T>(this ICollection<T> collection, ICollection<T> second , int id) where T : OrderProducts
        {
            foreach (var x in second)
            {
                x.OrderId = id;
                collection.Add(x);
            }
        }
    }
}