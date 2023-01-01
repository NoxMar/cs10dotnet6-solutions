using Packt.Shared;

using static System.Console;

Person harry = new() { Name = "Harry" };
Person marry = new() { Name = "Marry" };
Person jill = new() { Name = "Jill" };
// call instance method
Person baby1 = marry.ProcreateWith(harry);
baby1.Name = "Gary";
// call static method
Person baby2 = Person.Procreate(harry, jill);
// call an operator
Person baby3 = harry * marry;

WriteLine($"{harry.Name} has {harry.Children.Count} children.");
WriteLine($"{marry.Name} has {marry.Children.Count} children.");
WriteLine($"{jill.Name} has {jill.Children.Count} children.");

WriteLine($"{harry.Name}'s first child is named \"{harry.Children[0].Name}\"");

WriteLine($"5! is {Person.Factorial(5)}");

harry.Shout += Harry_Shout;
for (int i = 0; i < 4; i++)
{
    harry.Poke();
}

static void Harry_Shout(object? sender, EventArgs e)
{
    if (sender is null) return;
    Person p = (Person)sender;
    WriteLine($"{p.Name} is this angry: {p.AngerLevel}.");
}

System.Collections.Hashtable lookupObject = new();
lookupObject.Add(1, "Alpha");
lookupObject.Add(2, "Beta");
lookupObject.Add(3, "Gamma");
lookupObject.Add(harry, "Delta");
int key = 2;
WriteLine($"Key {key} has value: {lookupObject[key]}");
WriteLine($"Key {harry} has value: {lookupObject[harry]}");

// generic lookup collection
Dictionary<int, string> lookupIntString = new();
lookupIntString.Add(key: 1, value: "Alpha");
lookupIntString.Add(key: 2, value: "Beta");
lookupIntString.Add(key: 3, value: "Gamma");
lookupIntString.Add(key: 4, value: "Delta");
key = 3;
WriteLine($"Key {key} has value: {lookupObject[key]}");

Person[] people =
{
    new() { Name = "Simon" },
    new() { Name = "Jenny" },
    new() { Name = "Adam" },
    new() { Name = "Richard" }
};
WriteLine("Initial list of people:");
foreach (var p in people)
{
    WriteLine($"  {p.Name}");
}
WriteLine("Use Person's IComparable implementation to sort:");
Array.Sort(people);
foreach (var p in people)
{
    WriteLine($"  {p.Name}");
}

WriteLine("Use PersonComparer's IComparer implementation to sort:");
Array.Sort(people, new PersonComparer());
foreach (var p in people)
{
    WriteLine($"  {p.Name}");
}

DisplacementVector dv1 = new(3, 5);
DisplacementVector dv2 = new(-2, 7);
DisplacementVector dv3 = dv1 + dv2;
WriteLine($"({dv1.X}, {dv1.Y}) + ({dv2.X}, {dv2.Y}) = ({dv3.X}, {dv3.Y})");

Employee john = new()
{
    Name = "John Jones",
    DateOfBirth = new(year: 1990, month: 7, day: 28)
};
john.WriteToConsole();

john.EmployeeCode = "JJ001";
john.HireDate = new(year: 2014, month: 11, day: 23);
WriteLine($"{john.Name} was hired on {john.HireDate:dd/MM/yy}");

WriteLine(john.ToString());

Employee aliceInEmployee = new()
{ Name = "Alice", EmployeeCode = "AA123" };
Person aliceInPerson = aliceInEmployee;
aliceInEmployee.WriteToConsole();
aliceInPerson.WriteToConsole();
WriteLine(aliceInEmployee.ToString());
WriteLine(aliceInPerson.ToString());

if (aliceInPerson is Employee explicitAlice)
{
    WriteLine($"{nameof(aliceInPerson)} IS an Employee");
}

Employee? aliceAsEmployee = aliceInPerson as Employee; // could be null
if (aliceAsEmployee != null)
{
    WriteLine($"{nameof(aliceInPerson)} AS an Employee");
    // safely do something with aliceAsEmployee
}

// inverted check here to demonstrate is not syntax
string isOrIsnt = aliceInPerson is not Employee ? "isn't" : "is";
WriteLine($"{nameof(aliceInPerson)} {isOrIsnt} an Employee");