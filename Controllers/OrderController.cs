using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("/")]
public class OrderController(IOrderService orderService, IBusinessService businessService) : ControllerBase
{
    public async Task<ActionResult<List<Order>>> GetAll()
    {
        var orders = await orderService.GetAllAsync();
        return orders;
    }

    public async Task<ActionResult<List<Order>>> GetUserOrders(ApplicationUser user)
    {
        var orders = await orderService.GetUserOrdersAsync(user);
        return orders;
    }

    public async Task<ActionResult<List<Order>>> GetPendingOrders(ApplicationUser user)
    {
        var orders = await orderService.GetPendingOrders(user);
        return orders;
    }

    public async Task<ActionResult<Order>> GetById(Guid id)
    {
        var order = await orderService.GetByIdAsync(id);
        if (order == null)
        {
            return NotFound();
        }
        return order;
    }

    public async Task<ActionResult> AddToCart(Guid userId, Package package, int quantity)
    {
        /*
         * Ok so this is gonna be my bane.
         * "Adding to cart" implies having a 'cart' to add to (in this case, it's an order with a status of 'pending').
         * The first item to be added to the cart will create a new order with a status of 'pending'.
         * Said order will bear its identifier *as dictated by the business* and its accronym. we'll have it generic this time.
         * deleting all items from the cart will delete the order and its items.
         * if the order is accepted and sent, deleting it will be *soft* delete.
         */

        if (package is null || (quantity <= 0))
        {
            return BadRequest("Invalid package or quantity.");
        }

        var pendingRelevantOrders = await orderService.GetPendingOrdersForBusiness(userId, package.Business);
        Console.WriteLine($"Pending orders for business {package.Business.Name}: {pendingRelevantOrders.Count}");

        var activeOrder = pendingRelevantOrders.First();
        if (activeOrder == null)
        {
            // initialize the order
            activeOrder = await CreateOrder(userId, package.Business);
        }

        return Ok();
    }

    public async Task<Order> CreateOrder(Guid userId, Business business)
    {
        var uuid = Guid.NewGuid();
        var pending = await orderService.GetPendingStatus();
        string orderNumber = businessService.GenerateOrderName(business);
        
        var order = new Order
        {
            Id = uuid,
            UserId = userId.ToString(),
            Business = business,
            Status = pending,
            OrderNumber = orderNumber,
            CreatedAt = DateTime.UtcNow
        };

        await AddAsync(order);
        return order;
    }

    public async Task AddAsync(Order order)
    {
        await orderService.AddAsync(order);
        await orderService.SaveChangesAsync();
    }
}
