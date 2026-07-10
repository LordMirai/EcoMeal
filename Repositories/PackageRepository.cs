using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class PackageRepository(EcoMealDbContext context): IPackageRepository
{
    public async Task<List<Package>> GetAllAsync()
    {
        return await context.Packages.ToListAsync();
    }

    public async Task AddAsync(Package package)
    {
        await context.Packages.AddAsync(package);
    }

    public async Task UpdateAsync(Package package)
    {
        // todo
    }

    public async Task<Package?> GetByIdAsync(Guid id)
    {
        return await context.Packages
            .Include(p => p.PackageType)
            .Include(p => p.Business)
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var package = await context.Packages.FindAsync(id);
        if (package is null) return;
        context.Packages.Remove(package);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    // types
    public async Task<List<PackageType>> GetTypes()
    {
        return await context.PackageTypes.ToListAsync();
    }

    public async Task AddType(string name)
    {
        var packageType = new PackageType
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        await context.PackageTypes.AddAsync(packageType);
    }

    public async Task<List<Package>> GetByBusinessIdAsync(Guid businessId)
    {
        return await context.Packages
        .Where(p => p.Business.Id == businessId)
        .ToListAsync();
    }
}
