using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using OrderManagementSystem.Core.Models;
using OrderManagementSystem.DataAccess;

namespace OrderManagementSystem.UnitTests
{
    public class OrderUnitTests
    {
        [Fact]
        public void ShouldCreateOrder()
        {
            var dbContextOptions = new DbContextOptionsBuilder<OrderDbContext>()
                .EnableDetailedErrors()
                .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking)
                .UseSqlServer("Data Source=(local);Initial Catalog=OrderManagementSystem;Trusted_connection=true;TrustServerCertificate=True", 
                    dbContextBuilder =>
                    {
                        dbContextBuilder
                            .EnableRetryOnFailure(1)
                            .CommandTimeout(60)
                            .MinBatchSize(10)
                            .MaxBatchSize(100);
                    })
                .Options;

            IOrderRepository _orderRepository = new OrderRepository(new OrderDbContext(dbContextOptions),
                new Mock<ILogger<OrderRepository>>().Object);

            var newOrder = new Order
            {
                CustomerId = 122364,
                ProductId = 1,
                Quantity = 10
            };

            var result =  _orderRepository.InsertOrderAsync(newOrder);

            Assert.NotNull(result);
            Assert.True(result.Value > 0);
        }

    }
}