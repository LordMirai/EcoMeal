using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace EcoMeal.Services;

public class AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : IAuthService
{
    public async Task<SignInResult> LoginAsync(LoginRequest loginRequest)
    {
        return await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, true, lockoutOnFailure: false);
    }
}
