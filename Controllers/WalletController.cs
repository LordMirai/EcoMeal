using EcoMeal.Entities;
using EcoMeal.Repositories.Interfaces;
using EcoMeal.Services;
using Microsoft.AspNetCore.Mvc;

namespace EcoMeal.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WalletController(IWalletService walletService) : ControllerBase
{
    [HttpGet("user/{userId}")]
    public async Task<ActionResult<Wallet>> GetUserWallet(Guid userId)
    {
        await walletService.EnsureWalletExists(userId);

        var wallet = await walletService.GetUserWallet(userId);
        Console.WriteLine($"\nController says hi. Wallet here is null? {wallet is null}");
        if (wallet == null) // if this happens, something, somewhere, fucked up bad. <- ha, guess what, it did.
        {
            return NotFound("Wallet could not be created or retrieved.");
        }

        return wallet;
    }

    [HttpPost("adjust-balance")]
    public async Task<ActionResult<bool>> AdjustBalance([FromQuery] Guid walletId, [FromQuery] decimal amount, [FromQuery] string description)
    {
        var success = await walletService.ChangeBalanceAsync(walletId, amount, description);
        if (!success)
        {
            return BadRequest("Could not adjust wallet balance.");
        }
        return true;
    }
}