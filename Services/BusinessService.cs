using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;

namespace EcoMeal.Services;

public class BusinessService(IBusinessRepository businessRepository) : IBusinessService
{
    public async Task<List<Business>> GetAll()
    {
        return await businessRepository.GetAllAsync();
    }

    public async Task AddAsync(Business business)
    {
        await businessRepository.AddAsync(business);
        await businessRepository.SaveChangesAsync();
    }

    public async Task DeleteAsync(Business business)
    {
        await businessRepository.DeleteAsync(business.Id);
        await businessRepository.SaveChangesAsync();
    }

    public async Task<Business?> GetById(Guid id)
    {
        return await businessRepository.GetByIdAsync(id);
    }
}