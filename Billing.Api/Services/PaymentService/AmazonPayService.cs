namespace Billing.Api.Services.PaymentService;

public class AmazonPayService : IPaymentService
{
	public AmazonPayService()
	{
	}

    public Task<bool> CallbackAsync(decimal amount)
    {
        throw new NotImplementedException();
    }
}

