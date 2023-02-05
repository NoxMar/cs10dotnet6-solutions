using Exercise02.Db;

using static System.Console;

Exercise2();

static void Exercise2()
{
    using NorthwindContext db = new();
    var city = ReadCity(db);
    if (city is null)
    {
        return;
    }
    var customersInCity = db.Customers
        .Where(c => c.City == city);
    if (customersInCity is null)
    {
        WriteLine($"There were no customers found in {city}");
        return;
    }

    WriteLine($"There are {customersInCity.Count()} customers in {city}.");
    foreach (var c in customersInCity)
    {
        WriteLine(c.CompanyName);
    }
}

static string? ReadCity(NorthwindContext db)
{
    var cities = db.Customers.Select(c => c.City).Distinct();
    if (cities is null)
    {
        return null;
    }
    WriteLine("Cities with at least one customer:");
    WriteLine(String.Join(", ", cities));
    Write("Enter the name of a city: ");
    return ReadLine()!;
}