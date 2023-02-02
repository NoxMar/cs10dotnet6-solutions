using System.Diagnostics;
using static System.Console;

Stopwatch watch = new();
Write("Press ENTER to start. ");
_ = ReadLine();
watch.Start();

const int max = 45;
IEnumerable<int> numbers = Enumerable.Range(start: 1, count: 45);
WriteLine($"Calculating Fibonacci sequence up to {max}. Please wait...");
int[] fibonacciNumbers = numbers
    .AsParallel()
    .Select(Fibonacci)
    .OrderBy(n => n)
    .ToArray();
watch.Stop();
WriteLine($"{watch.ElapsedMilliseconds:#,##0} elapsed milliseconds.");
Write("Results: ");
WriteLine(String.Join(' ', fibonacciNumbers));

static int Fibonacci(int term) =>
    term switch
    {
        1 => 0,
        2 => 1,
        _ => Fibonacci(term - 1) + Fibonacci(term - 2)
    };