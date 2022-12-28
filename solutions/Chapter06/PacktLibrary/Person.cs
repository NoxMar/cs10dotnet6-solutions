using static System.Console;

namespace Packt.Shared;

public class Person : object
{
    // fields
    public string? Name; // ? - nullable
    public DateTime DateOfBirth;
    public List<Person> Children = new();

    public void WriteToConsole()
    {
        WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
    }
}
