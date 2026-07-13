using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace EcoMeal.Repositories;

public class WalletRepository(EcoMealDbContext context) : IWalletRepository
{
    public async Task AddAsync(Wallet wallet)
    {
        await context.Wallets.AddAsync(wallet);
    }

    public async Task SaveChangesAsync()
    {
        await context.SaveChangesAsync();
    }

    public async Task<Wallet?> GetById(Guid walletId)
    {
        return await context.Wallets
            .FirstOrDefaultAsync(w => w.Id == walletId && !w.IsDeleted);
    }

    public async Task<Wallet?> GetWalletByUserId(Guid userId)
    {
        var userIdString = userId.ToString();
        return await context.Wallets
            .FirstOrDefaultAsync(w => w.User.Id == userIdString && !w.IsDeleted);
    }

    public async Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description)
    {
        var wallet = await GetById(walletId);
        if (wallet == null)
            return false;

        if (amount < 0 && wallet.Balance < Math.Abs(amount))
            return false;

        wallet.Balance += amount;

        var transaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            Wallet = wallet,
            Amount = amount,
            Timestamp = DateTime.UtcNow,
            Description = description
        };

        await context.WalletTransactions.AddAsync(transaction);
        await context.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<WalletTransaction>> GetTransactions(Guid walletId)
    {
        return await context.WalletTransactions
            .Where(t => t.Wallet.Id == walletId && !t.Wallet.IsDeleted)
            .OrderByDescending(t => t.Timestamp)
            .ToListAsync();
    }

    public async Task<Wallet?> GetActiveUserWallet(Guid userId)
    {
        var userIdString = userId.ToString();
        return await context.Wallets
            .FirstOrDefaultAsync(w => w.User.Id == userIdString && !w.IsDeleted);
    }
}