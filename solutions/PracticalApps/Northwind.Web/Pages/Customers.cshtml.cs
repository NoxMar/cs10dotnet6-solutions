using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared;

namespace Northwind.Web.Pages;

public record struct CustomerCountryGroup(string Country, IList<Customer> Customers);
public class Customers : PageModel
{
    private NorthwindContext _db;
    public IList<CustomerCountryGroup> CustomersByCountry = null!;
    public Customers(NorthwindContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        CustomersByCountry = _db.Customers
            .GroupBy(c => c.Country)
            .Select(g => new CustomerCountryGroup(g.Key!, g.ToList()))
            .AsEnumerable()
            .OrderBy(g => g.Country)
            .ToList();
    }
}