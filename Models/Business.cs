using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace EcoMeal.Models;

public partial class Business
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public TimeOnly? OpenFrom { get; set; }

    public TimeOnly? OpenUntil { get; set; }

    public int? OwnerId { get; set; }

    public int? BusinessTypeId { get; set; }

    public string? Description { get; set; }

    public string? Address { get; set; }

    public string? ImageUrl { get; set; }

    public bool IsDeleted { get; set; }

    public virtual BusinessType? BusinessType { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();

    // helper func. Need to find a better way to do it because EFC nukes it every fucking time.
    public string GetAccronym()
    {
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
