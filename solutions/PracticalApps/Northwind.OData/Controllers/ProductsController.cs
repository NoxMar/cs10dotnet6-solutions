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
    public IActionResult Get()
        => Ok(_db.Products);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Products.Find(key));
}