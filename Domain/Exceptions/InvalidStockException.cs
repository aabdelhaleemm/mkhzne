using System;

namespace Domain.Exceptions
{
    public class InvalidStockException : Exception
    {
        public InvalidStockException(string msg)
            : base(msg)
        {
        }
    }
}