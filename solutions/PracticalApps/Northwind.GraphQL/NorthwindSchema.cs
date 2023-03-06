using GraphQL.Types; // Schema

namespace Northwind.GraphQL;

public class NorthwindSchema : Schema
{
    public NorthwindSchema(IServiceProvider provider) : base(provider)
    {
        Query = new GreetQuery();
    }
}