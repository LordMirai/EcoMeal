using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class BusinessRepository(EcoMealDbContext context) : IBusinessRepository
{
    public async Task<List<Business>> GetAllAsync(bool includeDeleted = false)
    {
        var query = context.Businesses.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(b => !b.IsDeleted);
        }
        //return await context.Businesses.Include(b=>b.BusinessType).ToListAsync();
        return await query.Include(b=>b.BusinessType).ToListAsync();
    }

    public async Task AddAsync(Business business)
    {
        await context.Businesses.AddAsync(business);
    }

    public void UpdateAsync(Business business)
    {
        // todo
    }

    public async Task<Business?> GetByIdAsync(Guid id, bool includeDeleted = false)
    {
        var query = context.Businesses.AsQueryable();
        if (!includeDeleted)
        {
            query = query.Where(b => !b.IsDeleted);
        }
        return await query.Include(b => b.BusinessType).FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task DeleteAsync(Business business)
    {
        business.IsDeleted = true;
    }

    public async Task RestoreAsync(Business business)
    {
        business.IsDeleted = false;
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
