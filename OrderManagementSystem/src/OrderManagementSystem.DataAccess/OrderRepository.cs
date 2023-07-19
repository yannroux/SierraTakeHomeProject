using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OrderManagementSystem.Core.Models;

namespace OrderManagementSystem.DataAccess;

public class OrderRepository : IOrderRepository
{
    readonly OrderDbContext _dbContext;
    readonly ILogger<OrderRepository> _logger;
    bool _disposed = false;
    public OrderRepository(OrderDbContext dbContext, ILogger<OrderRepository> logger)
    {
        _dbContext = dbContext;
        _logger = logger;
    }

    public async Task<Product?> GetProductAsync(int id)
    {
        try
        {
            return await _dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
        }
        catch (Exception exp)
        {
            _logger.LogError($"An exception of type {exp.GetBaseException().GetType()} occurred while trying to get a product with id {id}. Details: {exp.Message}");
        }

        return null;
    }

    public async Task<Order?> GetOrderAsync(int id)
    {
        try
        {
            return await _dbContext.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.Id == id);
        }
        catch (Exception exp)
        {
            _logger.LogError($"An exception of type {exp.GetBaseException().GetType()} occurred while trying to get an order with id {id}. Details: {exp.Message}");
        }

        return null;
    }

    public int? InsertOrderAsync(Order newOrder)
    {
        try
        {
            var order =  _dbContext.Orders
                .FromSqlRaw($"dbo.CreateOrder {newOrder.CustomerId}, {newOrder.ProductId}, {newOrder.Quantity}")
                .AsEnumerable()
                .FirstOrDefault();

            return order?.Id;
        }
        catch (Exception exp)
        {
            _logger.LogError($"An exception of type {exp.GetBaseException().GetType()} occurred while trying to create a new order. Details: {exp.Message}");
        }
        return null;
    }

    public async Task<bool> ProductExistsAsync(int productId)
    {
        try
        {
            return await _dbContext.Products.AnyAsync(p => p.Id == productId);
        }
        catch (Exception exp)
        {
            _logger.LogError($"An exception of type {exp.GetBaseException().GetType()} occurred while trying to check the existence of a product with id {productId}. Details: {exp.Message}");
        }
        return false;
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
        
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _dbContext.Dispose();
            }
            _disposed = true;
        }
    }
}