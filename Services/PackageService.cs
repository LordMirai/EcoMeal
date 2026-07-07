using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;

namespace EcoMeal.Services;

public class PackageService(IPackageRepository packageRepository) : IPackageService
{
    public async Task<List<Package>> GetAll()
    {
        return await packageRepository.GetAllAsync();
    }

    public async Task AddAsync(Package package)
    {
        await packageRepository.AddAsync(package);
        await packageRepository.SaveChangesAsync();
    }

    public async Task<Package?> GetByIdAsync(Guid id)
    {
        return await packageRepository.GetByIdAsync(id);
    }

    public async Task UpdateAsync(Package package)
    {
        await packageRepository.UpdateAsync(package);
        await packageRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        await packageRepository.DeleteAsync(id);
        await packageRepository.SaveChangesAsync();
    }

    public async Task<Package?> GetByNameAsync(string name)
    {
        var packages = await packageRepository.GetAllAsync();
        return packages.FirstOrDefault(p => p.Name == name);
    }

    public async Task<List<Package>> GetByBusinessIdAsync(Guid businessId)
    {
        return await packageRepository.GetByBusinessIdAsync(businessId);
    }
}
