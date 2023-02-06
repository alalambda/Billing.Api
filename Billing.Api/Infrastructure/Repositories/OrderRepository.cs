using Billing.Api.Models.Entities;
using Billing.Api.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Billing.Api.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private BillingDbContext _dbContext;

    public OrderRepository(BillingDbContext dbContext)
	{
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

        _dbContext.Database.EnsureCreatedAsync();
    }

    public async Task<Order?> GetOrderAsync(int orderNumber) => await _dbContext.Orders.FindAsync(orderNumber);

    public async Task UpdateOrderAsync(int orderNumber, OrderStatus orderStatus)
    {
        var entity = await GetOrderAsync(orderNumber);
        if (entity != null)
        {
            entity.OrderStatus = orderStatus;
            _dbContext.Entry(entity).State = EntityState.Modified;
        }
    }
}

