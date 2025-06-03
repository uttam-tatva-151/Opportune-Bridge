using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Maps admin users to their child users, representing hierarchical user relationships for permission and management purposes.
/// </summary>
[Table("admin_child_user")]
[Index("AdminId", "ChildId", Name = "unique_admin_child", IsUnique = true)]
public partial class AdminChildUser
{
    [Key]
    [Column("admin_child_user_id")]
    public Guid AdminChildUserId { get; set; }

    /// <summary>
    /// User ID of the admin (parent user).
    /// </summary>
    [Column("admin_id")]
    public Guid AdminId { get; set; }

    /// <summary>
    /// User ID of the child (subordinate user).
    /// </summary>
    [Column("child_id")]
    public Guid ChildId { get; set; }

    [Column("relation_description")]
    public string? RelationDescription { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("AdminId")]
    [InverseProperty("AdminChildUserAdmins")]
    public virtual User Admin { get; set; } = null!;

    [ForeignKey("ChildId")]
    [InverseProperty("AdminChildUserChildren")]
    public virtual User Child { get; set; } = null!;

    [InverseProperty("AdminChildUser")]
    public virtual ICollection<CustomPermission> CustomPermissions { get; set; } = new List<CustomPermission>();
}
