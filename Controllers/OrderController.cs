using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("/")]
public class OrderController(IOrderService orderService, IBusinessService businessService, IOrderEntryService orderEntryService) : ControllerBase
{
    public async Task<ActionResult<List<Order>>> GetAll()
    {
        var orders = await orderService.GetAllAsync();
        return orders;
    }

    public async Task<ActionResult<List<Order>>> GetUserOrders(ApplicationUser user)
    {
        return await orderService.GetUserOrdersAsync(user);
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
         * 
         * TODO: Do not permit to add to cart more than the available quantity of a package. 
         * (so while the av qnt is 15, you shouldn't be able to add 10 to cart and then 10 more, for example)
         */

        if (package is null || (quantity <= 0))
        {
            return BadRequest("Invalid package or quantity.");
        }

        var pendingRelevantOrders = await orderService.GetPendingOrdersForBusiness(userId, package.Business);
        Console.WriteLine($"Pending orders for business {package.Business.Name}: {pendingRelevantOrders.Count}");

        Order activeOrder;
        if (pendingRelevantOrders.Count == 0)
        {
            // initialize the order
            activeOrder = await CreateOrder(userId, package.Business);
        }
        else
        {
            activeOrder = pendingRelevantOrders.First();
            Console.WriteLine("Found existing pending order, adding to it.");
        }
        var newEntry = orderEntryService.Create(activeOrder, package, quantity);

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

        Console.WriteLine($"Creating order with Order Number: {order.OrderNumber}, for: {business.Name}");

        await AddAsync(order);
        return order;
    }

    public async Task AddAsync(Order order)
    {
        await orderService.AddAsync(order);
        await orderService.SaveChangesAsync();
    }

    public async Task CancelOrder(Guid orderId, ApplicationUser requestor)
    {
        Order order = await orderService.GetByIdAsync(orderId);
        if (order == null) return;
        if (order.UserId != requestor.Id) return;

        await orderService.DeleteAsync(order);
        await orderService.SaveChangesAsync();
    }

    public async Task SubmitOrder(Guid orderId, ApplicationUser requestor)
    {
        Order order = await orderService.GetByIdAsync(orderId);
        if (order == null) return;
        if (order.UserId != requestor.Id) return;

        await orderService.SetInProgress(order);
    }

    public async Task<ActionResult<List<OrderEntry>>> GetOrderEntries(Guid orderId)
    {
        var order = await orderService.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }
        
        //var entries = await orderEntryService.GetByOrder(orderId);
        var entries = order.OrderEntries;
        return entries;
    }

    public async Task<ActionResult<decimal>> CalculatePrice(Guid orderId)
    {
        var order = await orderService.GetByIdAsync(orderId);
        if (order == null)
        {
            return NotFound();
        }

        return await orderService.CalculatePrice(order);
    }

    public async Task<List<OrderStatus>> GetStatuses()
    {
        return await orderService.GetStatusesAsync();
    }

    public async Task UpdateStatus(Guid orderId, Guid statusId)
    {
        var order = await orderService.GetByIdAsync(orderId);
        if (order == null)
            return;

        var status = await orderService.GetStatusByNameAsync(statusId.ToString());
        if (status == null)
            return;

        order.Status = status;
        await orderService.UpdateAsync(order);
    }
}
