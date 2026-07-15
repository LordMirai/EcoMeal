using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities;

public class Wallet
{
    public Guid Id { get; set; }
    public required string UserId { get; set; } = string.Empty;
    [ForeignKey(nameof(UserId))]
    public ApplicationUser User { get; set; }
    public decimal Balance { get; set; } = 0;
    public bool IsDeleted { get; set; } = false;
}
