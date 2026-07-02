using System;
using System.Collections.Generic;

namespace EcoMeal.Models;

public partial class Order
{
    public int Id { get; set; }

    public int UserId { get; set; }

    public int BusinessId { get; set; }

    public int StatusId { get; set; }

    public string? OrderNumber { get; set; }

    public bool IsDeleted { get; set; }

    public virtual Business Business { get; set; } = null!;

    public virtual ICollection<OrderEntry> OrderEntries { get; set; } = new List<OrderEntry>();

    public virtual OrderStatus Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
