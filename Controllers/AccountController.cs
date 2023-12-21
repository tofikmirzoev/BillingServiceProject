using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;


namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
    public IActionResult GetAccounts()
    {
        var accounts = _accountService.GetAccounts();
        if (accounts.IsFailed)
            return BadRequest(accounts.Reasons);
        
        return Ok(accounts);
    }

    [HttpGet("{accountId}")]
    [ProducesResponseType(200, Type = typeof(AccountDTO))]
    [ProducesResponseType(400)]
    public IActionResult GetAccount(string accountId)
    {
        var accountResult = _accountService.GetAccount(accountId);
        if (accountResult.IsFailed)
            return BadRequest(accountResult.Reasons);
        
        return Ok(accountResult);
    }

    [HttpPost("UpdateBalance")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult UpdateBalance([FromBody] UpdateBalanceRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);

        var updateBalanceResult = _accountService.UpdateBalance(request);
        if (updateBalanceResult.IsFailed)
            return BadRequest(updateBalanceResult.Reasons);
        
        return Ok(updateBalanceResult);
    }
    
    // [HttpPost("registerAccount/{customerId}")]
    // [ProducesResponseType(200)]
    // [ProducesResponseType(400)]
    // public IActionResult RegisterAccount([FromBody] AccountDTO accountDto, string customerId)
    // {
    //     if (accountDto == null)
    //         return BadRequest(ModelState);
    //
    //     var accountMap = _mapper.Map<Account>(accountDto);
    //
    //     if (_accountRepository.AccountExists(accountMap.AccountId))
    //     {
    //         ModelState.AddModelError("","Account is already exist");
    //         return StatusCode(403, ModelState);
    //     }
    //
    //     if (!ModelState.IsValid)
    //         return BadRequest(ModelState);
    //     
    //     if (!_accountRepository.AddAccount(accountMap, customerId))
    //     {
    //         ModelState.AddModelError("", "Something went wrong while saving...");
    //         return StatusCode(500, ModelState);
    //     }
    //
    //     return Ok("Successfully created");
    // }
}