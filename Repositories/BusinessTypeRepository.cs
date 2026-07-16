using EcoMeal.Database;
using EcoMeal.Repositories.Interfaces;
using EcoMeal.Entities;

using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class BusinessTypeRepository(EcoMealDbContext context): IBusinessTypeRepository
{
    public async Task<List<BusinessType>> GetAllAsync()
    {
        return await context.BusinessTypes.ToListAsync();
    }

    public async Task AddAsync(BusinessType businessType)
    {
        await context.BusinessTypes.AddAsync(businessType);
    }

    public async Task UpdateAsync(BusinessType businessType)
    {
        context.BusinessTypes.Update(businessType);
    }

    public async Task<BusinessType?> GetByIdAsync(Guid id)
    {
        return await context.BusinessTypes.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var businessType = await context.BusinessTypes.FindAsync(id);
        if (businessType is null) return;
        context.BusinessTypes.Remove(businessType);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<BusinessType?> GetByNameAsync(string name)
    {
        return await context.BusinessTypes.FirstOrDefaultAsync(bt => bt.Name == name);
    }
}
