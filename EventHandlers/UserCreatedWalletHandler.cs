using EcoMeal.Database;
using EcoMeal.Entities;
using EcoMeal.Events;
using MediatR;

namespace EcoMeal.EventHandlers;

public class UserCreatedWalletHandler : INotificationHandler<UserCreatedEvent>
{
    private readonly EcoMealDbContext context;

    public UserCreatedWalletHandler(EcoMealDbContext context)
    {
        this.context = context;
    }

    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        var initialWallet = new Wallet
        {
            Id = Guid.NewGuid(),
            User = notification.User,
            Balance = 0.00m
        };

        //context.Wallets.Add(initialWallet);
        await context.SaveChangesAsync(cancellationToken);
    }
}