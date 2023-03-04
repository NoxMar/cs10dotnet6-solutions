using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class EmployeesController : ODataController
{
    private readonly NorthwindContext _db;

    public EmployeesController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Employees);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Employees.Find(key));
}