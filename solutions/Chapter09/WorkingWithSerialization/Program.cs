using System.Xml.Serialization; // XmlSerializer
using Packt.Shared; // Person
using NewJson = System.Text.Json.JsonSerializer;

using static System.Console;
using static System.Environment;
using static System.IO.Path;

List<Person> people = new()
{
    new(30_000M)
    {
        FirstName = "Alice",
        LastName = "Smith",
        DateOfBirth = new(1974, 3, 14),
    },
    new(40_000M)
    {
        FirstName = "Bob",
        LastName = "Jones",
        DateOfBirth = new(1969, 11, 23),
    },
    new(20_00M)
    {
        FirstName = "Charlie",
        LastName = "Cox",
        DateOfBirth = new(1984, 5, 4),
        Children = new ()
        {
            new(0M)
            {
                FirstName = "Sally",
                LastName = "Cox",
                DateOfBirth = new(2000, 7, 12),
            },
        },
    },
};

XmlSerializer xs = new(people.GetType());
string path = Combine(CurrentDirectory, "people.xml");
using (FileStream stream = File.Create(path))
{
    xs.Serialize(stream, people);
}

WriteLine("Written {0:N0} bytes of XML to {1}",
    new FileInfo(path).Length,
    path);
WriteLine();

WriteLine(File.ReadAllText(path));

using (FileStream xmlLoad = File.Open(path, FileMode.Open))
{
    List<Person>? loadedPeople = xs.Deserialize(xmlLoad) as List<Person>;
    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine($"{p.LastName} has {p.Children?.Count ?? 0} children.");
        }
    }
}

string jsonPath = Combine(CurrentDirectory, "people.json");
using (StreamWriter jsonStream = File.CreateText(jsonPath))
{
    Newtonsoft.Json.JsonSerializer jss = new();
    jss.Serialize(jsonStream, people);
}
WriteLine();
WriteLine("Written {0:N0} bytes of JSON to: {1}",
    new FileInfo(jsonPath).Length,
    jsonPath);

WriteLine(File.ReadAllText(jsonPath));

using (FileStream jsonLoad = File.Open(jsonPath, FileMode.Open))
{
    List<Person>? loadedPeople = await NewJson.DeserializeAsync(
        utf8Json: jsonLoad,
        returnType: typeof(List<Person>)) as List<Person>;
    if (loadedPeople is not null)
    {
        foreach (Person p in loadedPeople)
        {
            WriteLine($"{p.LastName} has {p.Children?.Count ?? 0} children.");
        }
    }
}