using EcoMeal.Entities;
using MediatR;

namespace EcoMeal.Events;

public record UserCreatedEvent(ApplicationUser User) : INotification;
