using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class BusinessType
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Business> Businesses { get; set; } = new List<Business>();
}
