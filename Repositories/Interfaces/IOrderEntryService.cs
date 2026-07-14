using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IOrderEntryService
{
    public Task<List<OrderEntry>> GetAllAsync();
    public Task<OrderEntry> GetById(Guid id);
    public Task<List<OrderEntry>> GetByOrder(Guid orderId);
    public Task AddAsync(OrderEntry orderEntry);
    public Task<OrderEntry> Create(Order Order, Package package, int quantity);
}
