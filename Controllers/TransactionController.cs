using BillingAPI.BillingMessages;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionService _transactionService;
    public TransactionController(ITransactionService transactionService)
    {
        _transactionService = transactionService;
    }
    
    [HttpPost("CommitTransaction")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult CommitTransaction([FromBody] PaymentRequest? request)
    {
        if (request == null)
            return BadRequest(ModelState);
        
        var doPurchaseResult = _transactionService.CommitTransaction(request);
        if (doPurchaseResult.IsFailed)
            return BadRequest(doPurchaseResult.Reasons);
        
        return Ok(doPurchaseResult.Value);
    }

    [HttpPost("CalculateSavingAccountBalance")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult CalculateSavingAccountBalance()
    {
        
        return Ok();
    }
}