using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Stores the roles for users.
/// </summary>
[Index("RoleName", Name = "Roles_role_name_key", IsUnique = true)]
[Index("RoleName", Name = "idx_roles_role_name")]
public partial class Role
{
    [Key]
    [Column("role_id")]
    public Guid RoleId { get; set; }

    [Column("role_name")]
    [StringLength(50)]
    public string RoleName { get; set; } = null!;

    [Column("is_global")]
    public bool IsGlobal { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }

    [Column("description")]
    public string Description { get; set; } = null!;

    [Column("is_parent_role")]
    public bool IsParentRole { get; set; }

    [InverseProperty("Role")]
    public virtual ICollection<CustomPermission> CustomPermissions { get; set; } = new List<CustomPermission>();

    [InverseProperty("Role")]
    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();

    [InverseProperty("ChildRole")]
    public virtual ICollection<RoleHierarchy> RoleHierarchyChildRoles { get; set; } = new List<RoleHierarchy>();

    [InverseProperty("ParentRole")]
    public virtual ICollection<RoleHierarchy> RoleHierarchyParentRoles { get; set; } = new List<RoleHierarchy>();

    [InverseProperty("Role")]
    public virtual ICollection<UserAuth> UserAuths { get; set; } = new List<UserAuth>();
}
