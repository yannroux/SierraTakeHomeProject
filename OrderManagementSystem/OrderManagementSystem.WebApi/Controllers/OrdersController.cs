using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OrderManagementSystem.Core.Models;
using OrderManagementSystem.DataAccess;

namespace OrderManagementSystem.WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]/v1")]
    
    public class OrdersController : ControllerBase
    {
        readonly IOrderRepository _orderRepository;

        public OrdersController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        
        [HttpGet("products/{productId}")]
        public async Task<IActionResult> GetProduct([FromRoute]int productId)
        {
            var product= await _orderRepository.GetProductAsync(productId);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpGet("orders/{orderId}")]
        public async Task<IActionResult> GetOrder([FromRoute] int orderId)
        {
            var order = await _orderRepository.GetOrderAsync(orderId);
            return order != null ? Ok(order) : NotFound();
        }

        [HttpPost("createOrder")]
        [Authorize()]
        public async Task<IActionResult> CreateOrder([FromBody] Order newOrder)
        {
            if (newOrder.Quantity < 0)
                return BadRequest("Creation failed. The quantity must be superior to 0");
            if(!await _orderRepository.ProductExistsAsync(newOrder.ProductId))
                return BadRequest("Creation failed. The product Id was not found");

            var orderId = _orderRepository.InsertOrderAsync(newOrder);
            return orderId != null ? Created($"orders/{orderId}", null) : BadRequest($"Creation failed");
        }

    }
}