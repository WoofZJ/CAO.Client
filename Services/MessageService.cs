using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.JSInterop;
using CAO.Client.Dtos;

namespace CAO.Client.Services;

public class MessageService(HttpClient httpClient, IJSRuntime jsRuntime)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IJSRuntime _jsRuntime = jsRuntime;

    public async Task<bool> SendMessageAsync(string nickname, string email, string message, int avatarId)
    {
        MessageDto messageDto = new()
        {
            Nickname = nickname,
            Email = email,
            Message = message,
            AvatarId = avatarId,
            VisitorId = await GetOrCreateVisitorId()
        };
        try
        {
            var response = await _httpClient.PutAsJsonAsync("message/put", messageDto);
            if (response.IsSuccessStatusCode)
            {
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                    "message_nickname", nickname);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                    "message_email", email);
                await _jsRuntime.InvokeVoidAsync("localStorage.setItem",
                    "message_avatar_id", avatarId.ToString());
                return true;
            }
            return false;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data from message/put: {ex.Message}");
        }
        return false;
    }

    public async Task<MessageDto> GetLocalMessage()
    {
        var nickname = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "message_nickname");
        var email = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "message_email");
        var avatarId = await _jsRuntime.InvokeAsync<string?>("localStorage.getItem", "message_avatar_id");

        return new MessageDto
        {
            Nickname = nickname ?? string.Empty,
            Email = email ?? string.Empty,
            AvatarId = int.TryParse(avatarId, out var id) ? id : 0
        };
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
}