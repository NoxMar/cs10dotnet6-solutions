using Microsoft.AspNetCore.Identity; // RoleManager, UserManager
using Microsoft.AspNetCore.Mvc; // Controller, IActionResult
namespace Northwind.Mvc.Controllers;

public class RolesController : Controller
{
  private const string AdminRole = "Administrators";
  private const string UserEmail = "test@example.com";
  private readonly RoleManager<IdentityRole> _roleManager;
  private readonly UserManager<IdentityUser> _userManager;
  private readonly ILogger<RolesController> _logger;
  public RolesController(RoleManager<IdentityRole> roleManager,
    UserManager<IdentityUser> userManager, ILogger<RolesController> logger)
  {
    _roleManager = roleManager;
    _userManager = userManager;
    _logger = logger;
  }
  public async Task<IActionResult> Index()
  {
    if (!(await _roleManager.RoleExistsAsync(AdminRole)))
    {
      await _roleManager.CreateAsync(new IdentityRole(AdminRole));
    }
    IdentityUser user = await _userManager.FindByEmailAsync(UserEmail);
    if (user == null)
    {
      user = new();
      user.UserName = UserEmail;
      user.Email = UserEmail;
      IdentityResult result = await _userManager.CreateAsync(
        user, "Pa$$w0rd");
      if (result.Succeeded)
      {
        _logger.LogInformation("User {UserName} created successfully.", user.UserName);
      }
      else
      { 
        foreach (IdentityError error in result.Errors)
        {
          _logger.LogError("Error occured while trying to create account:{Error}", error.Description);
        }
      }
    }
    if (!user.EmailConfirmed)
    {
      string token = await _userManager
        .GenerateEmailConfirmationTokenAsync(user);
      IdentityResult result = await _userManager
        .ConfirmEmailAsync(user, token);
      if (result.Succeeded)
      {
        _logger.LogInformation("User {UserName} email confirmed successfully.", user.UserName);
      }
      else
      {
        foreach (IdentityError error in result.Errors)
        {
          _logger.LogError("Error occured while trying to confirm email:{Error}", error.Description);
        }
      }
    }
    if (!(await _userManager.IsInRoleAsync(user, AdminRole)))
    {
      IdentityResult result = await _userManager
        .AddToRoleAsync(user, AdminRole);
      if (result.Succeeded)
      {
        _logger.LogInformation("User {UserName} added to {Role} successfully.", user.UserName, AdminRole);
      }
      else
      {
        foreach (IdentityError error in result.Errors)
        {
          _logger.LogError("Error occured while trying to add user to a role:{Error}", error.Description);
        }
      }
    }
    return Redirect("/");
  }
}