using Billing.Api.Generators;
using Billing.Api.Infrastructure.Repositories;
using Billing.Api.Models.DocumentModels;
using Billing.Api.Models.Entities;
using Billing.Api.Models.Enums;

namespace Billing.Api.Services.DocumentGenerationService;

public class DocumentGenerationService : IDocumentGenerationService
{
    private readonly IOrderRepository _orderRepository;
    private readonly IUserRepository _userRepository;

	public DocumentGenerationService(
        IOrderRepository orderRepository,
        IUserRepository userRepository)
	{
        _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
        _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
    }

    public async Task<object> GenerateReceipt(int orderNumber)
    {
        var order = await _orderRepository.GetOrderAsync(orderNumber);
        if (order == null)
        {
            throw new ArgumentNullException();
        }

        if (order.OrderStatus != OrderStatus.Completed)
        {
            throw new BadHttpRequestException("Order has invalid status to generate receipt");
        }

        var user = await _userRepository.GetUserAsync(order.UserId);
        if (user == null)
        {
            throw new ArgumentNullException();
        }

        var model = MapReceiptDocumentModel(order, user);
        var document = DocumentGenerator.Generate(Document.Receipt, model);
        return document;
    }

    private static ReceiptModel MapReceiptDocumentModel(Order order, User user)
    {
        return new ReceiptModel
        {
            UserName = $"{user.FirstName} {user.LastName}",
            OrderNumber = order.OrderNumber,
            PaymentMethod = order.PaymentGateway.ToString(),
            PaidAmount = order.PayableAmount
        };
    }
}

