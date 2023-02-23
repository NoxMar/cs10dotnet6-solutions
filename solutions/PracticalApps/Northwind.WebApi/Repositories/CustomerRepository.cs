using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.WebApi.Repositories;

public class CustomerRepository : ICustomerRepository
{
    // static, thread-safe dictionary as a cache
    private static ConcurrentDictionary<string, Customer>? _customerCache;

    private NorthwindContext _db;

    public CustomerRepository(NorthwindContext db)
    {
        _db = db;
        // pre-load customers from db
        _customerCache = new ConcurrentDictionary<string, Customer>(
            db.Customers.ToDictionary(c => c.CustomerId));
    }
    public async Task<Customer?> CreateAsync(Customer c)
    {
        // normalization
        c.CustomerId = c.CustomerId.ToUpper();
        if (_customerCache is null)
        {
            throw new InvalidOperationException(
                "`customerCache` should be initialized in the constructor but wasn't.");
        }
        await _db.Customers.AddAsync(c);
        var affected = await _db.SaveChangesAsync();
        return affected != 1 ? null : _customerCache.AddOrUpdate(c.CustomerId, c, UpdateCache);
    }

    public Task<IEnumerable<Customer>> RetrieveAllAsync()
    {
        return Task.FromResult(_customerCache?.Values ?? Enumerable.Empty<Customer>());
    }

    public Task<Customer?> RetrieveAsync(string id)
    {
        id = id.ToUpper();
        if (_customerCache is null)
        {
            throw new InvalidOperationException(
                "`customerCache` should be initialized in the constructor but wasn't.");
        }
        _customerCache.TryGetValue(id, out var c);
        return Task.FromResult(c);
    }

    private static Customer UpdateCache(string id, Customer c)
    {
        if (_customerCache is null)
        {
            throw new InvalidOperationException(
                "`customerCache` should be initialized in the constructor but wasn't.");
        }

        if (!_customerCache.TryGetValue(id, out var old))
        {
            return null!;
        }

        return _customerCache.TryUpdate(id, c, old) ? c : null!;
    }

    public async Task<Customer?> UpdateAsync(string id, Customer c)
    {
        // normalize customer Id
        id = id.ToUpper();
        if (_customerCache is not null)
        {
            _customerCache.Remove(id, out var oldVal);
            if (oldVal is not null) _db.Entry(oldVal).State = EntityState.Detached;
        }
        c.CustomerId = c.CustomerId.ToUpper();
        // update database
        _db.Customers.Update(c);
        var affected = await _db.SaveChangesAsync();
        return affected != 1 ? null : UpdateCache(id, c);
    }

    public async Task<bool?> DeleteAsync(string id)
    {
        if (_customerCache is null)
        {
            throw new InvalidOperationException(
                "`customerCache` should be initialized in the constructor but wasn't.");
        }
        id = id.ToUpper();

        var c = await _db.Customers.FindAsync(id);
        if (c is null)
        {
            return null;
        }
        _db.Customers.Remove(c);
        var affected = await _db.SaveChangesAsync();
        return affected != 1 ? null : _customerCache.TryRemove(id, out c);
    }
}