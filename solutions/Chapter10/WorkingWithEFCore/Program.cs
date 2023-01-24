using Microsoft.EntityFrameworkCore;

using Packt.Shared;

using static System.Console;

WriteLine($"Using {ProjectConstants.DatabaseProvider} databse provider.");
QueryingCategories();

static void QueryingCategories()
{
    using Northwind db = new();
    WriteLine("Categories and how many products they have:");
    IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products);
    if (categories is null)
    {
        WriteLine("No categories found.");
        return;
    }

    // execute query and enumerate results
    foreach (Category c in categories)
    {
        WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
    }
}