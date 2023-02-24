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
    
    // PUT: api/customers/[id]
    // BODY: Customer (JSON, XML)
    [HttpPut("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(404)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> Update(string id, [FromBody] Customer c)
    {
        if (c == null || c.CustomerId != id)
        {
            return BadRequest();
        }

        Customer? existing = await _repository.RetrieveAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        await _repository.UpdateAsync(id, c);
        return new NoContentResult();
    }
    
    // DELETE: api/customers/[id]
    [HttpDelete("{id}")]
    [ProducesResponseType(204)]
    [ProducesResponseType(400)]
    [ProducesResponseType(404)]
    public async Task<IActionResult> Delete(string id)
    {
        // take control of problem details
        if (id == "bad")
        {
            ProblemDetails problemDetails = new()
            {
                Status = StatusCodes.Status400BadRequest,
                Type = "https://localhost:5001/customers/failed-to-delete",
                Title = $"Customer ID {id} found but failed to delete.",
                Detail = "More details like Company Name, Country and so on.",
                Instance = HttpContext.Request.Path
            };
            return BadRequest(problemDetails); // 400 Bad Request
        }
        var existing = await _repository.RetrieveAsync(id);
        if (existing is null)
        {
            return NotFound();
        }

        var deleted = await _repository.DeleteAsync(id) ?? false;
        if (!deleted)
        {
            return BadRequest( // 400 Bad request
                $"Customer {id} was found but failed to delete");
        }
        return new NoContentResult();
    }
}