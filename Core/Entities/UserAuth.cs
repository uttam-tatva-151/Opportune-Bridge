using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Stores sensitive authentication information, tokens, MFA, and session data for users.
/// </summary>
[Table("UserAuth")]
[Index("Email", Name = "UserAuth_email_key", IsUnique = true)]
public partial class UserAuth
{
    [Key]
    [Column("user_auth_id")]
    public Guid UserAuthId { get; set; }

    [Column("user_id")]
    public Guid UserId { get; set; }

    [Column("email")]
    [StringLength(320)]
    public string Email { get; set; } = null!;

    [Column("password_hash")]
    [StringLength(255)]
    public string PasswordHash { get; set; } = null!;

    [Column("is_verified")]
    public bool IsVerified { get; set; }

    [Column("last_login_at", TypeName = "timestamp without time zone")]
    public DateTime? LastLoginAt { get; set; }

    [Column("failed_attempts")]
    public int FailedAttempts { get; set; }

    [Column("locked_until", TypeName = "timestamp without time zone")]
    public DateTime? LockedUntil { get; set; }

    [Column("refresh_token")]
    [StringLength(255)]
    public string? RefreshToken { get; set; }

    [Column("refresh_token_expires_at", TypeName = "timestamp without time zone")]
    public DateTime? RefreshTokenExpiresAt { get; set; }

    [Column("token_revoked")]
    public bool TokenRevoked { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [ForeignKey("UserId")]
    [InverseProperty("UserAuths")]
    public virtual User User { get; set; } = null!;
}
