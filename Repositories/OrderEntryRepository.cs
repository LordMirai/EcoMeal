using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class OrderEntryRepository(EcoMealDbContext context): IOrderEntryRepository
{
    public async Task<List<OrderEntry>> GetAllAsync()
    {
        return await context.OrderEntries.ToListAsync();
    }

    public async Task AddAsync(OrderEntry orderEntry)
    {
        await context.OrderEntries.AddAsync(orderEntry);
    }

    public async Task UpdateAsync(OrderEntry orderEntry)
    {
        context.OrderEntries.Update(orderEntry);
    }

    public async Task<OrderEntry?> GetByIdAsync(Guid id)
    {
        return await context.OrderEntries.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var orderEntry = await context.OrderEntries.FindAsync(id);
        if (orderEntry is null) return;
        context.OrderEntries.Remove(orderEntry);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<List<OrderEntry>> GetByOrderAsync(Guid orderId)
    {
        return await context.OrderEntries
            .Include(oe => oe.Package)
            .Where(oe => oe.Order.Id == orderId)
            .ToListAsync();
    }
}
