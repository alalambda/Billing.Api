namespace Billing.Api.Services.PaymentService;

public class AdyenService : IPaymentService
{
	public AdyenService()
	{
	}

    public async Task<bool> CallbackAsync(decimal amount)
    {
        // TODO implement adyen integration
        return true;
    }
}

