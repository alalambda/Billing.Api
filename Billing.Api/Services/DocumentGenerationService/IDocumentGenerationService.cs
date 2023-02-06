namespace Billing.Api.Services.DocumentGenerationService;

public interface IDocumentGenerationService
{
    Task<object> GenerateReceipt(int orderNumber);
}