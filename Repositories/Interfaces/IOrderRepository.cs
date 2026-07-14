using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IOrderRepository
{
    public Task<List<Order>> GetAllAsync();
    public Task AddAsync(Order order);
    public Task UpdateAsync(Order order);
    public Task DeleteAsync(Guid id);
    public Task<Order?> GetByIdAsync(Guid id);
    public Task SaveChangesAsync();
    public Task<List<Order>> GetUserOrders(ApplicationUser user);
    public Task<List<Order>> GetPendingOrders(ApplicationUser user, OrderStatus pendingStatus);
    public Task<List<Order>> GetPendingOrdersForBusiness(Guid userId, Business business, OrderStatus pendingStatus);
}
