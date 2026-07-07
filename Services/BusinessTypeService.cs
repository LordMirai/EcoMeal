using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Services;

public class BusinessTypeService(EcoMealDbContext context): IBusinessTypeService
{
    public async Task<List<BusinessType>> GetAllAsync()
    {
        return await context.BusinessTypes.ToListAsync();
    }

    public async Task AddAsync(BusinessType businessType)
    {
        await context.BusinessTypes.AddAsync(businessType);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(BusinessType businessType)
    {
        context.BusinessTypes.Remove(businessType);
        await context.SaveChangesAsync();
    }

    public async Task<BusinessType?> GetByIdAsync(Guid id)
    {
        return await context.BusinessTypes.FindAsync(id);
    }

    public async Task Create(string name)
    {
        var businessType = new BusinessType
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        await context.BusinessTypes.AddAsync(businessType);
        await context.SaveChangesAsync();
    }

    public async Task<BusinessType?> GetByNameAsync(string name)
    {
        return await context.BusinessTypes.FirstOrDefaultAsync(bt => bt.Name == name);
    }
}
