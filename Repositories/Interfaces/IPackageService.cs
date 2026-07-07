using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IPackageService
{
    public Task<List<Package>> GetAll();
    public Task AddAsync(Package package);
    public Task UpdateAsync(Package package);
    public Task<Package?> GetByIdAsync(Guid id);
    public Task<Package?> GetByNameAsync(string name);
    public Task<List<Package>> GetByBusinessIdAsync(Guid businessId);
}
