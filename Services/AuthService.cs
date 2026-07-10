using EcoMeal.Controllers;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace EcoMeal.Services;

public class AuthService(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager) : IAuthService
{
    public async Task<SignInResult> LoginAsync(EcoLoginRequest loginRequest)
    {
        return await signInManager.PasswordSignInAsync(loginRequest.Email, loginRequest.Password, isPersistent: loginRequest.Remember, lockoutOnFailure: false);
    }

    public async Task LogoutAsync()
    {
        await signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> RegisterAsync(EcoRegisterRequest registerRequest)
    {

        if (registerRequest.Password != registerRequest.ConfirmPassword)
        {
            return IdentityResult.Failed(new IdentityError { 
                Code = "PasswordMismatch",
                Description = "Passwords do not match." 
            });
        }

        var existingUser = await userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser != null)
        {
            return IdentityResult.Failed(new IdentityError
            {
                Code = "EmailAlreadyExists",
                Description = "A user with this email already exists."
            });
        }

        var newUser = new ApplicationUser
        {
            UserName = registerRequest.Email,
            Email = registerRequest.Email,
            FullName = registerRequest.FullName
        };

        var result = await userManager.CreateAsync(newUser, registerRequest.Password);
        if (result.Succeeded)
        {
            await signInManager.SignInAsync(newUser, isPersistent: false);
        }

        return result;
    }
}
