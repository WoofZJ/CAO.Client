using System.Net.Http.Json;
using CAO.Client.Dtos;

namespace CAO.Client.Services;

public class BlogService(HttpClient httpClient)
{
    private readonly HttpClient _httpClient = httpClient;

    public async Task<BlogMetaInfoDto?> GetBlogMetaInfoAsync(string slug)
    {
        try
        {
            var response = await _httpClient.GetAsync($"blog/metainfo/{slug}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlogMetaInfoDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching blog meta info: {ex.Message}");
        }
        return null;
    }

    public async Task<BlogHtmlDto?> GetBlogHtmlAsync(string slug)
    {
        try
        {
            var response = await _httpClient.GetAsync($"blog/html/{slug}");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<BlogHtmlDto>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching blog HTML: {ex.Message}");
        }
        return null;
    }

    public async Task<List<BlogMetaInfoDto>?> GetRecommendedBlogsAsync()
    {
        try
        {
            var response = await _httpClient.GetAsync("blog/recommended");
            if (response.IsSuccessStatusCode)
            {
                return await response.Content.ReadFromJsonAsync<List<BlogMetaInfoDto>>();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching recommended blogs: {ex.Message}");
        }
        return null;
    }

}