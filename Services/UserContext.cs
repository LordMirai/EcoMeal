using EcoMeal.Constants;
using EcoMeal.Database;
using EcoMeal.Entities; // Adjust to your namespace  
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace EcoMeal.Services;

public class UserContext(
    AuthenticationStateProvider authStateProvider,
    IDbContextFactory<EcoMealDbContext> contextFactory,
    IServiceScopeFactory scopeFactory)

{


    public async Task<ApplicationUser?> GetLocalUserAsync()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var userPrincipal = authState.User;

        if (userPrincipal.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        //var userId = userManager.GetUserId(userPrincipal);
        var userId = userPrincipal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        if (string.IsNullOrEmpty(userId))
        {
            return null;
        }

        //return await userManager.FindByIdAsync(userId);
        using var context = await contextFactory.CreateDbContextAsync();
        return await context.Set<ApplicationUser>().FindAsync(userId);
    }

    public async Task<IList<string>> GetLocalUserRolesAsync(ApplicationUser user)
    {
        //return await userManager.GetRolesAsync(user);

        using var scope = scopeFactory.CreateScope();
        var transientUserManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        return await transientUserManager.GetRolesAsync(user);
    }

    public async Task<string> GetUserRole(ApplicationUser user)
    {
        var roles = await GetLocalUserRolesAsync(user);
        return roles.FirstOrDefault(AppRoles.User);
    }

    public async Task<bool> IsAdmin(ApplicationUser user)
    {
        var roles = await GetLocalUserRolesAsync(user);
        return roles.Contains(AppRoles.Admin);
    }

    public async Task<bool> IsLoggedIn()
    {
        var authState = await authStateProvider.GetAuthenticationStateAsync();
        var userPrincipal = authState.User;
        return userPrincipal.Identity is { IsAuthenticated: true };
    }
}