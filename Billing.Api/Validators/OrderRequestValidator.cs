using Billing.Api.Models.Dto;
using FluentValidation;

public class OrderRequestValidator : AbstractValidator<OrderRequest>
{
    public OrderRequestValidator()
    {
        RuleFor(orderRequest => orderRequest.PayableAmount).GreaterThan(0).WithErrorCode("Amount should be greater than 0");
        RuleFor(orderRequest => orderRequest.PaymentGateway).IsInEnum().WithErrorCode("Payment gateway not supported");
        RuleFor(orderRequest => orderRequest.Description).MaximumLength(500).WithErrorCode("Description should be max 500 characters");
    }
}