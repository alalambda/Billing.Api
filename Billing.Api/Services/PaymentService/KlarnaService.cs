namespace Billing.Api.Services.PaymentService;

public class KlarnaService : IPaymentService
{
	public KlarnaService()
	{
	}

    public Task<bool> CallbackAsync(decimal amount)
    {
        throw new NotImplementedException();
    }
}

