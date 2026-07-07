using EcoMeal.Database;
using EcoMeal.Entities;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class UserRepository(EcoMealDbContext context)
{
    public async Task<List<ApplicationUser>> GetAllAsync()
    {
        return await context.Users.ToListAsync();
    }

    public async Task AddAsync(ApplicationUser user)
    {
        await context.Users.AddAsync(user);
    }

    public async Task UpdateAsync(ApplicationUser user)
    {
        // todo
    }

    public async Task<ApplicationUser?> GetByIdAsync(Guid id)
    {
        return await context.Users.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var user = await context.Users.FindAsync(id);
        if (user is null) return;
        context.Users.Remove(user);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
