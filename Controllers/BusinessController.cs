using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("/")]
public class BusinessController(IBusinessService businessService, IBusinessTypeService businessTypeService) : ControllerBase
{
    public async Task<ActionResult<List<Business>>> GetAll(bool includeDeleted=false)
    {
        return await businessService.GetAll(includeDeleted);
    }

    public async Task<ActionResult<Business?>> GetById(Guid id, bool includeDeleted=false)
    {
        var business = await businessService.GetById(id, includeDeleted);
        if (business == null)
        {
            return NotFound();
        }
        return business;
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

    public async Task<ActionResult> AddAsync(Business business)
    {
        await businessService.AddAsync(business);
        return Created();
    }

    public async Task<ActionResult<BusinessType?>> GetTypeAsync(string type)
    {
        try
        {
            Guid id = Guid.Parse(type);
            return await businessTypeService.GetByIdAsync(id);
        }
        catch (FormatException)
        {
            return BadRequest();
        }
    }

    public async Task<ActionResult> UpdateAsync(Business business)
    {
        var existingBusiness = await businessService.GetById(business.Id);
        if (existingBusiness == null)
        {
            return NotFound();
        }
        await businessService.UpdateAsync(business);
        return NoContent();
    }

    public async Task<ActionResult> Delete(Guid id)
    {
        var existingBusiness = await businessService.GetById(id);
        if (existingBusiness == null)
        {
            return NotFound();
        }
        await businessService.DeleteAsync(existingBusiness);
        return Ok();
    }

    public async Task<ActionResult> Restore(Guid id)
    {
        var existingBusiness = await businessService.GetById(id);
        if (existingBusiness == null)
        {
            return NotFound();
        }

        await businessService.RestoreAsync(existingBusiness);
        return Ok();
    }
}
