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