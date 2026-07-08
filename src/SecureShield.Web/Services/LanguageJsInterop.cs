using Microsoft.JSInterop;

namespace SecureShield.Web.Services;

/// <summary>
/// JS interop for persisting the language preference in localStorage.
/// </summary>
public class LanguageJsInterop
{
    private readonly IJSRuntime _js;
    public LanguageJsInterop(IJSRuntime js) => _js = js;

    public async Task<string?> GetSavedLangAsync()
    {
        try { return await _js.InvokeAsync<string>("localStorage.getItem", "uss-lang"); }
        catch { return null; }
    }

    public async Task SaveLangAsync(string lang)
    {
        try { await _js.InvokeVoidAsync("localStorage.setItem", "uss-lang", lang); }
        catch { }
    }
}
