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
        CopyToClipboard(text);
    }

    public void CopyToClipboard(string text)
    {
        if (string.IsNullOrEmpty(text)) return;
        try
        {
            _jsRuntime.InvokeVoidAsync("navigator.clipboard.writeText", text);
        }
        catch (InvalidOperationException)
        {
            Console.WriteLine("Browser window not fully ready for clipboard operations.");
        }
    }
}
