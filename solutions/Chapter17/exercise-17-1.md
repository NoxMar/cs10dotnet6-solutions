## Exercise 16.1 - Test your knowledge

## 1. Which class should you inherit from to create a controller class for an ASP.NET Core Web API service?

`ControlerBase` from `Microsoft.AspNetCore.Mvc`.

## 2. If you decorate your controller class with the `[ApiController]` attribute to get default behavior like automatic `400` responses for invalid models, what else must you do?

In versions older than .NET Core 3 compatibility version in the startup class by calling `SetCompatibilityVersion` on the MVC builder. In .NET Core 3+ it's a no-op. In .NET Core 6 it was even deprecated.

## 3. What must you do to specify which controller action method will be executed in response to an HTTP request?

Annotate action (controller public method of an appropriate return type) with `[Http<Method>]` where `<Method>` is an HTTP method. Optionally route can be provided if it isn't identical to controller's route.

## 4. What must you do to specify which controller action method will be executed in response to an HTTP request?

This is done using `[ProducesResponseType(int statusCode, [Type Type])]` attribute.

For example, to specify that action can return list of strings with status code `200`, you could use:

```cs
[ProducesReponseType(200, Type = typeof(IEnumerable<string>))] 
```

## 5. List three methods that can be called to return responses with different status codes.

- `Ok(content)` - response with status code `200` and content as passed by argument
- `NotFound([string error])` - response with status code `404` and optional message
- `NoContentResult()` - response with status code `204` and no content.

## 6. List four ways that you can test a web service.

- Using `cURL` or similar CLI tool
- Using GUI REST client like `Postman` or `Insomnia` (which can be IDE extensions like `REST Client` for VS Code)
- By using Swagger in your app and making requests using web GUI it exposes
- Making simpler (GET, with no content) requests with web browser.

##  7. Why should you not wrap your use of `HttpClient` in a `using` statement to dispose of it when you are finished even though it implements the `IDisposable` interface, and what should you use instead?

Since this would allocate and free excessive number of sockets unnecessarily. Additionally it would require configuring new clients during each creation. Using `HttpClientFactory` is encouraged to share common configuration and ensure efficient use of resources.  

## 8. What does the acronym CORS stand for and why is it important to enable it in a web service?

**C**ros-**O**rigin **R**esource **S**haring it's a way to specify from which origins (combinations of host and port) and which types of requests are allowed to a web service. This mechanism uses HTTP request headers to check if the request will be allowed (so-called preflight request). Servers responds with headers specifying it's policy on request from different origins.
If it isn't set browsers apply same origin policy by default which may not be appropriate for our usecase.

## 9. How can you enable clients to detect if your web service is healthy with ASP.NET Core 2.2 and later?

ASP.NET Core 2.2 adds health check api which enables exposing health checks as well as customizing requirements for service to be considered working properly (for example also checking database connection).

## 10. What benefits does endpoint routing provide?

It allows for more performant routing (meaning selection of appropriate controller and action). It also extracts routing from the MVC middleware enabling plugging other things into pipeline between route resolution and calling action.