using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessRepository
{
    public Task<List<Business>> GetAllAsync();
    public Task AddAsync(Business business);
    public void UpdateAsync(Business business);
    public Task<Business?> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);
    public Task SaveChangesAsync();

    // business types
    public Task<List<BusinessType>> GetTypesAsync();

    public Task<BusinessType> GetTypeAsync(string name);
}
