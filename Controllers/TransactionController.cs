using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;

    public TransactionController(ITransactionRepository transactionRepository, IMapper mapper)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
    }
    
    [HttpPost("DoPurchase")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult DoPurchase([FromBody] DoPurchaseRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);
        
        if (!_transactionRepository.AccountExists(request.fromAccount))
        {
            ModelState.AddModelError("","There is no such Account");
            return StatusCode(204, ModelState);
        }
        
        if (!_transactionRepository.AccountExists(request.toAccount))
        {
            ModelState.AddModelError("","Please make sure that destination account typed correctly");
            return StatusCode(204, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_transactionRepository.DoPurchase(request.fromAccount, request.toAccount, request.amountToSend, request.purchaseType))
        {
            ModelState.AddModelError("", "Something went wrong while saving...");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully transferred");
    }
    
    [HttpGet("MakeTopUp/{accountId}/{amount}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult DoPurchase(string accountId, double amount)
    {
        if (accountId == null || amount <= 0)
            return BadRequest(ModelState);
        
        if (!_transactionRepository.AccountExists(accountId))
        {
            ModelState.AddModelError("","There is no such Account");
            return StatusCode(403, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_transactionRepository.MakeTopUp(accountId, amount))
        {
            ModelState.AddModelError("", "Something went wrong while saving...");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully transferred");
    }
}