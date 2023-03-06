using GraphQL.Server; // GraphQLOptions
using GraphQL.SystemTextJson;
using Northwind.GraphQL; // GreetQuery, NorthwindSchema


var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5005/");

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddScoped<NorthwindSchema>();

builder.Services.AddGraphQL()
        .AddGraphTypes(typeof(NorthwindSchema), ServiceLifetime.Scoped)
        .AddDataLoader()
        .AddSystemTextJson(); // serialize responses as JSON

var app = builder.Build();

if (builder.Environment.IsDevelopment())
{
        app.UseGraphQLPlayground();
}

app.UseGraphQL<NorthwindSchema>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();