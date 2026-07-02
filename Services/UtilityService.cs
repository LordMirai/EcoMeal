using Microsoft.JSInterop;

namespace EcoMeal.Services;

public class UtilityService
{
    private readonly IJSRuntime _jsRuntime;

    public UtilityService(IJSRuntime jsRuntime)
    {
        _jsRuntime = jsRuntime;
    }

    public async Task CopyToClipboardAsync(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        try
        {
            await _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Browser window not fully ready for clipboard operations.");
        }
    }
}
