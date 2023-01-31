using Billing.Api.Models.Entities;

namespace Billing.Api.Infrastructure.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(int orderId);
}

