using NpgsqlTypes;

namespace Core.DTOs;

/// <summary>
/// DTO for the PostgreSQL composite type "refresh_token_result".
/// Represents the result of a refresh token query.
/// </summary>
[PgName("refresh_token_result")]
public class RefreshTokenResultDTO
{
    [PgName("id")]
    public Guid Id { get; set; }

    [PgName("user_id")]
    public Guid UserId { get; set; }

    [PgName("token")]
    public string Token { get; set; } = string.Empty;

    [PgName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [PgName("created_at")]
    public DateTime CreatedAt { get; set; }

    [PgName("created_by_ip")]
    public string CreatedByIp { get; set; } = string.Empty;

    [PgName("user_agent")]
    public string UserAgent { get; set; } = string.Empty;

    [PgName("device_id")]
    public string DeviceId { get; set; } = string.Empty;

    [PgName("is_revoked")]
    public bool IsRevoked { get; set; }

    [PgName("revoked_at")]
    public DateTime? RevokedAt { get; set; }

    [PgName("revoked_by_ip")]
    public string RevokedByIp { get; set; } = string.Empty;

    [PgName("is_active")]
    public bool IsActive { get; set; }
}

/// <summary>
/// DTO for the PostgreSQL composite type "refresh_token_input".
/// Represents the input for creating or updating a refresh token.
/// </summary>
[PgName("refresh_token_input")]
public class RefreshTokenInputDTO
{
    [PgName("user_id")]
    public Guid UserId { get; set; }

    [PgName("token")]
    public string Token { get; set; } = string.Empty;

    [PgName("expires_at")]
    public DateTime ExpiresAt { get; set; }

    [PgName("created_by_ip")]
    public string CreatedByIp { get; set; } = string.Empty;

    [PgName("user_agent")]
    public string UserAgent { get; set; } = string.Empty;

    [PgName("device_id")]
    public string DeviceId { get; set; } = string.Empty;
}