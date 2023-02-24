using Microsoft.AspNetCore.Mvc.Formatters;
using Northwind.WebApi.Repositories;
using Packt.Shared; // AddNorthwindContext extension method
using Swashbuckle.AspNetCore.SwaggerUI; // SubmitMethod
using Microsoft.AspNetCore.HttpLogging; // HttpLoggingFields
using Northwind.WebApi; // SecurityHeaders
using static System.Console;

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5002/");
// Add services to the container.
builder.Services.AddNorthwindContext();
builder.Services.AddControllers(options =>
    {
        foreach (var formatter in options.OutputFormatters)
        {
            var mediaFormatter = formatter as OutputFormatter;
            if (mediaFormatter is null)
            {
                WriteLine($"\t{formatter.GetType().Name}");
            }
            else // OutputFormatter class has SupportedMediaTypes
            {
                WriteLine("\t{0}, Media types: {1}",
                    arg0: mediaFormatter.GetType().Name,
                    arg1: string.Join(", ", mediaFormatter.SupportedMediaTypes));
            }
        }
    })
    .AddXmlDataContractSerializerFormatters()
    .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddHealthChecks()
    .AddDbContextCheck<NorthwindContext>();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "Northwind Service API", Version = "v1" });
});
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddHttpLogging(options =>
{
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; // default is 32k
    options.ResponseBodyLogLimit = 4096; // default is 32k
});

var app = builder.Build();

app.UseHttpLogging();

app.UseHealthChecks(path: "/howdoyoufeel");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Northwind Service API Version 1");
        c.SupportedSubmitMethods(SubmitMethod.Get, SubmitMethod.Post, SubmitMethod.Put, SubmitMethod.Delete);
    });
}


app.UseCors(configurePolicy: options =>
{
    options.WithMethods("GET", "POST", "PUT", "DELETE");
    options.WithOrigins("https://localhost:5001");
});
app.UseMiddleware<SecurityHeaders>();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();