using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared; // NorthwindContext

namespace Northwind.Web.Pages;

public class SuppliersModel : PageModel
{
    private NorthwindContext _db;
    public IEnumerable<Supplier>? Suppliers { get; set; }

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
}