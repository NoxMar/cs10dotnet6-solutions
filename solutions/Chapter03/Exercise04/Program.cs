using static System.Console;

byte ReadByteWithPrompt(string prompt)
{
    Write(prompt);
    return byte.Parse(ReadLine()!);
}

try
{
    byte dividend = ReadByteWithPrompt("Enter a number between 0 and 255:");
    byte divisor = ReadByteWithPrompt("Enter another number between 0 and 255:");

    WriteLine($"{dividend} divided by {divisor} is {dividend / divisor}");
}
catch (Exception e)
{
    WriteLine($"{e.GetType().Name}: {e.Message}");
}

