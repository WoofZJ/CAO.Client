namespace CAO.Client.Dtos;

public class ViewStatsDto
{
    public int TotalPageViews { get; set; }
    public int MonthlyPageViews { get; set; }
    public int TotalWebsiteViews { get; set; }
    public int MonthlyWebsiteViews { get; set; }
    public int TotalUniqueVisitors { get; set; }
    public int MonthlyUniqueVisitors { get; set; }
    public bool IsNewVisitor { get; set; }
    public bool IsNewView { get; set; }
}