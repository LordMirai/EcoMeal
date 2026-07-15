using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;

namespace EcoMeal.Services;

public class WalletService(IWalletRepository walletRepository) : IWalletService
{
    public async Task CreateWallet(Guid userId)
    {
        var initialWallet = new Wallet
        {
            Id = Guid.NewGuid(),
            UserId = userId.ToString(),
            Balance = 0.00m
        };

        Console.WriteLine($"Creating wallet for user '{userId}'.");

        await walletRepository.AddAsync(initialWallet);
        await walletRepository.SaveChangesAsync();

        await CreateTransactionAsync(initialWallet, 0.00m, "Account Creation");
    }

    public async Task EnsureWalletExists(Guid userId)
    {
        var existingWallet = await GetUserWallet(userId);
        if (existingWallet == null)
        {
            Console.WriteLine("\n\n'Ensure' found NO wallet for user.\n\n");
            await CreateWallet(userId);
        }
    }

    public async Task<Wallet?> GetUserWallet(Guid userId, bool expectAdmin = false)
    {
        Wallet? wallet = await walletRepository.GetWalletByUserId(userId, expectAdmin);
        Console.WriteLine($"\nService says hi. Wallet here is {wallet}");
        
        return wallet;
    }

    public async Task<Wallet?> GetWalletById(Guid walletId)
    {
        return await walletRepository.GetById(walletId);
    }

    public async Task<bool> ChangeBalanceAsync(Guid walletId, decimal amount, string description)
    {
        var wallet = await GetWalletById(walletId);
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

        await walletRepository.AddTransactionAsync(transaction);
        await walletRepository.SaveChangesAsync();
        return true;
    }

    public async Task<IEnumerable<WalletTransaction>> GetTransactionHistoryAsync(Guid walletId)
    {
        return await walletRepository.GetTransactions(walletId);
    }

    public async Task SaveChangesAsync()
    {
        await walletRepository.SaveChangesAsync();
    }

    public async Task<bool> UserCanAfford(Guid userId, decimal amount)
    {
        var wallet = await GetUserWallet(userId);
        if (wallet == null)
            return false;
        return wallet.Balance >= amount;
    }


    public async Task<WalletTransaction?> CreateTransactionAsync(Wallet wallet, decimal amount, string description)
    {
        if (wallet == null)
        {
            Console.WriteLine($"\nERR: Tried to create a transaction on a null wallet.");
            return null;
        }

        var transaction = new WalletTransaction
        {
            Id = Guid.NewGuid(),
            Wallet = wallet,
            Amount = amount,
            Timestamp = DateTime.UtcNow,
            Description = string.IsNullOrWhiteSpace(description) ? "Wallet Transaction" : description
        };

        Console.WriteLine($"Logging wallet transaction of {amount:C} for wallet '{wallet.Id}'.");

        await walletRepository.AddTransactionAsync(transaction);
        await walletRepository.SaveChangesAsync();

        return transaction;
    }
}