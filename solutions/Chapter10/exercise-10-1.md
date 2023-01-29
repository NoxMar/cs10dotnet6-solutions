# Exercise 10.1 - Test your knowledge

## 1. What type would you use for the property that represents a table, for example, the `Products` property of a database context?

`DBSet<T>` from namespace `Microsoft.EntityFrameworkCore` where type used for mapping rows as generic `T`. For example of `Products` that would be `DBSet<Product>`.

## 2. What type would you use for the property that represents a one-to-many relationship, for example, the Products property of a Category entity?

I would use `ICollection<T>` where `T` is (many-side) entity type. For example `ICollection<Product>`.

## 3. What is the EF Core convention for primary keys?

`<entity name>Id` or just `Id` case insensitive.

## 4. When might you use an annotation attribute in an entity class?

If I would want to specify some additional properties for the column to which the field maps. Examples include:
- `[Required]` if specify that value is required
- `[StringLength(min, max)]` if the text column has minimum and maximum length for values
- `[Range(min, max)]` to define range of acceptable integer values

Additionally to specify that the field is not mapped (`[NotMapped]`)

To specify other DB mapping details, like:
- `[Column("name")]` to specify name of the column mapped to that field
- `[Precision]` to specify precision and scale of decimal columns

All of those can be also accomplished using Fluent API.

## 5. Why might you choose the Fluent API in preference to annotation attributes?

To make your classes POCOs (Plain Old CLR Objects) and not pollute models with things that might be considered DB mapping implementation details as it's likely they will touch different parts of the application.

On the other hand you might opt to use annotations if you use the same models for other parts of your application and want to use the same parameters for other things like client-side (with Blazor) or api ingest validation.

## 6. What does a transaction isolation level of `Serializable` mean?

That entities that are "touched" by the transaction are locked (based on the ranges of keys used) to any actions that would affect results (which includes inserts and deletes) by any other process. This prevents all integrity problems (at the cost of performance).

## 7. What does the DbContext.SaveChanges() method return?

Number (`int`) of rows modified in any way (added, updated, deleted) by the implicit transaction executed by calling this method.

## 8. What is the difference between eager loading and explicit loading?

Eager loading loads all related entities as soon as entity is loaded. This saves round-trips to the database when accessing them but should be used only when you will need them (almost) always since otherwise it will load them unnecessarily.

Explicit loading doesn't load any related entities by default and lets developer control which related entities are loaded and when.

## 9. How should you define an EF Core entity class to match the following table?

```sql
CREATE TABLE Employees(
  EmpId INT IDENTITY,
  FirstName NVARCHAR(40) NOT NULL,
  Salary MONEY
)
```

```cs
using System.ComponentModel.DataAnnotations;

public class Employee
{
    [Column("EmpId")]
    public int EmpId { get; set; }
    [Required]
    [MaxLength(40)]
    public string FirstName { get; set; }
    [Column(TypeName = "money")]
    public decimal? Salary { get; set; }
}
```

## 10. What benefit do you get from declaring entity navigation properties as virtual?

This way you can decide to enable lazy-loading proxies to make EF-Core fetch related entities only if and when you try to access them.