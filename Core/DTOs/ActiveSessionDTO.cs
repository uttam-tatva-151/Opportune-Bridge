namespace Core.DTOs;

public class ActiveSessionDTO
{
    public Guid RefreshTokenId { get; set; }
    public string DeviceId { get; set; } = string.Empty;
    public string UserAgent { get; set; } = string.Empty;
    public DateTime CreatedAt { get; set; }
    public string CreatedByIp { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
}
