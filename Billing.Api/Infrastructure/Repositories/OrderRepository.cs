using Billing.Api.Models.Entities;

namespace Billing.Api.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private BillingDbContext _dbContext;

    public OrderRepository(BillingDbContext dbContext)
	{
        _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public async Task<Order?> GetOrderAsync(int orderId) => await _dbContext.Orders.FindAsync(orderId);
}

