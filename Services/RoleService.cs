using EcoMeal.Data;
using EcoMeal.Models;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Services;

public class RoleService: DBService
{
    public RoleService(IDbContextFactory<AppDbContext> dbContextFactory) : base(dbContextFactory) { }
}
