using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared; // NorthwindContext
using Microsoft.AspNetCore.Mvc; // [BindProperty], IActionResult

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    private NorthwindContext _db;
    public IEnumerable<Supplier>? Suppliers { get; set; }
    
    [BindProperty]
    public Supplier? Supplier { get; set; }

    public SuppliersModel(NorthwindContext injectedContext)
    {
        _db = injectedContext;
    }
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Suppliers";
        Suppliers = _db.Suppliers
            .OrderBy(c => c.Country)
            .ThenBy(c => c.CompanyName);
    }

    public IActionResult OnPost()
    {
        if (Supplier is null || !ModelState.IsValid)
        {
            return Page();
        }

        _db.Suppliers.Add(Supplier);
        _db.SaveChanges();
        return RedirectToPage("/suppliers");
    }
}