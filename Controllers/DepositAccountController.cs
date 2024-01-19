using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Repository.UnitOfWork;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class DepositAccountController : Controller
{
    private readonly IAccountRepository _accountRepository;
    public DepositAccountController(IAccountRepository accountRepository)
    {
        _accountRepository = accountRepository;
    }

    [HttpGet("GetDepositAccountsInfo")]
    [ProducesResponseType(200, Type = typeof(IEnumerable<AccountDTO>))]
    public IActionResult GetDepositAccountsInfo()
    {
        return Ok();
    }
    
}