using EcoMeal.Entities;


namespace EcoMeal.Repositories.Interfaces;

public interface IPackageRepository
{
    public Task<List<Package>> GetAllAsync();
    public Task AddAsync(Package package);
    public Task UpdateAsync(Package package);
    public Task<Package?> GetByIdAsync(Guid id);
    public Task DeleteAsync(Guid id);
    public Task SaveChangesAsync();
    public Task<List<Package>> GetByBusinessIdAsync(Guid Id);
}
