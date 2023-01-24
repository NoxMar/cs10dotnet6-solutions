﻿using Microsoft.EntityFrameworkCore;

using Packt.Shared;

using static System.Console;

WriteLine($"Using {ProjectConstants.DatabaseProvider} databse provider.");
//QueryingCategories();
//FilteredIncludes();
QueryingProducts();

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