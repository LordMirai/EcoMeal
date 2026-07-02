using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int PackageId { get; set; }

    public int StatusId { get; set; }

    public string? OrderNumber { get; set; }

    public bool IsDeleted { get; set; }
}
