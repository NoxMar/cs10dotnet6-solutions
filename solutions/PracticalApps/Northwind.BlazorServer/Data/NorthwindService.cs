using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.BlazorServer.Data;

public class NorthwindService : INorthwindService
{
    private readonly NorthwindContext _db;

    public NorthwindService(NorthwindContext db)
    {
        _db = db;
    }

    public Task<List<Customer>> GetCustomersAsync() =>
        _db.Customers.ToListAsync();

    public Task<List<Customer>> GetCustomersAsync(string country) =>
        _db.Customers
            .Where(c => c.Country == country)
            .ToListAsync();

    public Task<Customer?> GetCustomerAsync(string id) =>
        _db.Customers
            .FirstOrDefaultAsync(c => c.CustomerId == id);

    public async Task<Customer> CreateCustomerAsync(Customer c)
    {
        await _db.Customers.AddAsync(c);
        await _db.SaveChangesAsync();
        return c;
    }
    
    public async Task<Customer> UpdateCustomerAsync(Customer c)
    {
        _db.Entry(c).State = EntityState.Modified;
        await _db.SaveChangesAsync();
        return c;
    }

    public async Task DeleteCustomerAsync(string id)
    {
        var customer = await _db.Customers.FirstOrDefaultAsync(c => c.CustomerId == id);
        if (customer is null)
        {
            return;
        }

        _db.Customers.Remove(customer);
        await  _db.SaveChangesAsync();
    }
}