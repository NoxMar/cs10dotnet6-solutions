using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class ProductsController : ODataController
{
    private readonly NorthwindContext _db;

    public ProductsController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult GetAll(int version = 1)
    {
        Console.WriteLine($"ProductsController version {version}");
        return Ok(_db.Products);
    }

    [EnableQuery]
    public IActionResult Get(int key, int version = 1)
    {
        Console.WriteLine($"ProductsController version {version}.");
        var p = _db.Products.Find(key);
        if (p is null)
        {
            return NotFound($"Product with id {key} not found");
        }

        if (version == 2)
        {
            p.ProductName += " version 2.0";
        }
        return Ok(p);
    }
}