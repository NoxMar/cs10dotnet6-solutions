using static System.Console;

const int PER_LINE = 10;
const int MAX_NUMBER = 100;

for (int i = 1; i < MAX_NUMBER; i += PER_LINE)
{
    var mappedNumbers = Enumerable.Range(i, PER_LINE)
        .Select(i => FizzBuzz(i));
    WriteLine(String.Join(", ", mappedNumbers));
}

string FizzBuzz(int i)
{
    return (i % 3, i % 5) switch
    {
        (0, 0) => "FizzBuzz",
        (0, _) => "Fizz",
        (_, 0) => "Buzz",
        (_, _) => i.ToString()
    };
}