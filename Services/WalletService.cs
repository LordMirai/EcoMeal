using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;

namespace EcoMeal.Services;

public class WalletService(IWalletRepository walletRepository) : IWalletService
{
    public async Task CreateWallet(ApplicationUser user)
    {
        if (user == null)
            return;

        var initialWallet = new Wallet
        {
            Id = Guid.NewGuid(),
            User = user,
            Balance = 0.00m
        };

        await walletRepository.AddAsync(initialWallet);
        await walletRepository.SaveChangesAsync();
    }

    public async Task<Wallet?> GetUserWallet(Guid userId)
    {
        return await walletRepository.GetWalletByUserId(userId);
    }

    public async Task<Wallet?> GetWalletById(Guid walletId)
    {
        return await walletRepository.GetById(walletId);
    }

    public async Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description)
    {
        return await walletRepository.ChangeBalanceAsync(walletId, amount, description);
    }

    public async Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId)
    {
        return await walletRepository.GetTransactions(walletId);
    }

    public async Task SaveChangesAsync()
    {
        await walletRepository.SaveChangesAsync();
    }

    public async Task<bool> UserCanAfford(ApplicationUser user, decimal amount)
    {
        Guid id;
        try
        {
            id = Guid.Parse(user.Id);
        }
        catch (FormatException)
        {
            return false;
        }

        var wallet = await GetUserWallet(id);
        if (wallet == null)
            return false;
        return wallet.Balance >= amount;
    }
}