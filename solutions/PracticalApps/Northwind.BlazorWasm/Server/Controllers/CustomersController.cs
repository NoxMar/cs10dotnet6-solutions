using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.BlazorWasm.Server.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CustomersController : ControllerBase
{
    private readonly NorthwindContext _db;

    public CustomersController(NorthwindContext db)
    {
        _db = db;
    }

    [HttpGet]
    public async Task<IList<Customer>> GetCustomersAsync() 
        => await _db.Customers.ToListAsync();

    [HttpGet("in/{country}")]
    public async Task<IList<Customer>> GetCustomersAsync(string country) 
        => await _db.Customers
            .Where(c => c.Country == country)
            .ToListAsync();

    [HttpGet("{id}")]
    public async Task<Customer?> GetCustomerAsync(string id)
        => await _db.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id);

    [HttpPost]
    public async Task<Customer?> CreateCustomerAsync(Customer customerToAdd)
    {
        Customer? existing = await _db.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == customerToAdd.CustomerId);
        if (existing is null)
        {
            return existing;
        }

        _db.Customers.Add(customerToAdd);
        return await _db.SaveChangesAsync() == 1 ? customerToAdd : existing;
    }

    [HttpPut]
    public async Task<Customer?> UpdateCustomerAsync(Customer c)
    {
        _db.Entry(c).State = EntityState.Modified;
        return await _db.SaveChangesAsync() == 1 ? c : null;
    }

    [HttpDelete("{id}")]
    public async Task<int> DeleteCustomerAsync(string id)
    {
        Customer? c = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        if (c is null)
        {
            return 0;
        }

        _db.Customers.Remove(c);
        return await _db.SaveChangesAsync();
    }
}