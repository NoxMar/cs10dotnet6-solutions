using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

using static System.Console;
using static System.Environment;
using static System.IO.Path;

Book csharp10 = new(title:
    "C# 10 and .NET 6 - Modern Cross-platform Development")
{
    Author = "Mark J Price",
    PublishDate = new(year: 2021, month: 11, day: 9),
    Pages = 823,
    Created = DateTimeOffset.UtcNow,
};

JsonSerializerOptions options = new()
{
    IncludeFields = true,
    PropertyNameCaseInsensitive = true,
    WriteIndented = true,
    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
};
options.Converters.Add(new DateOnlyConverter());
options.Converters.Add(new DateOnlyNullableConverter());
string filePath = Combine(CurrentDirectory, "book.json");
using (Stream fileStream = File.Create(filePath))
{
    JsonSerializer.Serialize<Book>(
        utf8Json: fileStream, value: csharp10, options);
}
WriteLine("Written {0:N0} bytes of JSON to {1}",
    new FileInfo(filePath).Length,
    filePath);
WriteLine();
WriteLine(File.ReadAllText(filePath));
public class Book
{
    public Book(string title)
    {
        Title = title;
    }

    // properties
    public string Title { get; set; }
    public string? Author { get; set; }
    // fields
    [JsonInclude]
    public DateOnly PublishDate;
    [JsonInclude]
    public DateTimeOffset Created;
    public ushort Pages;
}

