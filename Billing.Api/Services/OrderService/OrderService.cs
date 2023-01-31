using Billing.Api.Infrastructure.Repositories;
using Billing.Api.Models.Dto;
using Billing.Api.Models.Entities;
using Billing.Api.Models.Enums;
using Billing.Api.Services.PaymentService;

namespace Billing.Api.Services.OrderService;

public class OrderService : IOrderService
{
    private readonly IUserRepository _userRepository;
    private readonly IOrderRepository _orderRepository;
    private IPaymentService _paymentService;

	public OrderService(
        IUserRepository userRepository,
        IOrderRepository orderRepository)
	{
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
	}

    public async Task<bool> ProcessOrderAsync(OrderRequest orderRequest)
    {
        var user = await GetUserAsync(orderRequest.UserId);
        var order = await GetOrderAsync(orderRequest.OrderNumber);
        if (user == null || order == null)
        {
            return false;
        }

        if (order.OrderStatus != OrderStatus.AwaitingPayment)
        {
            return false;
        }

        _paymentService = GetPaymentService(orderRequest.PaymentGateway);
        var status = await _paymentService.CallbackAsync(orderRequest.PayableAmount);

        return status;
    }

    private async Task<User?> GetUserAsync(int userId)
    {
        var userEntity = await _userRepository.GetUserAsync(userId);
        return userEntity;
    }

    private async Task<Order?> GetOrderAsync(int orderId)
    {
        var orderEntity = await _orderRepository.GetOrderAsync(orderId);
        return orderEntity;
    }

    private static IPaymentService GetPaymentService(PaymentGateway paymentGateway)
    {
        switch (paymentGateway)
        {
            case PaymentGateway.Adyen:
                return new AdyenService();
            case PaymentGateway.AmazonPay:
                return new AmazonPayService();
            case PaymentGateway.Klarna:
                return new KlarnaService();
            default:
                throw new NotImplementedException();
        }
    }
}

