## Exercise 17.1 - Test your knowledge

## 1. What are the two primary hosting models for Blazor, and how are they different?

- server-side Balzor which runs all of the code server-side and sends just the rendered output to the browser (via SignalR). This means that the server needs to keep connection with each client. That impacts scalability quite significantly.

- Blazor WASM which runs component code client-side (in client's browser) using WASM. That means that each time app needs access to some resource on the server it needs to make HTTP request as it runs in the browser. This, however, eliminates the need for SingalR connection to all clients thus improving scalability. 

## 2. In a Blazor Server website project, compared to an ASP.NET Core MVC website project, what extra configuration is required in the Startup class?

`builder.Services.AddServerSideBlazor();` must be called when setting up services in the container.

Additionally`MapBlazorHub()` and `MapFallbackToPage()` must be called to add those components to the request pipeline.

## 3. One of the benefits of Blazor is being able to implement client-side components using C# and .NET instead of JavaScript. Does a Blazor component need any JavaScript?

Part of the Blazor technology are files in JavaScript provided by Microsoft. Additionally, if a developer wants to interact with browser's APIs since Blazor code can't interact directly with those.

## 4. In a Blazor project, what does the `App.razor` file do?

It configures a router for **all** of the components in the current assembly. This, for example, includes shared layout and error pages.

## 5. What is a benefit of using the `<NavLink>` component?

It automatically shows the selected page differently as compared to the normal `<a>` tags.

## 6. How can you pass a value into a component?

By annotating a public property in code block or code-behind file with `[Parameter]`. This exposes it externally as tag's attribute.

##  7. What is a benefit of using the `<EditForm>` component?

It enables additional functionality such as validation based on standard validation attributes.

## 8. How can you execute some statements when parameters are set?

By overriding the `OnParamteresSetAsync` method in your component.

## 9. How can you execute some statements when a component appears?

By overriding the `OnInitializedAsync` method in your component.

## 10. What are two key differences in the `Program` class between a Blazor Server and Blazor WebAssembly project?

1. Blazor WASM uses `WebAssemblyHostBuilder` instead of `Host.CreateDefaultBuilder`
2. Blazor WASM projects register `HttpClient` for the base address of the app by default (used for sending requests to the server from the client side code in the browser).