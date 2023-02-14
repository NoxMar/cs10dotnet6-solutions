using Microsoft.AspNetCore.Mvc.RazorPages;
using Packt.Shared; // 

// ReSharper disable once CheckNamespace 
namespace Northwind.Razor.Employees.Pages;

public class EmployeesPageModel : PageModel
{
    private NorthwindContext _db;
    public Employee[] Employees { get; set; } = null!;
    public EmployeesPageModel(NorthwindContext db)
    {
        _db = db;
    }
    public void OnGet()
    {
        ViewData["Title"] = "Northwind B2B - Employees";
        Employees = _db.Employees
            .OrderBy(e => e.LastName)
            .ThenBy(e => e.FirstName)
            .ToArray();
    }
}
