namespace CAO.Client.Dtos;

public class BlogStatsDto
{
    public int BlogsCount { get; set; }
    public int TagsCount { get; set; }
    public int MessagesCount { get; set; }
    public int MonthlyCreated { get; set; }
    public int MonthlyUpdated { get; set; }
    public string MostUsedTag { get; set; } = string.Empty;
    public int MostUsedTagCount { get; set; }
}