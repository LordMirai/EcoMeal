using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class PackageTypeRepository(EcoMealDbContext context): IPackageTypeRepository
{
    public async Task<List<PackageType>> GetAllAsync()
    {
        return await context.PackageTypes.ToListAsync();
    }

    public async Task AddAsync(PackageType packageType)
    {
        await context.PackageTypes.AddAsync(packageType);
    }

    public async Task<PackageType?> GetByIdAsync(Guid id)
    {
        return await context.PackageTypes.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var packageType = await context.PackageTypes.FindAsync(id);
        if (packageType is null) return;
        context.PackageTypes.Remove(packageType);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<PackageType?> GetByNameAsync(string name)
    {
        return await context.PackageTypes.FirstOrDefaultAsync(pt => pt.Name == name);
    }

    public async Task Create(string name)
    {
        var packageType = new PackageType
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        await AddAsync(packageType);
        await SaveChangesAsync();
    }

    public async Task UpdateAsync(PackageType packageType)
    {
        // todo
    }
}
