using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.Interfaces;
using BillingAPI.ServiceIntefaces;
using BillingAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TransactionController : Controller
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IMapper _mapper;
    private readonly ITransactionService _transactionService;

    public TransactionController(ITransactionRepository transactionRepository, IMapper mapper, ITransactionService transactionService)
    {
        _transactionRepository = transactionRepository;
        _mapper = mapper;
        _transactionService = transactionService;
    }
    
    [HttpPost("DoPurchase")]
    [ProducesResponseType(200)]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    public IActionResult DoPurchase([FromBody] DoPurchaseRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);
        
        bool doPurchase = _transactionService.DoPurchase(request, ModelState);

        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!doPurchase)
            return StatusCode(500, ModelState);
        
        return Ok("Successfully transferred");
    }
    
    [HttpPost("MakeTopUp")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult MakeTopUp([FromBody] TopUpRequest request)
    {
        
        if (request == null)
            return BadRequest(ModelState);

        var makeTopUp = _transactionService.MakeTopUp(request,ModelState);
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        if (!makeTopUp)
            return StatusCode(500, ModelState);
        
        return Ok("Successfully transferred");
    }
}