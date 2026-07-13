using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IOrderStatusRepository
{
    public Task<List<OrderStatus>> GetStatusesAsync();
    public Task AddStatusAsync(string name);
    public Task<OrderStatus?> GetStatusByIdAsync(int id);
    public Task<OrderStatus?> GetStatusByNameAsync(string name);
    public Task DeleteStatusAsync(int id);
    public Task DeleteAllStatusesAsync();
    public Task EnsureInitialOrderStatuses();
    public Task SaveChangesAsync();
}
