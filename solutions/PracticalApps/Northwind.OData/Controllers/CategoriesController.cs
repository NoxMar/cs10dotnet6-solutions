using Microsoft.AspNetCore.Mvc; // IActionResult
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Packt.Shared;

namespace Northwind.OData.Controllers;

public class CategoriesController : ODataController
{
    private readonly NorthwindContext _db;

    public CategoriesController(NorthwindContext db)
    {
        _db = db;
    }

    [EnableQuery]
    public IActionResult Get()
        => Ok(_db.Categories);

    [EnableQuery]
    public IActionResult Get(int key)
        => Ok(_db.Categories.Find(key));
}