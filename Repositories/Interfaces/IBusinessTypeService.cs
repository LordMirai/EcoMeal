using EcoMeal.Entities;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessTypeService
{
    public Task<List<BusinessType>> GetAllAsync();
    public Task AddAsync(BusinessType businessType);
    public Task DeleteAsync(BusinessType businessType);
    public Task<BusinessType?> GetByIdAsync(Guid id);
    public Task Create(string name);
    public Task<BusinessType?> GetByNameAsync(string name);
}
