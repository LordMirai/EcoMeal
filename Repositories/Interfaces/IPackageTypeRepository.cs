using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IPackageTypeRepository
{
    public Task<List<PackageType>> GetAllAsync();
    public Task AddAsync(PackageType packageType);
    public Task<PackageType?> GetByIdAsync(Guid id);
    public Task UpdateAsync(PackageType packageType);
    public Task DeleteAsync(Guid id);
    public Task SaveChangesAsync();
    public Task<PackageType?> GetByNameAsync(string name);

    public Task Create(string name);
}
