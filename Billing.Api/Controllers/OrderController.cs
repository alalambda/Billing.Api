using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Billing.Api.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Billing.Api.Controllers;

[Route("api/[controller]")]
public class OrderController : Controller
{
    [HttpPost]
    public async Task<ActionResult<object>> ProcessOrderAsync([FromBody] OrderRequest request)
    {
        return Ok();
    }
}
