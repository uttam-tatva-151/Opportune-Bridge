using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

[Index("RoleId", "ModuleId", "AdminChildUserId", Name = "unique_role_module_user", IsUnique = true)]
public partial class CustomPermission
{
    [Key]
    [Column("permission_id")]
    public Guid PermissionId { get; set; }

    [Column("role_id")]
    public Guid RoleId { get; set; }

    [Column("module_id")]
    public Guid ModuleId { get; set; }

    [Column("admin_child_user_id")]
    public Guid? AdminChildUserId { get; set; }

    [Column("is_custom")]
    public bool IsCustom { get; set; }

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

    [Column("updated_at", TypeName = "timestamp without time zone")]
    public DateTime UpdatedAt { get; set; }

    [Required]
    [Column("is_active")]
    public bool? IsActive { get; set; }

    [ForeignKey("AdminChildUserId")]
    [InverseProperty("CustomPermissions")]
    public virtual AdminChildUser? AdminChildUser { get; set; }

    [ForeignKey("ModuleId")]
    [InverseProperty("CustomPermissions")]
    public virtual Module Module { get; set; } = null!;

    [ForeignKey("RoleId")]
    [InverseProperty("CustomPermissions")]
    public virtual Role Role { get; set; } = null!;
}
