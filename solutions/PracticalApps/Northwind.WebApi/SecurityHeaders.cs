using Microsoft.Extensions.Primitives;

namespace Northwind.WebApi;

public class SecurityHeaders
{
    private readonly RequestDelegate next;

    public SecurityHeaders(RequestDelegate next)
    {
        this.next = next;
    }

    public Task Invoke(HttpContext context)
    {
        // adding HTTP response headers 
        context.Response.Headers.Add("super-secure", new StringValues("enable"));
        return next(context);
    }
}