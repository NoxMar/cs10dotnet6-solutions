using Packt.Shared;
using static System.Console;

Person bob = new();
WriteLine(bob.ToString());

bob.Name = "Bob Smith";
bob.DateOfBirth = new(1965, 12, 22);
WriteLine($"{bob.Name} was born on {bob.DateOfBirth:dddd, d MMMM yyyy}");

Person alice = new() { Name = "Alice Jones", DateOfBirth = new(1998, 3, 7) };

WriteLine($"{alice.Name} was born on {alice.DateOfBirth:dd MMM yy}");


bob.FavoriteAncientWonder = WondersOfTheAncientWorld.StatueOfZeusAtOlympia;

WriteLine($"{bob.Name}'s favorite wonder is {bob.FavoriteAncientWonder}, Its integer is {(int)bob.FavoriteAncientWonder}");

bob.BucketList =
    WondersOfTheAncientWorld.HangingGardensOfBabylon
    | WondersOfTheAncientWorld.MausoleumAtHalicarnassus;
// bob.BucketList = (WondersOfTheAncientWorld)18;
WriteLine($"{bob.Name}'s bucket list is {bob.BucketList}");

bob.Children.Add(new Person() { Name = "Alfred" }); // C# 3.0 and later
bob.Children.Add(new() { Name = "Zoe" }); // C# 9.0 and later
WriteLine($"{bob.Name} has {bob.Children.Count} children:");
foreach (var child in bob.Children)
{
    WriteLine($"  {child.Name}");
}