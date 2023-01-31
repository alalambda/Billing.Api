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

        var orderRequestValidator = new OrderRequestValidator();
        var validationResult = orderRequestValidator.Validate(request);
        if (!validationResult.IsValid)
        {
            return BadRequest(validationResult.Errors);
        }

        var orderProcessingResult = await _orderService.ProcessOrderAsync(request);
        //if (!orderProcessingResult.Status)
        //{
        //    return null;
        //}
        //var receipt = _documentGenerationService.GenerateReceipt(orderProcessingResult); //DocumentGenerator.Generate(Document.Receipt, );

        return Ok();
    }
}
