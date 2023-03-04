using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class SuppliersController : ODataController
{
    private readonly NorthwindContext _db;

    public SuppliersController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Suppliers);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Suppliers.Find(key));
}