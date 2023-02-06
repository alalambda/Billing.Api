using Billing.Api.Models.Entities;
using Billing.Api.Models.Enums;

namespace Billing.Api.Infrastructure.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetOrderAsync(int orderNumber);
    Task UpdateOrderAsync(int orderNumber, OrderStatus orderStatus);
}

