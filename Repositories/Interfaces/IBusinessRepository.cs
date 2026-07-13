using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessRepository
{
    public Task<List<Business>> GetAllAsync(bool includeDeleted = false);
    public Task AddAsync(Business business);
    public void UpdateAsync(Business business);
    public Task<Business?> GetByIdAsync(Guid id, bool includeDeleted = false);
    public Task DeleteAsync(Business business);
    public Task RestoreAsync(Business business);
    public Task SaveChangesAsync();

    // business types
    public Task<List<BusinessType>> GetTypesAsync();

    public Task<BusinessType> GetTypeAsync(string name);
}
