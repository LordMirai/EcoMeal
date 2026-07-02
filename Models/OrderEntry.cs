using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class OrderEntry
{
    public int Id { get; set; }

    public int PackageId { get; set; }

    public int OrderId { get; set; }

    public int Quantity { get; set; }

    public bool IsDeleted { get; set; }
}
