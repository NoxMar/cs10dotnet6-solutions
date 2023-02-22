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
}