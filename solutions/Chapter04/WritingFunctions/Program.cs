using static System.Console;

static void TimesTable(byte number)
{
    WriteLine($"This is the {number} times table:");
    for (byte row = 0; row <= 12; row++)
    {
        WriteLine($"{row} x {number} = {row * number}");
    }
}

TimesTable(12);
