using Billing.Api.Models.Dto;

namespace Billing.Api.Services.OrderService;

public interface IOrderService
{
    Task<bool> ProcessOrderAsync(OrderRequest orderRequest);
}

