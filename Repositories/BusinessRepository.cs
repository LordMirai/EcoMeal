using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class BusinessRepository(EcoMealDbContext context) : IBusinessRepository
{
    public async Task<List<Business>> GetAllAsync()
    {
        
        return await context.Businesses.Include(b=>b.BusinessType).ToListAsync();
    }

    public async Task AddAsync(Business business)
    {
        await context.Businesses.AddAsync(business);
    }

    public void UpdateAsync(Business business)
    {
        // todo
    }

    public async Task<Business?> GetByIdAsync(Guid id)
    {
        return await context.Businesses.FindAsync(id);
    }

    public async Task DeleteAsync(Guid id)
    {
        var business = await context.Businesses.FindAsync(id);
        if (business is null) return;
        context.Businesses.Remove(business);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    // types
    public async Task<List<BusinessType>> GetTypesAsync()
    {
        return await context.BusinessTypes.ToListAsync();
    }

    public async Task AddTypeAsync(string name)
    {
        var businessType = new BusinessType
        {
            Id = Guid.NewGuid(),
            Name = name
        };
        await context.BusinessTypes.AddAsync(businessType);
    }

    public async Task<BusinessType> GetTypeAsync(string name)
    {
        return await context.BusinessTypes.FirstOrDefaultAsync(bt => bt.Id == Guid.Parse(name));
    }
}
