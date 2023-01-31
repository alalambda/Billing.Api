namespace Billing.Api.Services.PaymentService;

public interface IPaymentService
{
    Task<bool> CallbackAsync(decimal amount);
}

