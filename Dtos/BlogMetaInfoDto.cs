namespace CAO.Client.Dtos;

public class BlogMetaInfoDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdateAt { get; set; } = DateTime.UtcNow;
    public List<string> Tags { get; set; } = [];
    public bool IsRecommended { get; set; } = false;
    public bool IsSticky { get; set; } = false;
}