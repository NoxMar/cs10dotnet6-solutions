using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.Web.Pages;

public class CustomerModel : PageModel
{
    private NorthwindContext _db;
    public Customer Customer = null!;
    public CustomerModel(NorthwindContext db)
    {
        _db = db;
    }
    public async Task<IActionResult> OnGet(string id)
    {
        var customer = await _db.Customers
            .Where(c => c.CustomerId == id)
            .Include(c => c.Orders)
            .FirstOrDefaultAsync();
        if (customer is null)
        {
            return Redirect("/customers");
        }

        Customer = customer;

        return Page();
    }
}