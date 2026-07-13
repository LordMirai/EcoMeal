using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IOrderService
{
    public Task<List<Order>> GetAllAsync();
    public Task AddAsync(Order order);
    public Task DeleteAsync(Order order);
    public Task UpdateAsync(Order order);
    public Task<Order?> GetByIdAsync(Guid id);
    public Task SaveChangesAsync();
    public Task<List<Order>> GetUserOrdersAsync(ApplicationUser user);
    public Task<List<Order>> GetPendingOrders(ApplicationUser user);

}
