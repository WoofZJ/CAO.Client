namespace CAO.Client.Dtos;

public class MessageGetDto
{
    public string Nickname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Message { get; set; } = string.Empty;
    public int AvatarId { get; set; } = 0;
    public DateTime UploadAt { get; set; } = DateTime.UtcNow;
    public bool IsRead { get; set; } = false;
    public bool IsApproved { get; set; } = false;
    public bool IsSelf { get; set; } = false;
}