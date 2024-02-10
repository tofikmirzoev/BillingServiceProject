using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DepositController : Controller
{
    private readonly IDepositService _depositService;
    public DepositController(IDepositService depositService)
    {
        _depositService = depositService;
    }
    
    [HttpGet("GetAllDeposits")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<DepositDTO>))]
    [ProducesResponseType(400)]
    public IActionResult GetAllDeposits()
    {
        var deposits = _depositService.GetAllDeposits();
        if (deposits.IsFailed)
            return BadRequest(deposits.Reasons);

        if (deposits.Value == null)
            return NotFound();

        return Ok(deposits.Value);
    }

    [HttpGet("GetAccountAllDeposits/{accountId}")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<DepositDTO>))]
    [ProducesResponseType(400)]
    public IActionResult GetAccountAllDeposits(string accountId)
    {
        if (accountId == null)
            return BadRequest(ModelState);

        var deposits = _depositService.GetDeposits(accountId);
        if (deposits.IsFailed)
            return BadRequest(deposits.Reasons);

        if (deposits.Value == null)
            return NotFound();

        return Ok(deposits.Value);
    }
    
    [HttpPost("RegisterDeposit")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult RegisterDeposit([FromBody] RegisterDepositRequest registerDepositRequest)
    {
        if (registerDepositRequest == null)
            return BadRequest(ModelState);

        var depositRegistrationResult = _depositService.RegisterDeposit(registerDepositRequest);
        if (depositRegistrationResult.IsFailed)
            return BadRequest(depositRegistrationResult.Reasons);
        
        return Ok(depositRegistrationResult);
    }
    
    [HttpPost("UpdateDeposits")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult UpdateDeposits([FromBody] UpdateDepositsRequest updateDepositsRequest)
    {
        if (updateDepositsRequest == null)
            return BadRequest(ModelState);

        var depositUpdateResult = _depositService.UpdateDeposits(updateDepositsRequest);
        if (depositUpdateResult.IsFailed)
            return BadRequest(depositUpdateResult.Reasons);
        
        return Ok(depositUpdateResult.Value);
    }
    
    [HttpPatch("CloseDeposit/{depositId}")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult CloseDeposit(string depositId)
    {
        var closeDeposit = _depositService.CloseDeposit(depositId);
        if (closeDeposit.IsFailed)
            return BadRequest(closeDeposit.Reasons);
        
        return Ok(closeDeposit);
    }
}