using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService): ControllerBase
{
    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] EcoLoginRequest request, [FromQuery] string? returnUrl)
    {
        Console.WriteLine($"Login attempt for {request.Email}, password {request.Password}, remember {request.Remember}, returnUrl {returnUrl}");

        var result = await authService.LoginAsync(request);

        if (result.Succeeded)
        {
            return LocalRedirect(returnUrl ?? "/");
        }

        var errorMessage = Uri.EscapeDataString("Invalid login credentials");
        var encodedReturn = Uri.EscapeDataString(returnUrl ?? "/");
        return LocalRedirect($"/login?error={errorMessage}&returnUrl={encodedReturn}");
    }

    [HttpGet("logout")]
    public async Task<IActionResult> Logout()
    {
        await authService.LogoutAsync();
        return LocalRedirect("/");
    }
}

public class EcoLoginRequest {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Remember { get; set; } = false;
}