namespace Billing.Api.Services.DocumentGenerationService;

public interface IDocumentGenerationService
{
    string GenerateReceipt(int orderNumber);
}