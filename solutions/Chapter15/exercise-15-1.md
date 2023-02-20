## Exercise 15.1 - Test your knowledge

## 1. What do the files with the special names `_ViewStart` and `_ViewImports` do when created in the `Views` folder?

- `_ViewStart` is perpended to any other view. It's most commonly used to set the layout and possibly other common items in the `ViewBag`. It usually doesn't carry HTML.

- `_ViewImports` provides common imports that are accessible from all views.

## 2. What are the names of the three segments defined in the default ASP.NET Core MVC route, what do they represent, and which are optional?

Those are `{controller}`, `{action}` and `{id}`. All of them are optional. `{controller}` and `{action}` are provided with default values (`Home` and `Index` respectively), while `{id}` is marked as optional using `?` suffix.

## 3. What does the default model binder do, and what data types can it handle?

It parses data from the path, query string and request body and maps it to the parameters of given action method. It can handle simple value types, object/struct types and collections. All of those can be nullable.

## 4. In a shared layout file like `_Layout.cshtml`, how do you output the content of the current view?

By calling `RenderBody` method, like so: `@RenderBody()`. Rendered current view will be inserted in the markup at the place where `RenderBody()` was called (as it's just a method returning its markup).

## 5. In a shared layout file like `_Layout.cshtml`, how do you output a section that the current view can supply content for, and how does the view supply the contents for that section?

Rendering can be done using `RenderSection` method. (for example: `@RenderSection("Scripts", required: False)`). View can define contents for section using `@section`. For example given before:

```cs	
@section Scripts
{
    <script>
        alert('Hello from specific page.');
    </script>
}
```

## 6. When calling the `View` method inside a controller's action method, what paths are searched for the view by convention?

Assuming  `<controller>` as placeholder for a controller name (excluding the `Controller` suffix, meaning `Home` for `HomeController` etc.), `<action>` as action's name:

- `/Views/<controller>/<action>.cshtml`
- `/Views/Shared/<action>.cshtml`
- `/Pages/Shared/<action>.cshtml`

##  7. How can you instruct the visitor's browser to cache the response for 24 hours?

To set the caching duration to `24` hours we can use `Duration` parameter of the `ReponseCache` attribute. `Duration` is expressed in seconds so to set it to 24 hours we would use 86400(`24*60*60`).  To set the caching location to the browser we can use `Location` parameter of the same attribute and set it to `ResponseCacheLocation.Client`. This attribute can be used on either action or controller level (then caching policy is applied to all action in the decorated controller).

## 8. Why might you enable Razor Pages even if you are not creating any yourself?

Razor pages are used by different parts of the ASP.NET Core. For example Identity UI feature uses Razor pages.

## 9. How does ASP.NET Core MVC identify classes that can act as controllers?

By the fact that they are decorated with the `[Controller]` attribute letting ASP.NET Core MVC find them using reflection. 

## 10. In what ways does ASP.NET Core MVC make it easier to test a website?

It helps with separation of concerns (using models for data, controllers for logic, and views for presentation). This makes it easier to test given part of the application in separation while possibly mocking other parts.

MVC architecture also facilitates using patterns like Inversion of Control or Dependency injection which make testing easier.