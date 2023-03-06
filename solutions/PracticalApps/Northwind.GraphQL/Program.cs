var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5005/");

// Add services to the container.

builder.Services.AddControllers();

var app = builder.Build();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();