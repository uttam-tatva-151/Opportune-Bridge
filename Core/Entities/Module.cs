using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

/// <summary>
/// Stores application modules and their metadata including descriptions.
/// </summary>
[Table("Module")]
[Index("ModuleName", Name = "Module_module_name_key", IsUnique = true)]
[Index("ModuleName", Name = "idx_module_module_name")]
public partial class Module
{
    [Key]
    [Column("module_id")]
    public Guid ModuleId { get; set; }

    [Column("module_name")]
    [StringLength(100)]
    public string ModuleName { get; set; } = null!;

    [Column("description")]
    public string Description { get; set; } = null!;

    [Required]
    [Column("is_global")]
    public bool? IsGlobal { get; set; }

    [Column("created_at", TypeName = "timestamp without time zone")]
    public DateTime CreatedAt { get; set; }

    [Column("modified_at", TypeName = "timestamp without time zone")]
    public DateTime ModifiedAt { get; set; }

    [Required]
    [Column("is_active")]
    public bool? IsActive { get; set; }

    [Column("created_by")]
    public Guid? CreatedBy { get; set; }

    [Column("modified_by")]
    public Guid? ModifiedBy { get; set; }

    [InverseProperty("Module")]
    public virtual ICollection<CustomPermission> CustomPermissions { get; set; } = new List<CustomPermission>();

    [InverseProperty("Module")]
    public virtual ICollection<Permission> Permissions { get; set; } = new List<Permission>();
}
