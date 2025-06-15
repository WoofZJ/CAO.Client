using System.Net.Http.Json;
using Microsoft.Extensions.Caching.Memory;
using CAO.Client.Dtos;

namespace CAO.Client.Services;

public class BlogService(HttpClient httpClient, IMemoryCache cache)
{
    private readonly HttpClient _httpClient = httpClient;
    private readonly IMemoryCache _cache = cache;

    private async Task<T?> GetWithCacheAsync<T>(string cacheKey, string apiEndpoint) where T : class
    {
        if (_cache.TryGetValue(cacheKey, out T? cachedResult))
        {
            return cachedResult;
        }
        try
        {
            var response = await _httpClient.GetAsync(apiEndpoint);
            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadFromJsonAsync<T>();
                if (result != null)
                {
                    _cache.Set(cacheKey, result, TimeSpan.FromHours(1));
                }
                return result;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error fetching data from {apiEndpoint}: {ex.Message}");
        }
        return null;
    }

    public Task<BlogMetaInfoDto?> GetBlogMetaInfoAsync(string slug) =>
        GetWithCacheAsync<BlogMetaInfoDto>($"blog-meta-{slug}", $"blog/metainfo/{slug}");

    public Task<BlogHtmlDto?> GetBlogHtmlAsync(string slug) =>
        GetWithCacheAsync<BlogHtmlDto>($"blog-html-{slug}", $"blog/html/{slug}");

    public Task<List<BlogMetaInfoDto>?> GetRecommendedBlogsAsync() =>
        GetWithCacheAsync<List<BlogMetaInfoDto>>("recommended-blogs", "blog/recommended");

    public Task<BlogStatsDto?> GetBlogStatsAsync() =>
        GetWithCacheAsync<BlogStatsDto>("blog-stats", "blog/stats");
}