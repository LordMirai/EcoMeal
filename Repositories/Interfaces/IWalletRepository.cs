using EcoMeal.Entities;

namespace EcoMeal.Repositories.Interfaces;

public interface IWalletRepository
{
    public Task AddAsync(Wallet wallet);
    public Task SaveChangesAsync();
    public Task<Wallet?> GetWalletByUserId(Guid userId);
    public Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description);
    public Task<IEnumerable<WalletTransaction>> GetTransactions(Guid walletId);

    public Task<Wallet?> GetById(Guid walletId);
    public Task<Wallet?> GetActiveUserWallet(Guid userId);
}
