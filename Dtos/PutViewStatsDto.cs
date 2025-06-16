namespace CAO.Client.Dtos;

public class PutViewStatsDto
{
    public string Origin { get; set; } = string.Empty;
    public string Pathname { get; set; } = string.Empty;
    public string Search { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public string Referrer { get; set; } = string.Empty;
    public string VisitorId { get; set; } = string.Empty;
    public string SessionId { get; set; } = string.Empty;
}