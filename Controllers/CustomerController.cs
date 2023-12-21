using AutoMapper;
using BillingAPI.BillingMessages;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using BillingAPI.ServiceIntefaces;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustomerService _customerService;

    public CustomerController(ICustomerService customerService)
    {
        _customerService = customerService;
    }
    
    /*[HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<CustomerDTO>))]
    public IActionResult GetAccounts()
    {
        var customers = _mapper.Map<List<CustomerDTO>>(_customerRepository.GetCustomers());
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        return Ok(customers);
    }

    [HttpGet("{customerId}")]
    [ProducesResponseType(200, Type = typeof(Account))]
    [ProducesResponseType(400)]
    public IActionResult GetAccount(string customerId)
    {
        if (!_customerRepository.CustomerExists(customerId))
            return NotFound();

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        var customer = _mapper.Map<CustomerDTO>(_customerRepository.GetCustomer(customerId));
        return Ok(customer);
    }  */  
    [HttpPost("addNewCustomer")]
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