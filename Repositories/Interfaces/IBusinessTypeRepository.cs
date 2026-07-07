using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessTypeRepository
{
    public Task<List<BusinessType>> GetAllAsync();
    public Task AddAsync(BusinessType businessType);
    public Task UpdateAsync(BusinessType businessType);
    public Task<BusinessType?> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);
    public Task SaveChangesAsync();
    public Task<BusinessType?> GetByNameAsync(string name);
}
