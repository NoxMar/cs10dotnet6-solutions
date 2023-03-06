using GraphQL;
using GraphQL.Types;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;

namespace Northwind.GraphQL;

public class NorthwindQuery : ObjectGraphType
{
    public NorthwindQuery(NorthwindContext db)
    {
        Field<ListGraphType<CategoryType>>(
            name: "categories",
            description: "A query type that returns a list of all categories.",
            resolve: _ => db.Categories.Include(c => c.Products)
        );

        Field<CategoryType>(
            name: "category",
            description: "A query type that returns a category using its Id.",
            arguments:  new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" }
                ),
            resolve: context =>
            {
                var category = db.Categories.Find(context.GetArgument<int>("categoryId"));
                db.Entry(category).Collection(c => c.Products).Load();
                return category;
            });

        Field<ListGraphType<ProductType>>(
            name: "products",
            arguments: new QueryArguments(new QueryArgument<IntGraphType> { Name = "categoryId" }),
            resolve: ctx =>
            {
                var category = db.Categories.Find(ctx.GetArgument<IntGraphType>("categoryId"));
                db.Entry(category).Collection(c => c.Products).Load();
                return category?.Products;
            }
        );
    }
}