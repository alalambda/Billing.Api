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

    private const int DescriptionMaxLength = 500;

	public OrderService(
        IUserRepository userRepository,
        IOrderRepository orderRepository)
	{
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
	}

    public async Task<bool> ProcessOrderAsync(OrderRequest orderRequest)
    {
        _paymentService = GetPaymentService(orderRequest.PaymentGateway);
        var status = await _paymentService.CallbackAsync(orderRequest.PayableAmount);

        if (status)
        {
            await _orderRepository.UpdateOrderAsync(orderRequest.OrderNumber, OrderStatus.Completed);
        }

        return status;
    }

    public async Task<bool> IsRequestValid(OrderRequest orderRequest)
    {
        if (orderRequest.PayableAmount <= 0 || orderRequest.Description?.Length > DescriptionMaxLength)
        {
            return false;
        }

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

        return true;
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
        return paymentGateway switch
        {
            PaymentGateway.Adyen => new AdyenService(),
            PaymentGateway.AmazonPay => new AmazonPayService(),
            PaymentGateway.Klarna => new KlarnaService(),
            _ => throw new NotImplementedException(),
        };
    }
}

