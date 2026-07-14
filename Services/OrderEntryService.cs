using EcoMeal.Repositories.Interfaces;
using EcoMeal.Entities;

namespace EcoMeal.Services;

public class OrderEntryService(IOrderEntryRepository orderEntryRepository) : IOrderEntryService
{
    public async Task<List<OrderEntry>> GetAllAsync()
    {
        return await orderEntryRepository.GetAllAsync();
    }

    public async Task<OrderEntry> GetById(Guid id)
    {
        return await orderEntryRepository.GetByIdAsync(id);
    }

    public async Task<List<OrderEntry>> GetByOrder(Guid orderId)
    {
        return await orderEntryRepository.GetByOrderAsync(orderId);
    }

    public async Task AddAsync(OrderEntry orderEntry)
    {
        await orderEntryRepository.AddAsync(orderEntry);
        await orderEntryRepository.SaveChangesAsync();
    }

    public async Task<OrderEntry> Create(Order order, Package package, int quantity)
    {
        var orderEntry = new OrderEntry
        {
            Id = Guid.NewGuid(),
            Order = order,
            Package = package,
            Quantity = quantity
        };
        await AddAsync(orderEntry);
        return orderEntry;
    }
}
