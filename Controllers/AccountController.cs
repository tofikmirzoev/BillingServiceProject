using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.Data;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Models;
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

    public AccountController(IAccountRepository accountRepository, IMapper mapper)
    {
        _accountRepository = accountRepository;
        _mapper = mapper;
    }

    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
    public IActionResult GetAccounts()
    {
        var accounts = _mapper.Map<List<AccountDTO>>(_accountRepository.GetAccounts());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(accounts);
    }

    [HttpGet("{accountId}")]
    [ProducesResponseType(200, Type = typeof(AccountDTO))]
    [ProducesResponseType(400)]
    public IActionResult GetAccount(string accountId)
    {
        if (!_accountRepository.AccountExists(accountId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var account = _mapper.Map<AccountDTO>(_accountRepository.GetAccount(accountId));
        return Ok(account);
    }

    [HttpPut("{accountId}/{newBalance}")]
    [ProducesResponseType(200, Type = typeof(bool))]
    [ProducesResponseType(400)]
    public IActionResult UpdateBalance(string accountId, double newBalance)
    {
        if (!_accountRepository.AccountExists(accountId))
            return NotFound();

        if (newBalance < 0)
        {
            ModelState.AddModelError("","New Balance can not be less than 0");
            return BadRequest(ModelState);
        }
        
        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var result = _accountRepository.UpdateBalance(accountId, newBalance);
        return Ok(result);
    }

    [HttpPost("registerAccount/{customerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult RegisterAccount([FromBody] AccountDTO accountDto, string customerId)
    {
        if (accountDto == null)
            return BadRequest(ModelState);

        var accountMap = _mapper.Map<Account>(accountDto);

        if (_accountRepository.AccountExists(accountMap.AccountId))
        {
            ModelState.AddModelError("","Account is already exist");
            return StatusCode(403, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_accountRepository.AddAccount(accountMap, customerId))
        {
            ModelState.AddModelError("", "Something went wrong while saving...");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
}