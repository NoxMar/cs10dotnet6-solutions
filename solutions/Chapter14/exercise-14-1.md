## Exercise 14.1 -Test your knowledge

## 1. List six method names that can be specific in an HTTP request.

- `GET`
- `POST`
- `PUT`
- `DELETE`
- `HEAD`
- `PATCH`

## 2. List six status codes and their descriptions that can be returned in an HTTP response.

- `200` - OK
- `201` - Created
- `204` - No Content
- `500` - Internal Server Error
- `404` - Not Found
- `403` - Not Authorized

## 3. In ASP.NET Core, what is the `Startup` class used for?

It's used for configuring request pipeline (in the `Configure` method) and services for dependency injection container (in the `ConfigureServices` method). Advantage of doing it in the `Startup` class is clean separation of configuration of the request and response pipeline from the configuration of dependency injection.

## 4. What does the acronym HSTS stand for and what does it do?

HSTS (**H**TTP **S**trict **T**ransport **S**ecurity) - it's a mechanism meant to protect against MITM attacks by sending a specified header to the browser. This header will cause the browser (of course if supported) to force all communication with the sending domain (potentially including subdomains, configurable with this header using an optional switch) including from the user requests but also from JS scripts over a secure connection. 

## 5. How do you enable static HTML pages for a website?

By calling `UseStaticFiles()` on an application builder. Order of `Use` calls including `UseStaticFiles` will determine to order of matching requests. If you also need files like `index.html` and such to be static you need to call `app.UseDefaultFiles()` **before** `UseStaticFiles()`.

## 6. How do you mix C# code into the middle of HTML to create a dynamic page?

There are a few ways to accomplish this.

- `@SimpleExpression` - will **evaluate the expression** and **replace** it with **its result** in the response to the client. Expression needs to be as simple as accessing properties, and calling methods so parser can determin where it ends (for examples arithmetic operations would be problematic, consider: `@99 - diff bottles of beer on the wall.`)

-  `@(AnyExpression)` - work similar to the simple `@` syntax but informs the parser of bound of your expression explicitly. Returning to the previous example:

  `@(99 - diff) bottles of beer on the wall.` solves the previous problem.

- For multi-line pieces of code:

  ```cs
  @{
      // Multi-line pieces of C# code in HTML page
  }
  ```

##  7. How can you define shared layouts for Razor Pages?

By creating files in the `Shared` subcatalog in the `Pages` catalog, naming the layout file staring with `_` and calling `@RenderBody()` which will return particular content to be placed in this layout when it's used in any given page.

On the side on the page containing content (not layout) we need to set layout:

```cs
@{
    Layout = "_Layout";
}
```

To prevent repeating this code in each view we can move it to the `_ViewStart.cshtml` file.

## 8. How can you separate the markup from the code-behind in a Razor Page?

By creating file with the same name as your razor page in the same location and adding extension `.cs` to it (resulting in `xyz.cshtml.cs` for Razor Page `xyz.cshtml`). On the side of razor page created class should be declared as its model (using `@model Fully.Qualified.Name.Of.The.Model`).

## 9. How do you configure an Entity Framework Core data context for use with an ASP.NET Core website?

By adding it the DI container using `UseXYZ` (like `UseSqlServer(options)`, `UseSqlite(options)` and such) or using your custom extension method then injecting it into your page using either constructor argument in code-behind file or `@inject` directly in the razor page (if the page doesn't have code-behind file).

## 10. How can you reuse Razor Pages with ASP.NET Core 2.2 or later?

By packaging it in Razor Class Library.