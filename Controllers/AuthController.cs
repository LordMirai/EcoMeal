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


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] EcoRegisterRequest request, [FromQuery] string? returnUrl)
    {

        var result = await authService.RegisterAsync(request);

        if (result.Succeeded)
        {
            return LocalRedirect("/");
        }

        var firstError = result.Errors.FirstOrDefault()?.Description ?? "Registration failed";
        var encodedError = Uri.EscapeDataString(firstError);



        return LocalRedirect($"/register?error={encodedError}&returnUrl={Uri.EscapeDataString(returnUrl ?? "/")}");
    }
}

public class EcoLoginRequest {
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool Remember { get; set; } = false;
}

public class EcoRegisterRequest
{
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
    public string FullName { get; set; } = string.Empty;
}