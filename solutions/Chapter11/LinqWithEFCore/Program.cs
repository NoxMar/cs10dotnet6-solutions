using Packt.Shared; // Northwind, Category, Product
using Microsoft.EntityFrameworkCore; // DbSet<T>
using static System.Console;

// FilterAndSort();
// JoinCategoriesAndProducts();
GroupJoinCategoriesAndProducts();

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

static void JoinCategoriesAndProducts()
{
    using Northwind db = new();
    var queryJoin = db.Categories.Join(
        inner: db.Products,
        outerKeySelector: category => category.CategoryId,
        innerKeySelector: product => product.CategoryId,
        resultSelector: (c, p) =>
            new { c.CategoryName, p.ProductName, p.ProductId })
        .OrderBy(cp => cp.CategoryName);
    foreach (var item in queryJoin)
    {
        WriteLine(
            $"{item.ProductId}: {item.ProductName} is in {item.CategoryName}");
    }
}

static void GroupJoinCategoriesAndProducts()
{
    using Northwind db = new();
    var queryGroup = db.Categories.AsEnumerable().GroupJoin(
        inner: db.Products,
        outerKeySelector: category => category.CategoryId,
        innerKeySelector: product => product.CategoryId,
        resultSelector: (category, matchingProducts) => new
        {
            category.CategoryName,
            Products = matchingProducts.OrderBy(p => p.ProductName)
        });

    foreach (var categoryProducts in queryGroup)
    {
        WriteLine("{0} has {1} product(s)",
            categoryProducts.CategoryName,
            categoryProducts.Products.Count());
        foreach (var product in categoryProducts.Products)
        {
            WriteLine($" {product.ProductName}");
        }
    }
}