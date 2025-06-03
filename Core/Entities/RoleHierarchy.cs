using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Maps parent roles to their child roles, defining the role hierarchy structure.
/// </summary>
[Table("RoleHierarchy")]
[Index("ParentRoleId", "ChildRoleId", Name = "RoleHierarchy_unique", IsUnique = true)]
[Index("ChildRoleId", Name = "idx_rolehierarchy_child_role_id")]
[Index("ParentRoleId", Name = "idx_rolehierarchy_parent_role_id")]
public partial class RoleHierarchy
{
    [Key]
    [Column("role_hierarchy_id")]
    public Guid RoleHierarchyId { get; set; }

    [Column("parent_role_id")]
    public Guid ParentRoleId { get; set; }

    [Column("child_role_id")]
    public Guid ChildRoleId { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("updated_by")]
    public Guid? UpdatedBy { get; set; }

    [ForeignKey("ChildRoleId")]
    [InverseProperty("RoleHierarchyChildRoles")]
    public virtual Role ChildRole { get; set; } = null!;

    [ForeignKey("ParentRoleId")]
    [InverseProperty("RoleHierarchyParentRoles")]
    public virtual Role ParentRole { get; set; } = null!;
}
