using Domain.Exceptions;
using ValueOf;

namespace Domain.ValueObjects
{
    public class Stock : ValueOf<int, Stock>
    {
        protected override void Validate()
        {
            if (Value < 0) throw new InvalidStockException("Stock is empty!");
            base.Validate();
        }
    }
}