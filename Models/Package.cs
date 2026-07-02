using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class Package
{
    public int Id { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public int? BusinessId { get; set; }

    public double? Price { get; set; }

    public DateTime? StartDate { get; set; }

    public DateTime? ExpiryDate { get; set; }

    public int? Quantity { get; set; }

    public bool? IsDeleted { get; set; }
}
