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

WriteLine($"{harry.Name} has {harry.Children.Count} children.");
WriteLine($"{marry.Name} has {marry.Children.Count} children.");
WriteLine($"{jill.Name} has {jill.Children.Count} children.");

WriteLine($"{harry.Name}'s first child is named \"{harry.Children[0].Name}\"");