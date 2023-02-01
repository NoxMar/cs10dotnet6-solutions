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
    WriteLine("Products that cost less than $10:");
    foreach (Product p in sortedAndFilteredProducts)
    {
        WriteLine(
            $"{p.ProductId}: {p.ProductName} costs {p.UnitPrice:$#,##0.00}");
    }
    WriteLine();
}