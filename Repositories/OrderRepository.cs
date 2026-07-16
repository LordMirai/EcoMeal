using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class OrderRepository(EcoMealDbContext context): IOrderRepository
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
        context.Orders.Update(order);
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

    public async Task<List<Order>> GetUserOrders(ApplicationUser user, bool includeDeleted = false)
    {
        return await context.Orders
            .Include(o => o.Business)
            .Include(o => o.Status)
            .Include(o => o.OrderEntries) // todo: look ts up later, wtf
            .ThenInclude(o => o.Package)
            .Where(o => o.User.Id == user.Id)
            .Where(o => includeDeleted || !o.IsDeleted)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingOrders(ApplicationUser user, OrderStatus pendingStatus)
    {
        return await context.Orders
            .Include(o => o.Business)
            .Where(o => o.User.Id == user.Id)
            .Where(o => o.Status.Id == pendingStatus.Id)
            .ToListAsync();
    }

    public async Task<List<Order>> GetPendingOrdersForBusiness(Guid userId, Business business, OrderStatus pendingStatus)
    {
        return await context.Orders.Where(o => o.User.Id == userId.ToString())
            .Where(o => o.Business.Id == business.Id)
            .Where(o => o.Status.Id == pendingStatus.Id)
            .Include(o => o.Business)
            .ToListAsync();
    }
}
