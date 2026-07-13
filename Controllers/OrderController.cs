using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace EcoMeal.Controllers;

[ApiController]
[Route("/")]
public class OrderController(IOrderService orderService) : ControllerBase
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

}
