using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // DbSet<T>
using static System.Console;

FilterAndSort();

static void FilterAndSort()
{
    using Northwind db = new();
    DbSet<Product> allProducts = db.Products!;
    IQueryable<Product> filteredProduct =
        allProducts.Where(product => product.UnitPrice < 10M);
    IOrderedQueryable<Product> sortedAndFilteredProducts =
        filteredProduct.OrderByDescending(product => product.UnitPrice);
    var projectedProducts = sortedAndFilteredProducts
        .Select(product => new
        {
            product.ProductId,
            product.ProductName,
            product.UnitPrice,
        });
    WriteLine("Products that cost less than $10:");
    foreach (var p in projectedProducts)
    {
        WriteLine(
            $"{p.ProductId}: {p.ProductName} costs {p.UnitPrice:$#,##0.00}");
    }
    WriteLine();
}