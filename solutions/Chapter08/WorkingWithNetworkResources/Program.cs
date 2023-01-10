using System.Net;

using static System.Console;

Write("Enter a valid web address: ");
string? url = ReadLine();
if (string.IsNullOrWhiteSpace(url))
{
    url = "https://stackoverflow.com/search?q=securestring";
}
Uri uri = new(url);
WriteLine($"URL: {url}");
WriteLine($"Scheme: {uri.Scheme}");
WriteLine($"Port: {uri.Port}");
WriteLine($"Host: {uri.Host}");
WriteLine($"Path: {uri.AbsolutePath}");
WriteLine($"Query: {uri.Query}");