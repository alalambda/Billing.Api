namespace Billing.Api.Models.Enums;

public enum OrderStatus
{
	Pending, // Customer started the checkout process but did not complete it
    AwaitingPayment, // Customer has completed the checkout process, but payment has yet to be confirmed
    Completed, // Customer has completed the checkout process and payment has been confirmed
    Cancelled, 
	Refunded,
	Declined,
}

