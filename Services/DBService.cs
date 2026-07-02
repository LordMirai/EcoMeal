using Microsoft.EntityFrameworkCore;
using EcoMeal.Data;
using EcoMeal.Models;

namespace EcoMeal.Services;

public class DBService
{
    protected readonly IDbContextFactory<AppDbContext> _dbContextFactory;

    public DBService(IDbContextFactory<AppDbContext> dbContextFactory)
    {
        _dbContextFactory = dbContextFactory;
    }
}
