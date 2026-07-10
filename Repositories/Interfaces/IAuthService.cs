using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.Data;

namespace EcoMeal.Repositories.Interfaces;

public interface IAuthService
{
    public Task<SignInResult> LoginAsync(LoginRequest loginRequest);
}