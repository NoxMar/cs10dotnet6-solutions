using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.ChangeTracking; // CollectionTracking
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore.Storage; // IDbContextTransaction

using Packt.Shared;

using static System.Console;

WriteLine($"Using {ProjectConstants.DatabaseProvider} databse provider.");
// QueryingCategories();
// FilteredIncludes();
// QueryingProducts();
// QueryingWithLike();
// if (AddProduct(categoryId: 6, productName: "Bob's Burgers", price: 500M))
// {
//     WriteLine("Add product successful.");
// }
// if (IncreaseProductPrice(productNameStartsWith: "Bob", amount: 20M))
// {
//     WriteLine("Update product price successful.");
// }
// ListProducts();
int deletedRows = DeleteProducts(productNameStartsWith: "Bob");
WriteLine($"{deletedRows} product(s) were deleted.");

static void QueryingCategories()
{
    using Northwind db = new();
    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());
    WriteLine("Categories and how many products they have:");
    IQueryable<Category>? categories;
    // = db.Categories;
    //.Include(c => c.Products);
    db.ChangeTracker.LazyLoadingEnabled = false;
    Write("Enable eager loading? (Y/N): ");
    bool eagerLoading = (ReadKey().Key == ConsoleKey.Y);
    bool explicitLoading = false;
    WriteLine();
    if (eagerLoading)
    {
        categories = db.Categories?.Include(c => c.Products);
    }
    else
    {
        categories = db.Categories;
        WriteLine("Enable explicit loading? (Y/N) :");
        explicitLoading = (ReadKey().Key == ConsoleKey.Y);
        WriteLine();
    }
    if (categories is null)
    {
        WriteLine("No categories found.");
        return;
    }

    // execute query and enumerate results
    foreach (Category c in categories)
    {
        Write($"Explicitly load products for {c.CategoryName}? (Y/N):");
        ConsoleKeyInfo key = ReadKey();
        WriteLine();
        if (key.Key == ConsoleKey.Y)
        {
            CollectionEntry<Category, Product> products =
                db.Entry(c).Collection(c2 => c2.Products);
            if (!products.IsLoaded)
            {
                products.Load();
            }
        }
        WriteLine($"{c.CategoryName} has {c.Products.Count} products.");
    }
}

static void FilteredIncludes()
{
    using Northwind db = new();
    Write("Enter a minimum for units in stock: ");
    string unitsInStock = ReadLine() ?? "10";
    int stock = int.Parse(unitsInStock);
    IQueryable<Category>? categories = db.Categories?
        .Include(c => c.Products.Where(p => p.Stock >= stock));

    if (categories is null)
    {
        WriteLine("No categories found.");
        return;
    }

    WriteLine($"ToQueryString: {categories.ToQueryString()}");
    // execute query and enumerate results
    foreach (Category c in categories)
    {
        WriteLine($"{c.CategoryName} has {c.Products.Count} products with a minimum of {stock} units in stock.");
        foreach (Product p in c.Products)
        {
            WriteLine($"   {p.ProductName} has {p.Stock} units in stock.");
        }
    }
}

static void QueryingProducts()
{
    using Northwind db = new();
    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());
    WriteLine("Products that cost more than a price, highest at top.");
    string? input;
    decimal price;
    do
    {
        Write("Enter a product price:");
        input = ReadLine();
    } while (!decimal.TryParse(input, out price));
    Write("Enter a minimum for units in stock: ");

    IQueryable<Product>? products = db.Products?
        .Where(p => p.Cost > price)
        .OrderByDescending(p => p.Cost);

    if (products is null)
    {
        WriteLine("No products found");
        return;
    }
    foreach (var p in products)
    {
        WriteLine(
            "{0}: {1} costs {2:$#,##0.00} and has {3} in stock.",
            p.ProductId,
            p.ProductName,
            p.Cost,
            p.Stock);
    }
}

static void QueryingWithLike()
{
    using Northwind db = new();

    ILoggerFactory loggerFactory = db.GetService<ILoggerFactory>();
    loggerFactory.AddProvider(new ConsoleLoggerProvider());

    Write("Enter part of a product name: ");
    string input = ReadLine()!;

    IQueryable<Product>? products = db.Products?
        .Where(p => EF.Functions.Like(p.ProductName, $"%{input}%"));
    if (products is null)
    {
        WriteLine("No products found.");
        return;
    }

    foreach (Product p in products)
    {
        WriteLine("{0} has {1} units in stock. Discontinued? {2}",
            p.ProductName,
            p.Stock,
            p.Discontinued);
    }
}

static bool AddProduct(
    int categoryId, string productName, decimal? price)
{
    using Northwind db = new();
    Product p = new()
    {
        CategoryId = categoryId,
        ProductName = productName,
        Cost = price
    };
    // mark product as added in change tracking
    db.Products!.Add(p);
    // save tracked change to database
    int affectedRows = db.SaveChanges();
    return (affectedRows == 1);
}

static void ListProducts()
{
    using Northwind db = new();
    WriteLine("{0,-3} {1,-35} {2,8} {3,5} {4}",
        "Id", "Product Name", "Cost", "Stock", "Disc.");

    foreach (var p in db.Products!
        .OrderByDescending(p => p.Cost))
    {
        WriteLine("{0:000} {1,-35} {2,8:$#,##0.00} {3,5} {4}",
            p.ProductId, p.ProductName, p.Cost, p.Stock, p.Discontinued);
    }
}

static bool IncreaseProductPrice(
    string productNameStartsWith, decimal amount
)
{
    using Northwind db = new();
    Product productToUpdate = db.Products!
        .First(p => p.ProductName.StartsWith(productNameStartsWith));

    productToUpdate.Cost += amount;
    int affectedRows = db.SaveChanges();
    return affectedRows == 1;
}

static int DeleteProducts(string productNameStartsWith)
{
    using Northwind db = new();
    using IDbContextTransaction t = db.Database.BeginTransaction();
    WriteLine($"Transaction isolation level: {t.GetDbTransaction().IsolationLevel}");
    IQueryable<Product>? products = db.Products?
        .Where(p => p.ProductName.StartsWith(productNameStartsWith));

    if (products is null)
    {
        WriteLine("No products found to delete.");
        return 0;
    }

    db.Products!.RemoveRange(products);
    int affectedRows = db.SaveChanges();
    t.Commit();
    return affectedRows;
}