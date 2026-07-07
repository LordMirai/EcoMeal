using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IBusinessService
{
    public Task<List<Business>> GetAll();

    public Task AddAsync(Business business);
    public Task<Business?> GetById(Guid id);
}

