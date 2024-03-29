﻿using Packt.Shared; // Northwind, Category, Product
using System.Xml.Linq;
using Microsoft.EntityFrameworkCore; // DbSet<T>
using static System.Console;

// FilterAndSort();
// JoinCategoriesAndProducts();
// GroupJoinCategoriesAndProducts();
// AggregateProducts();
// OutputProductsAsXml();
// ProcessSettings();
CustomExtensionsMethods();

static void FilterAndSort()
{
    using Northwind db = new();
    DbSet<Product> allProducts = db.Products!;
    IQueryable<Product> processedProducts = allProducts.ProcessSequence();
    IQueryable<Product> filteredProduct =
        processedProducts.Where(product => product.UnitPrice < 10M);
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

static void AggregateProducts()
{
    using Northwind db = new();
    WriteLine("{0,-25} {1,10}", "Product count:", db.Products!.Count());
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        "Highest product price:",
        db.Products!.Max(p => p.UnitPrice));
    WriteLine("{0,-25} {1,10:N0}",
        "Sum of units in stock:",
        db.Products!.Sum(p => p.UnitsInStock));
    WriteLine("{0,-25} {1,10:N0}",
        "Sum of units on order",
        db.Products!.Sum(p => p.UnitsOnOrder));
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        "Average unit price:",
        db.Products!.Average(p => p.UnitPrice));
    WriteLine("{0,-25} {1,10:$#,##0.00}",
        "Value of units in stock:",
        db.Products!.Sum(p => p.UnitsInStock * p.UnitPrice));
}

static void OutputProductsAsXml()
{
    using Northwind db = new();
    Product[] productsArray = db.Products.ToArray();
    XElement xml = new("products",
        from p in productsArray
        select new XElement("product",
            new XAttribute("id", p.ProductId),
            new XAttribute("price", p.UnitPrice),
            new XElement("name", p.ProductName)
        )
    );
    WriteLine(xml.ToString());
}

static void ProcessSettings()
{
    XDocument doc = XDocument.Load("settings.xml");
    var appSettings = doc.Descendants("appSettings")
        .Descendants("add")
        .Select(node => new
        {
            Key = node.Attribute("key")?.Value,
            Value = node.Attribute("value")?.Value,
        }).ToArray();
    foreach (var item in appSettings)
    {
        WriteLine($"{item.Key}: {item.Value}");
    }
}

static void CustomExtensionsMethods()
{
    using Northwind db = new();
    WriteLine("Mean units in stock: {0:N0}",
        db.Products!.Average(p => p.UnitsInStock));
    WriteLine("Mean unit price: {0:$#,##0.00}",
        db.Products!.Average(p => p.UnitPrice));
    WriteLine("Median units in stock: {0:N0}",
        db.Products!.Median(p => p.UnitsInStock));
    WriteLine("Median unit price: {0:$#,##0.00}",
        db.Products!.Median(p => p.UnitPrice));
    WriteLine("Mode units in stock: {0:N0}",
        db.Products!.Mode(p => p.UnitsInStock));
    WriteLine("Mode unit price: {0:$#,##0.00}",
        db.Products!.Mode(p => p.UnitPrice));
}