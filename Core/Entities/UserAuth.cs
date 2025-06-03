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

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("UserAuths")]
    public virtual Role Role { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserAuths")]
    public virtual User User { get; set; } = null!;
}
