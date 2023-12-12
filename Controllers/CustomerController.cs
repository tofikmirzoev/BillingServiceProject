using AutoMapper;
using BillingAPI.DTO;
using BillingAPI.Interfaces;
using BillingAPI.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BillingAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CustomerController : Controller
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public CustomerController(ICustomerRepository customerRepository, IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }
    
    [HttpGet]
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
    }
    
    [HttpPost("addNewCustomer")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public IActionResult AddCustomer([FromBody] CustomerDTO customerDto, [FromQuery] string accountId)
    {
        if (customerDto == null || accountId == null)
            return BadRequest(ModelState);
            
        var customerMap = _mapper.Map<Customer>(customerDto);
        
        if (_customerRepository.CustomerExists(customerMap.CustomerId))
        {
            ModelState.AddModelError("","Customer is already exist");
            return StatusCode(403, ModelState);
        }

        if (!ModelState.IsValid)
            return BadRequest(ModelState);
        
        if (!_customerRepository.AddCustomer(customerMap, accountId))
        {
            ModelState.AddModelError("", "Something went wrong while saving...");
            return StatusCode(500, ModelState);
        }

        return Ok("Successfully created");
    }
    
}