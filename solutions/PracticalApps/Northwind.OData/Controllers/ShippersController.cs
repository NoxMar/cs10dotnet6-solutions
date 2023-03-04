using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class ShippersController : ODataController
{
    private readonly NorthwindContext _db;

    public ShippersController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Shippers);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Shippers.Find(key));
}