using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("/")]
public class PackageController(IPackageService packageService) : ControllerBase
{
    public async Task<ActionResult<List<Package>>> GetAll()
    {
        return await packageService.GetAll();
    }

    public async Task<ActionResult> AddAsync(Package package)
    {
        await packageService.AddAsync(package);
        return Created();
    }

    public async Task<ActionResult<Package?>> GetByIdAsync(Guid id)
    {
        var package = await packageService.GetByIdAsync(id);
        if (package == null)
        {
            return NotFound();
        }
        return package;
    }

    public async Task<ActionResult<List<Package>>> GetByBusinessIdAsync(Guid businessId)
    {
        var packages = await packageService.GetByBusinessIdAsync(businessId);
        if (packages == null || packages.Count == 0)
        {
            return NotFound();
        }
        return packages;
    }

    public async Task<ActionResult> UpdateAsync(Package package)
    {
        await packageService.UpdateAsync(package);
        return NoContent();
    }
}
