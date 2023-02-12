var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.UseHttpsRedirection();

app.MapGet("/", () => "Hello World!");

if (!app.Environment.IsDevelopment())
{
    app.UseHsts();
}

app.Run();
Console.WriteLine("This executes after the web server has stopped!");