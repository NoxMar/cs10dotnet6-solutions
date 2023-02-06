using System.Diagnostics;
using static System.Console;

OutputThreadInfo();
Stopwatch timer = Stopwatch.StartNew();
/*
WriteLine("Running methods synchronously on one thread.");
MethodA();
MethodB();
MethodC();
*/
WriteLine("Running methods asynchronously on multiple threads");
Task taskA = new(MethodA);
taskA.Start();
Task taskB = Task.Factory.StartNew(MethodB);
Task taskC = Task.Run(MethodC);
Task.WaitAll(taskA, taskB, taskC);
WriteLine($"{timer.ElapsedMilliseconds:#,##0}ms elapsed.");

static void OutputThreadInfo()
{
    Thread t = Thread.CurrentThread;
    WriteLine("Thread Id: {0}, Priority: {1}, Background: {2}, Name: {3}",
        t.ManagedThreadId,
        t.Priority,
        t.IsBackground,
        t.Name ?? "null");
}

static void MethodA()
{
    WriteLine("Starting Method A...");
    OutputThreadInfo();
    Thread.Sleep(3000); // simulates 3 s of work
    WriteLine("Finished Method A.");
}

static void MethodB()
{
    WriteLine("Starting Method B...");
    OutputThreadInfo();
    Thread.Sleep(2000); // simulates 2 s of work
    WriteLine("Finished method B.");
}

static void MethodC()
{
    WriteLine("Starting Method C...");
    OutputThreadInfo();
    Thread.Sleep(1000); // simulates 1 s of work
    WriteLine("Finished method C.");
}