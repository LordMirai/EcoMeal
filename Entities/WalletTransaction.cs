using System.ComponentModel.DataAnnotations.Schema;

namespace EcoMeal.Entities;

public class WalletTransaction
{
    public Guid Id { get; set; }

    [ForeignKey("WalletId")]
    public required Wallet Wallet { get; set; }
    public decimal Amount { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public string Description { get; set; } = "Wallet Transaction";
}
