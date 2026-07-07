using EcoMeal.Database;
using EcoMeal.Entities;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class OrderRepository(EcoMealDbContext context)
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await context.Orders.ToListAsync();
    }

    public async Task AddAsync(Order order)
    {
        await context.Orders.AddAsync(order);
    }

    public async Task UpdateAsync(Order order)
    {
        // todo
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await context.Orders.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var order = await context.Orders.FindAsync(id);

        if (order is null) return;

        context.Orders.Remove(order);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
