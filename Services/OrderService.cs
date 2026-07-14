using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Bogus;

namespace EcoMeal.Services;

public class OrderService(IOrderRepository orderRepository, IOrderStatusRepository orderStatusRepository, IBusinessService businessService) : IOrderService
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
        var pending = await GetPendingStatus();

        return await orderRepository.GetPendingOrders(user, pending);
    }

    public async Task<List<Order>> GetPendingOrdersForBusiness(Guid userId, Business business)
    {
        var pending = await GetPendingStatus();

        return await orderRepository.GetPendingOrdersForBusiness(userId, business, pending);
    }

    public async Task<OrderStatus> GetPendingStatus()
    {
        return await orderStatusRepository.GetStatusByNameAsync("Pending");
    }
}
