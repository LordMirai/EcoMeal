using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;

namespace EcoMeal.Services;

public class OrderService(IOrderRepository orderRepository, IOrderStatusRepository orderStatusRepository) : IOrderService
{
    public async Task<List<Order>> GetAllAsync()
    {
        return await orderRepository.GetAllAsync();
    }

    public async Task AddAsync(Order order)
    {
        await orderRepository.AddAsync(order);
    }

    public async Task DeleteAsync(Order order)
    {
        await orderRepository.DeleteAsync(order.Id);
    }

    public async Task UpdateAsync(Order order)
    {
        await orderRepository.UpdateAsync(order);
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await orderRepository.GetByIdAsync(id);
    }

    public async Task SaveChangesAsync()
    {
        await orderRepository.SaveChangesAsync();
    }

    public async Task<List<Order>> GetUserOrdersAsync(ApplicationUser user)
    {
        return await orderRepository.GetUserOrders(user);
    }

    public async Task<List<Order>> GetPendingOrders(ApplicationUser user)
    {
        var pending = await orderStatusRepository.GetStatusByNameAsync("Pending");

        return await orderRepository.GetPendingOrders(user, pending);
    }
}
