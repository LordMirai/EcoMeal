using Bogus.DataSets;
using EcoMeal.Data;
using EcoMeal.Models;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

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

    public string GetAccronym(Business business)
    {
        string Name = business.Name;
        Name = Name.Trim();
        if (string.IsNullOrEmpty(Name)) return "[?]";

        string cleanName = Name.ToUpper();

        string[] noiseWords = { "AND", "INC", "CO", "LLC", "CORP" };
        foreach (string noise in noiseWords)
        {
            cleanName = Regex.Replace(cleanName, $@"\b{noise}\b", "", RegexOptions.IgnoreCase);
        }
        cleanName = Regex.Replace(cleanName, @"[^A-Z\s]", "");

        var accroChars = cleanName.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(word => word[0])
            .Take(4);

        return string.Concat(accroChars);
    }
}
