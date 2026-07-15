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

    public async Task<Wallet?> GetWalletByUserId(Guid userId, bool includeDeleted = false)
    {
        var userIdString = userId.ToString();
        Console.WriteLine($"\n\nFrom Repo! Fetch wallet for user '{userIdString}' (expectAdmin={includeDeleted})\n\n");
        var query = context.Wallets.AsQueryable();
        query = query.Where(w => w.UserId == userIdString);
        if (!includeDeleted)
        {
            query = query.Where(w => !w.IsDeleted);
        }
        return await query.FirstOrDefaultAsync();
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
            .Where(w => w.UserId == userIdString && !w.IsDeleted)
            .FirstOrDefaultAsync();
    }


    public async Task AddTransactionAsync(WalletTransaction transaction)
    {
        await context.WalletTransactions.AddAsync(transaction);
    }
}