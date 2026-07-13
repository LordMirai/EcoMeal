using System.ComponentModel.DataAnnotations.Schema;
namespace EcoMeal.Entities;

public class Order
{
    public Guid Id { get; set; }
    public required string OrderNumber { get; set;}
    [ForeignKey("UserId")]
    public required ApplicationUser User { get; set;}

    [ForeignKey("StatusId")]
    public required OrderStatus Status { get; set; }
    public bool IsDeleted { get; set; } = false;
}
