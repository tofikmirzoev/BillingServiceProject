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
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult CommitTransaction([FromBody] CommitTransactionRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);
        
        var doPurchaseResult = _transactionService.CommitTransaction(request);
        if (doPurchaseResult.IsFailed)
            return BadRequest(doPurchaseResult.Reasons);
        
        return Ok(doPurchaseResult.Value);
    }

    [HttpPost("MakeTopUp")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult MakeTopUp([FromBody] TopUpRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);

        var makeTopUpResult = _transactionService.MakeTopUp(request);
        if (makeTopUpResult.IsFailed)
            return BadRequest(makeTopUpResult.Reasons);

        return Ok(makeTopUpResult);
    }
    
    [HttpPost("Collection")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult Collection([FromBody] CollectionRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);

        var collectionResult = _transactionService.Collect(request);
        if (collectionResult.IsFailed)
            return BadRequest(collectionResult.Reasons);

        return Ok(collectionResult);
    }
}