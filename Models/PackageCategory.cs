using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class PackageCategory
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public virtual ICollection<Package> Packages { get; set; } = new List<Package>();
}
