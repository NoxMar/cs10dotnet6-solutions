using Microsoft.AspNetCore.Mvc; // [Route], [ApiController], ControllerBase
using Packt.Shared; // Customer
using Northwind.WebApi.Repositories; // ICustomerRepository
namespace Northwind.WebApi.Controllers;
// base address: api/customers
[ApiController]
[Route("/api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly ICustomerRepository _repository;

    public CustomersController(ICustomerRepository repository)
    {
        _repository = repository;
    }

    // GET: api/customers
    // GET: api/customers?country=[country]
    [HttpGet]
    [ProducesResponseType(200, Type = typeof(IEnumerable<Customer>))]
    public async Task<IEnumerable<Customer>> GetCustomers(string? country)
    {
        var customers = await _repository.RetrieveAllAsync();
        return string.IsNullOrWhiteSpace(country) ? customers : customers.Where(c => c.Country == country);
    }

    // GET: api/customers/[id]
    [HttpGet("{id}", Name = nameof(GetCustomer))]
    [ProducesResponseType(200, Type=typeof(Customer))]
    [ProducesResponseType(404)]
    public async Task<IActionResult> GetCustomer(string id)
    {
        var c = await _repository.RetrieveAsync(id);
        return c is not null ? Ok(c) : NotFound(c);
    }
    
    // POST: api/customers
    // BODY: Customer (JSON, XML)
    [HttpPost]
    [ProducesResponseType(201, Type = typeof(Customer))]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Create([FromBody] Customer c)
    {
        if (c is null)
        {
            return BadRequest();
        }

        var addedCustomer = await _repository.CreateAsync(c);
        if (addedCustomer is null)
        {
            return BadRequest("Repository failed to create customer.");
        }

        return CreatedAtRoute( // 201 Created
            routeName: nameof(GetCustomer),
            routeValues: new {id = addedCustomer.CustomerId},
            value: addedCustomer
        );
    }
}