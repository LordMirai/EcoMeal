using EcoMeal.Database;
using EcoMeal.Entities;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class OrderStatusRepository(EcoMealDbContext context)
{
    public async Task<List<OrderStatus>> GetStatusesAsync()
    {
        return await context.OrderStatuses.ToListAsync();
    }

    public async Task AddStatusAsync(string name)
    {
        var orderStatus = new OrderStatus
        {
            Name = name
        };
        await context.OrderStatuses.AddAsync(orderStatus);
    }

    public async Task<OrderStatus?> GetStatusByIdAsync(int id)
    {
        return await context.OrderStatuses.FindAsync(id);
    }

    public async Task DeleteStatusAsync(int id)
    {
        var orderStatus = await context.OrderStatuses.FindAsync(id);
        if (orderStatus is null) return;
        context.OrderStatuses.Remove(orderStatus);
    }

    public async Task DeleteAllStatusesAsync()
    {
        var orderStatuses = await context.OrderStatuses.ToListAsync();
        context.OrderStatuses.RemoveRange(orderStatuses);
        await SaveChangesAsync();
    }

    public async Task EnsureInitialOrderStatuses()
    {
        await AddStatusAsync("Pending");
        await AddStatusAsync("Reserved");
        await AddStatusAsync("In Progress");
        await AddStatusAsync("Completed");
        await AddStatusAsync("Cancelled");
        await AddStatusAsync("Terminated");
        await AddStatusAsync("Errored");
        await AddStatusAsync("Status Unknown");
        await SaveChangesAsync();
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }
}
