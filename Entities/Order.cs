using System.ComponentModel.DataAnnotations.Schema;
namespace EcoMeal.Entities;

public class Order
{
    public Guid Id { get; set; }
    public required string OrderNumber { get; set;}
    public required string UserId { get; set; }
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set;}

    [ForeignKey("StatusId")]
    public required OrderStatus Status { get; set; }
    [ForeignKey("BusinessId")]
    public required Business Business { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public bool IsDeleted { get; set; } = false;
}
