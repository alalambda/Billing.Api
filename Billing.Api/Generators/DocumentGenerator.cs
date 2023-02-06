using Billing.Api.Models.Enums;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Billing.Api.Generators;

public static class DocumentGenerator
{
    public static object Generate(Document documentType, PageModel model)
    {
        var template = GetDocumentTemplate(documentType);
        var path = $"../Templates/{template}";

        // TODO document generation logic

        return new object();
    }

    private static string GetDocumentTemplate(Document documentType)
    {
        return documentType switch
        {
            Document.Receipt => "Receipt.cshtml",
            Document.ShippingConfirmation => throw new NotImplementedException(),
            _ => throw new NotImplementedException(),
        };
    }
}