using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using Northwind.Mvc.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Packt.Shared;
using Northwind.Common;

namespace Northwind.Mvc.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IHttpClientFactory _clientFactory;
    private readonly NorthwindContext _db;

    public HomeController(ILogger<HomeController> logger, NorthwindContext db, IHttpClientFactory clientFactory)
    {
        _logger = logger;
        _db = db;
        _clientFactory = clientFactory;
    }

    [ResponseCache(Duration = 10, Location = ResponseCacheLocation.Any)]
    public async Task<IActionResult> Index()
    {
        _logger.LogError("This is a serious error (not really!)");
        _logger.LogWarning("This is your first warning!");
        _logger.LogWarning("Second warning!");
        _logger.LogInformation("Im in in the Index method of the HomeController.");
        HomeIndexViewModel model = new(
            VisitorCount: new Random().Next(1, 1001),
            Categories: await _db.Categories.ToListAsync(),
            Products: await _db.Products.ToListAsync()
        );

        try
        {
            var client = _clientFactory.CreateClient("Minimal.WebApi");
            HttpRequestMessage request = new(HttpMethod.Get, "api/weather");
            var response = await client.SendAsync(request);
            ViewData["weather"] = await response.Content
                .ReadFromJsonAsync<WeatherForecast[]>();
        }
        catch (Exception ex)
        {
            _logger.LogWarning($"The Minimal.WebApi service is no longer responding. Exception: {ex.Message}");
            ViewData["weather"] = Enumerable.Empty<WeatherForecast>().ToArray();
        }
        return View(model);
    }

    [Route("private")]
    [Authorize(Roles="Administrators")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }

    public async Task<IActionResult> ProductDetail(int? id)
    {
        if (!id.HasValue)
        {
            return BadRequest("You must pass a product ID to the route, for example, /Home/ProductDetail/21");
        }

        var product  = await _db.Products.SingleOrDefaultAsync(p => p.ProductId == id);
        if (product is null)
        {
            return NotFound($"ProductId {id} not found.");
        }

        return View(product);
    }

    [HttpGet]
    public IActionResult ModelBinding()
    {
        return View(); // the page to submit the form
    }

    [HttpPost]
    public IActionResult ModelBinding(Thing thing)
    {
        HomeModelBindingViewModel model = new(
            thing,
            !ModelState.IsValid,
            ModelState.Values
                .SelectMany(state => state.Errors)
                .Select(e => e.ErrorMessage)
        );
        return View(model); // show the model bound thing
    }

    public IActionResult ProductsThatCostMoreThan([FromQuery] decimal? price)
    {
        if (!price.HasValue)
        {
            return BadRequest(
                "You must pass a product price in the query string, for example /Home/ProductsThatCostMoreThan?price=50");
        }

        var products = _db.Products
            .Include(p => p.Category)
            .Include(p => p.Supplier)
            .Where(p => p.UnitPrice > price);
        if (!products.Any())
        {
            return NotFound($"No products cost more than {price:C}");
        }
        ViewData["MaxPrice"] = price.Value.ToString("C");
        return View(products);
    }

    [Route("/category/{id:int}")]
    public async Task<IActionResult> Category([FromRoute] int id)
    {
        var category = await _db.Categories
            .Include(c => c.Products)
            .FirstOrDefaultAsync(c => c.CategoryId == id);
        if (category is null)
        {
            return NotFound($"No product could be found for id: {id}");
        }
        return View(category);
    }

    public async Task<IActionResult> Customers(string country)
    {
        string uri;
        if (string.IsNullOrEmpty(country))
        {
            ViewData["Title"] = "All Customers Worldwide";
            uri = "api/customers/";
        }
        else
        {
            ViewData["Title"] = $"Customers in {country}";
            uri = $"api/customers?country={country}";
        }

        HttpClient client = _clientFactory.CreateClient("Northwind.WebApi");
        HttpRequestMessage request = new(method: HttpMethod.Get, requestUri: uri);
        HttpResponseMessage response = await client.SendAsync(request);
        var model = await response.Content.ReadFromJsonAsync<IEnumerable<Customer>>();
        if (model is null)
        {
            throw new Exception();
        }

        return View(model);
    }
}