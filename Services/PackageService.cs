using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;

namespace EcoMeal.Services;

public class PackageService(IPackageRepository packageRepository, IPackageTypeRepository packageTypeRepository) : IPackageService
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
        var existing = await packageRepository.GetByIdAsync(package.Id);
        if (existing is null) return;

        existing.Name = package.Name;
        existing.Description = package.Description;
        existing.Price = package.Price;
        existing.Quantity = package.Quantity;
        existing.ExpiryDate = package.ExpiryDate;
        existing.ImageURL = package.ImageURL;
        existing.Business = package.Business;
        existing.PackageType = package.PackageType;
        existing.PickupTime = package.PickupTime;

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

    public async Task<List<PackageType>> GetTypes()
    {
        return await packageTypeRepository.GetAllAsync();
    }

    public async Task<bool> DecreaseQuantity(Guid id, int quantity)
    {
        var package = await packageRepository.GetByIdAsync(id);
        if (package is null) return false;
        if (package.Quantity < quantity)
            return false;
        package.Quantity -= quantity;
        return true;
    }

    public async Task<bool> UpdatePackageQuantities(List<OrderEntry> orderEntries)
    {
        foreach (var entry in orderEntries)
        {
            bool result = await DecreaseQuantity(entry.Package.Id, entry.Quantity);
            if (!result)
                return false;
        }
        await packageRepository.SaveChangesAsync();
        return true;
    }
}
