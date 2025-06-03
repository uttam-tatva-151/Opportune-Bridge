namespace Core.DTOs;

public class UserDTO
{
    public Guid UserId { get; set; }
    public string UserName { get; set;} = null!;
    public string UserEmail { get; set;} = null!;
    public string HashPassword { get; set; } = null!;
    public Guid RoleId { get; set; }
    public string RoleName { get; set; } = null!;
    public byte[]? ProfilePicture { get; set; }
    public DateTime? LastLoginAt { get; set; }
    public int FailedAttempts { get; set; }
    public DateTime? LockedUntil { get; set; }
}
