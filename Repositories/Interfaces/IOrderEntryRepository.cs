using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IOrderEntryRepository
{
    public Task<List<OrderEntry>> GetAllAsync();
    public Task AddAsync(OrderEntry orderEntry);
    public Task DeleteAsync(Guid id);
    public Task UpdateAsync(OrderEntry orderEntry);
    public Task<OrderEntry?> GetByIdAsync(Guid id);
    public Task SaveChangesAsync();
}
