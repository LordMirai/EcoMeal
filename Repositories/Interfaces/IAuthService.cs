using EcoMeal.Controllers;
using Microsoft.AspNetCore.Identity;

namespace EcoMeal.Repositories.Interfaces;

public interface IAuthService
{
    public Task<SignInResult> LoginAsync(EcoLoginRequest loginRequest);
    public Task LogoutAsync();

    public Task<IdentityResult> RegisterAsync(EcoRegisterRequest registerRequest);
}