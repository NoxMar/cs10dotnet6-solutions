using System.Net.Http.Json;
using Packt.Shared;

namespace Northwind.BlazorWasm.Client.Data;

public class NorthwindService : INorthwindService
{
    private const string ApiExceptionText = "Unexpected API response"; 
    private readonly HttpClient _http;

    public NorthwindService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Customer>> GetCustomersAsync()
        => await _http.GetFromJsonAsync<List<Customer>>("api/customers")
           ?? throw new InvalidOperationException(ApiExceptionText);

    public async Task<List<Customer>> GetCustomersAsync(string country)
        => await _http.GetFromJsonAsync<List<Customer>>($"api/customers/in{country}")
           ?? throw new InvalidOperationException(ApiExceptionText);

    public async Task<Customer> GetCustomerAsync(string id)
        => await _http.GetFromJsonAsync<Customer>($"api/cusotmers/{id}")
           ?? throw new InvalidOperationException(ApiExceptionText);

    public async Task<Customer> CreateCustomerAsync(Customer c)
    {
        var response = await _http.PostAsJsonAsync("api/customers", c);
        return await response.Content.ReadFromJsonAsync<Customer>() ??
               throw new InvalidOperationException(ApiExceptionText);
    }

    public async Task<Customer> UpdateCustomerAsync(Customer c)
    {
        var response = await _http.PutAsJsonAsync("api/customers", c);
        return await response.Content.ReadFromJsonAsync<Customer>() ??
               throw new InvalidOperationException(ApiExceptionText);
    }

    public async Task DeleteCustomerAsync(string id)
    {
        _ = await _http.DeleteAsync($"api/customers/{id}");
    }
}