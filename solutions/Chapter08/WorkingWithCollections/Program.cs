using static System.Console;

// WorkingWithLists();
WorkingWithDictionaries();

static void Output(string title, IEnumerable<string> collection)
{
    WriteLine(title);
    foreach (string item in collection)
    {
        WriteLine($"  {item}");
    }
}

static void WorkingWithDictionaries()
{
    // Explicitly adding
    Dictionary<string, string> keywords = new();
    keywords.Add(key: "int", value: "32-bit integer data type");
    keywords.Add("long", "64-bit integer data type");
    keywords.Add("float", "Single precision floating point number");

    // Creating with values using initializer. Converted by calls to `Add` by the compiler.
    Dictionary<string, string> keywords1 = new()
    {
      { "int", "32-bit integer data type" },
      { "long", "64-bit integer data type" },
      { "float", "Single precision floating point number" },
    };

    // Creating with values using alternative syntax. Converted by calls to `Add` by the compiler.
    Dictionary<string, string> keywords2 = new()
    {
        ["int"] = "32-bit integer data type",
        ["long"] = "64-bit integer data type",
        ["float"] = "Single precision floating point number", // last comma is optional
    };

    Output("Dictionary keys:", keywords.Keys);
    Output("Dictionary values:", keywords.Values);
    WriteLine("Keywords and their definitions");
    foreach (KeyValuePair<string, string> item in keywords)
    {
        WriteLine($"  {item.Key}: {item.Value}");
    }
    string key = "long";
    WriteLine($"The definition of {key} is {keywords[key]}");
}

static void WorkingWithLists()
{
    // Simple syntax for creating a list and adding three items
    List<string> cities = new();
    cities.Add("London");
    cities.Add("Paris");
    cities.Add("Milan");

    List<string> cities2 = new() { "London", "Paris", "Milan" };

    List<string> cities3 = new();
    cities.AddRange(new[] { "London", "Paris", "Milan" });

    Output("Initial list", cities);
    WriteLine($"The first city is {cities[0]}.");
    WriteLine($"The last city is {cities[cities.Count - 1]}.");
    cities.Insert(0, "Sydney");
    Output("After inserting Sydney at index 0", cities);
    cities.RemoveAt(1);
    cities.Remove("Milan");
    Output("After removing two cities", cities);
}