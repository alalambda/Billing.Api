namespace Billing.Api.Services.PaymentService;

public class AdyenService : IPaymentService
{
	public AdyenService()
	{
	}

    public Task<bool> CallbackAsync(decimal amount)
    {
        throw new NotImplementedException();
    }
}

