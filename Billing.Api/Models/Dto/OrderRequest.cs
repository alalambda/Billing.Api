using Billing.Api.Models.Enums;

namespace Billing.Api.Models.Dto;

public class OrderRequest
{
    public int OrderNumber { get; init; }
    public int UserId { get; init; }
    public decimal PayableAmount { get; init; }
    public PaymentGateway PaymentGateway { get; init; }
    public string? Description { get; init; }
}