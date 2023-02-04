# Exercise 11.1 - Test your knowledge

## 1. What are the two required parts of LINQ?

- Extension methods
- providers

## 2. Which LINQ extension method would you use to return a subset of properties from a type?

`Select`, sometimes with with lambda returning anonymous type to not create additional not needed classes.

## 3. Which LINQ extension method would you use to filter a sequence?

`Where` passing predicate to it.

## 4. List five LINQ extension methods that perform aggregation.

- `Aggregate`
- `Average`
- `Count`
- `Max`
- `Min`

## 5. What is the difference between the Select and SelectMany extension methods?

SelectMany takes delegate returning that can return IEnumerable and flattens those enumerables into one resulting sequence.

## 6. What is the difference between `IEnumerable<T>` and `IQueryable<T>`? And how do you switch between them?

`IQueryable<T>`  constructs a expression tree when non-terminating methods are called. When terminating methods is called this tree is mapped to some other query syntax (for example SQL) and executed against a (for example against a database) and then results are returned to the app.
`IEnumerable<T>` pertains to a sequence that is held in memory. It supports more operations thus you might wish to "get" `IEnumerable<T>` from `IQueryable<T>`. This can be done trough `AsEnumerable()` method. It's vital to mention that this **executes query against the database** and **stores results in memory of your program**.

## 7. What does the last type parameter `T` in generic `Func` delegates like `Func<T1, T2, T>` represent?

`T` signifies the return type of the func.

## 8. What is the benefit of a LINQ extension method that ends with OrDefault?

They return default value for the type (or default supplied as an argument) when called on an empty enumerable whereas their counterparts without `OrDefault` suffix throw an exception.

## 9. Why is query comprehension syntax optional?

Because it is just converted at compile time to calls to regular LINQ methods, thus not adding any new capabilities (except the let keyword) just providing syntactic sugar.

## 10. How can you create your own LINQ extension methods?

By declaring public static extension method (meaning method with this prefixed before its first argument) and making type of this argument IEnumerable.