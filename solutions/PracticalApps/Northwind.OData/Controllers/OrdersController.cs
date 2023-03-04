using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class OrdersController : ODataController
{
    private readonly NorthwindContext _db;

    public OrdersController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Orders);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Orders.Find(key));
}