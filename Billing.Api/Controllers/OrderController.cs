using Billing.Api.Models.Dto;
using Billing.Api.Services.DocumentGenerationService;
using Billing.Api.Services.OrderService;
using Microsoft.AspNetCore.Mvc;

namespace Billing.Api.Controllers;

[Route("api/[controller]")]
public class OrderController : Controller
{
    private readonly IOrderService _orderService;
    private readonly IDocumentGenerationService _documentGenerationService;

    public OrderController(
        IOrderService orderService,
        IDocumentGenerationService documentGenerationService)
    {
        _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
        _documentGenerationService = documentGenerationService ?? throw new ArgumentNullException(nameof(documentGenerationService));
    }

    [HttpPost]
    public async Task<ActionResult<object>> ProcessOrderAsync([FromHeader(Name = "Autorization")] string token, [FromBody] OrderRequest request)
    {
        // TODO validate token

        var isValid = await _orderService.IsRequestValid(request);
        if (!isValid)
        {
            return BadRequest();
        }

        var processingStatus = await _orderService.ProcessOrderAsync(request);
        if (!processingStatus)
        {
            return new StatusCodeResult(StatusCodes.Status500InternalServerError);
        }

        var receipt = await _documentGenerationService.GenerateReceipt(request.OrderNumber);

        return Ok(receipt);
    }
}
