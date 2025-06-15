namespace CAO.Client.Dtos;

public class BlogArchiveItemDto
{
    public string Slug { get; set; } = string.Empty;
    public string Title { get; set; } = string.Empty;
    public DateTime CreateAt { get; set; } = DateTime.UtcNow;
}