using Bogus;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EcoMeal.Services;

public class BusinessService(IBusinessRepository businessRepository) : IBusinessService
{
    public async Task<List<Business>> GetAll(bool includeDeleted = false)
    {
        return await businessRepository.GetAllAsync(includeDeleted);
    }

    public async Task AddAsync(Business business)
    {
        await businessRepository.AddAsync(business);
        await businessRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Business business)
    {
        await businessRepository.DeleteAsync(business);
        await businessRepository.SaveChangesAsync();
    }

    public async Task RestoreAsync(Business business)
    {
        await businessRepository.RestoreAsync(business);
        await businessRepository.SaveChangesAsync();
    }

    public async Task<Business?> GetById(Guid id, bool includeDeleted = false)
    {
        return await businessRepository.GetByIdAsync(id, includeDeleted = false);
    }

    public async Task UpdateAsync(Business business)
    {
        // manual db update element by element
        var existingBusiness = await businessRepository.GetByIdAsync(business.Id);
        if (existingBusiness is null) return;

        existingBusiness.Name = business.Name;
        existingBusiness.Description = business.Description;
        existingBusiness.Address = business.Address;
        existingBusiness.ImageURL = business.ImageURL;
        
        await businessRepository.SaveChangesAsync();
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

    public string GenerateOrderName(Business business)
    {
        var faker = new Faker();

        var header = $"{GetAccronym(business)}-";
        var id = faker.Random.AlphaNumeric(6).ToUpper();
        return $"{header}{id}";
    }
}