using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class CustomersController : ODataController
{
    private readonly NorthwindContext _db;

    public CustomersController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Customers);

    [EnableQuery]
    public IActionResult Get(string key)
        => Ok(_db.Customers.Find(key));
}