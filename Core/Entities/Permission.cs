using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Maps roles to modules with specific CRUD permissions.
/// </summary>
[Index("RoleId", "ModuleId", Name = "role_module_unique", IsUnique = true)]
public partial class Permission
{
    [Key]
    [Column("permission_id")]
    public Guid PermissionId { get; set; }

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [Column("module_id")]
    public Guid ModuleId { get; set; }

    [Column("can_read")]
    public bool CanRead { get; set; }

    [Column("can_add")]
    public bool CanAdd { get; set; }

    [Column("can_edit")]
    public bool CanEdit { get; set; }

    [Column("can_delete")]
    public bool CanDelete { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("modified_at", TypeName = "timestamp without time zone")]
    public DateTime ModifiedAt { get; set; }

    [Column("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("Permissions")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("Permissions")]
    public virtual Role Role { get; set; } = null!;
}
