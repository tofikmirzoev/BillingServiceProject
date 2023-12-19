using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.BillingResponses;
using BillingAPI.Data;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IAccountRepository _accountRepository;
    private readonly IMapper _mapper;
    private readonly IAccountService _accountService;

    public AccountController(IAccountRepository accountRepository, IMapper mapper, IAccountService accountService)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
        _accountService = accountService;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
    public IActionResult GetAccounts()
    {
        var accounts = _mapper.Map<List<AccountDTO>>(_accountService.GetAccounts());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(accounts);
    }

    [HttpGet("{accountId}")]
    [ProducesResponseType(200, Type = typeof(AccountDTO))]
    [ProducesResponseType(400)]
    public IActionResult GetAccount(string accountId)
    {
        var account = _mapper.Map<AccountDTO>(_accountService.GetAccount(accountId, ModelState));
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(account);
    }

    [HttpPost("UpdateBalance")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult UpdateBalance([FromBody] UpdateBalanceRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);

        var result = _accountService.UpdateBalance(request, ModelState);
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        return Ok(result);
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