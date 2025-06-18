namespace CAO.Client.Dtos;

public class MessageDto
{
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int AvatarId { get; set; } = 0;
    public string VisitorId { get; set; } = string.Empty;
}