namespace CAO.Client.Dtos;

public class BlogMetaInfoDto
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string ImageUrl { get; set; } = string.Empty;
    public DateTime PublishAt { get; set; }
    public DateTime UpdateAt { get; set; }
    public IEnumerable<string> Tags { get; set; } = [];
    public bool IsRecommended;
    public bool IsSticky;
}