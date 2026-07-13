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
}