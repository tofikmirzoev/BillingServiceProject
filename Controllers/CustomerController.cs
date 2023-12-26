using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;
    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    [HttpGet]
    [ProducesResponseType(200)]
    public IActionResult GetAccounts()
    {
        var customersResult = _customerService.GetCustomers();
        if (customersResult.IsFailed)
            return BadRequest(customersResult.Reasons);

        return Ok(customersResult);
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult GetAccount(string customerId)
    {
        if (customerId == null)
            return BadRequest("Account is null");
        
        var customerResult =_customerService.GetCustomer(customerId);
        if (customerResult.IsFailed)
            return BadRequest(customerResult.Reasons);
        
        return Ok(customerResult);
    }
    [HttpPost("addCustomer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult AddCustomer([FromBody] AddCustomerRequest request)
    {
        if (request == null)
            return BadRequest(ModelState);
        
        var result = _customerService.AddCustomer(request, ModelState);
        if (!ModelState.IsValid)
            return BadRequest(ModelState); 

        return Ok(result);
    }
    
}