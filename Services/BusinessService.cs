using EcoMeal.Data;
using EcoMeal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;

namespace EcoMeal.Services;

public class BusinessService: DBService
{
    public BusinessService(IDbContextFactory<AppDbContext> dbContextFactory) : base(dbContextFactory) { }

    public async Task<Business> GetBusinessById(int id, bool includeDeleted=false)
    {
        using var context = await _dbContextFactory.CreateDbContextAsync();

        IQueryable<Business> query = context.Businesses;
        if (!includeDeleted)
        {
            query = query.Where(u => !u.IsDeleted);
        }
        query = query.Where(u => u.Id == id);

        return await query.FirstAsync();
    }
}
