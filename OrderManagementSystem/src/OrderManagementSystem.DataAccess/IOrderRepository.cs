using OrderManagementSystem.Core.Models;

namespace OrderManagementSystem.DataAccess;

public interface IOrderRepository : IDisposable
{
    public Task<Product?> GetProductAsync(int id);
    public Task<Order?> GetOrderAsync(int id);
    public int? InsertOrderAsync(Order newOrder);
    public Task<bool> ProductExistsAsync(int productId);
}