using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessService
{
    public Task<List<Business>> GetAll(bool includeDeleted = false);

    public Task AddAsync(Business business);
    public Task<Business?> GetById(Guid id, bool includeDeleted = false);

    public Task UpdateAsync(Business business);

    public Task DeleteAsync(Business business);
    public Task RestoreAsync(Business business);
    public string GetAccronym(Business business);
    public string GenerateOrderName(Business business);
}

