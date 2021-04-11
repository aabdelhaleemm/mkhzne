using Domain.Entities;
using Domain.Exceptions;
using Domain.ValueObjects;
using Xunit;

namespace Domain.UnitTest.ValueObject
{
    public class StockTest
    {
        [Fact]
        public void ShouldReturnTheCorrectCountWithoutAnyException()
        {
            //Arrange 
            var productTest = new Product();

            //Act
            productTest.Count = Stock.From(15);

            //Assert
            Assert.Equal(15, productTest.Count);
        }

        [Fact]
        public void ShouldThrowInvalidStockException()
        {
            var productTest = new Product();

            Assert.Throws<InvalidStockException>(() => productTest.Count = Stock.From(-5));
        }
    }
}