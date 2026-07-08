using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("business")]
public class BusinessController(IBusinessService businessService, IBusinessTypeService businessTypeService) : ControllerBase
{
    public async Task<ActionResult<List<Business>>> GetAll()
    {
        return await businessService.GetAll();
    }

    public async Task<ActionResult<Business?>> GetById(Guid id)
    {
        var business = await businessService.GetById(id);
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
}
