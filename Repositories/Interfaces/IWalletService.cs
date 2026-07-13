using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IWalletService
{
    Task CreateWallet(ApplicationUser user);
    Task<Wallet?> GetUserWallet(Guid userId);
    Task<Wallet?> GetWalletById(Guid walletId);
    Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description);
    Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId);
    Task SaveChangesAsync();
}
