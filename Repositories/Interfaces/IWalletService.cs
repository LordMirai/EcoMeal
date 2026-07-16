using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IWalletService
{
    public Task CreateWallet(Guid userId);
    public Task EnsureWalletExists(Guid userId);
    public Task<Wallet?> GetUserWallet(Guid userId, bool expectAdmin = false);
    public Task<Wallet?> GetWalletById(Guid walletId);
    public Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description);
    public Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId);
    public Task SaveChangesAsync();
    public Task<WalletTransaction?> CreateTransactionAsync(Wallet wallet, decimal amount, string description);
    public Task<bool> UserCanAfford(Guid userId, decimal amount);
    public Task<bool> CanAfford(Guid walletId, decimal amount);


}
