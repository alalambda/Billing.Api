using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Billing.Api.Models.DocumentModels;

public class ReceiptModel : PageModel
{
    public string UserName { get; init; }
    public int OrderNumber { get; init; }
    public string PaymentMethod { get; set; }
    public decimal PaidAmount { get; init; }

    public void OnGet()
    {
    }

}
