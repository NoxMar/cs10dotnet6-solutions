using Exercise02.Db;

using static System.Console;

Exercise2();

static void Exercise2()
{
    using NorthwindContext db = new();
    var city = ReadCity();
    var customersInCity = db.Customers
        .Where(c => c.City == city);
    if (customersInCity is null)
    {
        WriteLine($"There were no customers found in {city}");
        return;
    }

    WriteLine($"There are {customersInCity.Count()} customers in {city}:");
    foreach (var c in customersInCity)
    {
        WriteLine(c.CompanyName);
    }
}

static string ReadCity()
{
    Write("Enter the name of a city: ");
    return ReadLine()!;
}