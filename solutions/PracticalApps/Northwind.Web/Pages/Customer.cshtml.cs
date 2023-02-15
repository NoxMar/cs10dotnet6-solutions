using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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
        var customer = await _db.Customers.FindAsync(id);
        if (customer is null)
        {
            return Redirect("/customers");
        }

        Customer = customer;

        return Page();
    }
}