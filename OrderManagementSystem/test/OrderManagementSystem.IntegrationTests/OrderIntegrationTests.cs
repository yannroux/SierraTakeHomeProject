using System.Net;
using System.Net.Http.Json;
using OrderManagementSystem.Core.Models;

namespace OrderManagementSystem.IntegrationTests
{
    public class OrderIntegrationTests
    {
        [Fact]
        public async  Task ShouldNotAuthorizeCreateAnOrder()
        {
            var httpClient = new OrderManagementSystemApiFactory().CreateClient();

            var response = await httpClient.PostAsJsonAsync("/api/orders/v1/createOrder", 
                new Order
                {
                    CustomerId = 122364,
                    ProductId = 1,
                    Quantity = 10
                });

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Unauthorized, response.StatusCode);
        }

        [Fact]
        public async Task ShouldCreateAnOrder()
        {
            var httpClient = new OrderManagementSystemApiFactory()
                .CreateClient();

            httpClient.DefaultRequestHeaders.Add("Authorization",
                "Bearer eyJhbGciOiJIUzI1NiJ9.eyJhdWQiOiJodHRwOi8vbG9jYWxob3N0IiwiaXNzIjoiT3JkZXJNYW5hZ2VtZW50U3lzdGVtLkF1dGhlbnRpY2F0aW9uU2VydmljZSIsImV4cCI6MTcyMTM1MTUyMCwiaWF0IjoxNjg5NzI5MTIwfQ.Dze0W7jOaVbgx3csDWWz691QtJNcuxcYmJFqgwt96KM");
            
            var response = await httpClient.PostAsJsonAsync("/api/orders/v1/createOrder",
                new Order
                {
                    CustomerId = 122364,
                    ProductId = 1,
                    Quantity = 10
                });

            Assert.NotNull(response);
            Assert.Equal(HttpStatusCode.Created, response.StatusCode);
        }
    }
}