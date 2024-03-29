﻿using static System.Console;

// a string array is a sequence that implements IEnumerable<string>
string[] names = new[] { "Michael", "Pam", "Jim", "Dwight", "Angela", "Kevin",
    "Toby", "Creed" };
WriteLine("Deferred execution");
// Question: Which names end with an M?

// (written with LINQ extension methods)
var query1 = names.Where(name => name.EndsWith("m"));

// (written with LINQ query comprehension syntax)
var query2 = from name in names where name.EndsWith("m") select name;

// Getting answers
string[] result1 = query1.ToArray();

List<string> result2 = query2.ToList();

foreach (string name in query1)
{
    WriteLine(name); // outputs Pam
    names[2] = "Jimmy"; // change Jim to Jimmy to show that finding next element is deferred
    // by changing before the second iteration and showing that original "Jim" is not returned.
}

WriteLine("Writing queries:");
// With explicit delegate instantiation:
// var query = names.Where(new Func<string, bool>(NameLongerThanFour));

IOrderedEnumerable<string> query = names
    .Where(name => name.Length > 4)
    .OrderBy(name => name.Length)
    .ThenBy(name => name);

foreach (string item in query)
{
    WriteLine(item);
}

WriteLine("Filtering by type:");
List<Exception> exceptions = new()
{
    new ArgumentException(),
    new SystemException(),
    new IndexOutOfRangeException(),
    new InvalidOperationException(),
    new NullReferenceException(),
    new InvalidCastException(),
    new OverflowException(),
    new DivideByZeroException(),
    new ApplicationException(),
};

var arithmeticExceptionsQuery = exceptions.OfType<ArithmeticException>();
foreach (ArithmeticException exception in arithmeticExceptionsQuery)
{
    WriteLine(exception);
}