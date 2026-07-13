using EcoMeal.Database;
using EcoMeal.Events;
using EcoMeal.Repositories.Interfaces;
using MediatR;

namespace EcoMeal.EventHandlers;

public class UserCreatedWalletHandler(EcoMealDbContext context, IWalletService walletService) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        Console.WriteLine("Wallet handler has been caught! GG!!!");

        /*
        var initialWallet = new Wallet
        {
            Id = Guid.NewGuid(),
            User = notification.User,
            Balance = 0.00m
        };
        
        context.Wallets.Add(initialWallet);
        */
        await walletService.CreateWallet(notification.User);

        await context.SaveChangesAsync(CancellationToken.None);
    }
}