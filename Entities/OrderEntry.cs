using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities;

public class OrderEntry
{
    public Guid Id { get; set; }

    [ForeignKey("OrderId")]
    public required Order Order { get; set; }

    [ForeignKey("PackageId")]
    public required Package Package { get; set; }

    public int Quantity { get; set; }
}
