using System.Net.Http.Json;
using CAO.Client.Dtos;
using Microsoft.JSInterop;

namespace CAO.Client.Services;

public class ViewService(HttpClient httpClient, IJSRuntime jsRuntime)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public async Task<ViewStatsDto?> PutViewStatsAsync()
    {
        var putViewStatsDto = new PutViewStatsDto
        {
            Origin = await _jsRuntime.InvokeAsync<string>("eval", "window.location.origin"),
            Pathname = await _jsRuntime.InvokeAsync<string>("eval", "window.location.pathname"),
            Search = await _jsRuntime.InvokeAsync<string>("eval", "window.location.search"),
            UserAgent = await _jsRuntime.InvokeAsync<string>("eval", "navigator.userAgent"),
            Referrer = await _jsRuntime.InvokeAsync<string>("eval", "document.referrer"),
            VisitorId = await GetOrCreateVisitorId(),
            SessionId = await GetOrCreateSessionId()
        };
        try
        {
            var response = await _httpClient.PutAsJsonAsync("view/stats", putViewStatsDto);
            if (response.IsSuccessStatusCode)
            {
                var viewStats = await response.Content.ReadFromJsonAsync<ViewStatsDto>();
                return viewStats;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error sending view stats: {ex.Message}");
        }
        return null;
    }

    private async Task<string> GetOrCreateVisitorId()
    {
        var visitorId = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "visitor_id");
        if (string.IsNullOrEmpty(visitorId))
        {
            visitorId = Guid.NewGuid().ToString();
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", "visitor_id", visitorId);
        }
        return visitorId;
    }

    private async Task<string> GetOrCreateSessionId()
    {
        var sessionId = await _jsRuntime.InvokeAsync<string?>("sessionStorage.getItem", "session_id");
        if (string.IsNullOrEmpty(sessionId))
        {
            sessionId = Guid.NewGuid().ToString();
            await _jsRuntime.InvokeVoidAsync("sessionStorage.setItem", "session_id", sessionId);
        }
        return sessionId;
    }
}