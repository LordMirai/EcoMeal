using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;

namespace EcoMeal.Services;

public class UtilityService
{
    private readonly IJSRuntime _jsRuntime;

    public string? AlertMessage { get; private set; }
    public string AlertType { get; private set; } = "success";
    //public bool HasAlert => !string.IsNullOrEmpty(AlertMessage);
    public bool AlertSingleUse = false;

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

    public void SetMessage(string message, string type="success", bool single = false)
    {
        AlertMessage = message;
        AlertType = type;
        AlertSingleUse = single;
    }

    public void ClearMessage()
    {
        AlertMessage = null;
        AlertType = "success";
    }


    public bool HasAlert()
    {
        return !AlertMessage.IsNullOrEmpty();
    }
}
