using EcoMeal.Data;
using Microsoft.EntityFrameworkCore;
using EcoMeal.Models;

namespace EcoMeal.Services;

public class UserService: DBService
{
    public UserService(IDbContextFactory<AppDbContext> dbContextFactory) : base(dbContextFactory) {}

    public async Task <List<User>> GetUsers(bool includeDeleted = false)
    {
        using var context = _dbContextFactory.CreateDbContext();


        IQueryable<User> query = context.Users;
        if (!includeDeleted)
        {
            query = query.Where(u => !u.IsDeleted);
        }

        return await query.ToListAsync();
    }

    public bool IsAdmin(User user)
    {
        int roleID = user.RoleId;

        return (roleID == 2); // make it explicit, ideally.
    }
}
