using Microsoft.Extensions.DependencyInjection; // GetRequiredService
using Packt.Shared;
using GraphQL.Types; // Schema

namespace Northwind.GraphQL;

public class NorthwindSchema : Schema
{
    public NorthwindSchema(IServiceProvider provider) : base(provider)
    {
        Query = new NorthwindQuery(provider.GetRequiredService<NorthwindContext>());
    }
}