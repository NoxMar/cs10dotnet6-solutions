using static System.Console;

try
{
    checked
    {

        int max = 500;
        for (byte i = 0; i < max; i++)
        {
            WriteLine(i);
        }
    }
}
catch (OverflowException)
{
    Console.WriteLine($"Error: integer overflow encountered for `byte` variable, max value for this type is {byte.MaxValue}");
}