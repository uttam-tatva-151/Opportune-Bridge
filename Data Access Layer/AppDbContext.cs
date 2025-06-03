using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<AdminChildUser> AdminChildUsers { get; set; }

    public virtual DbSet<CustomPermission> CustomPermissions { get; set; }

    public virtual DbSet<ErrorLog> ErrorLogs { get; set; }

    public virtual DbSet<Module> Modules { get; set; }

    public virtual DbSet<Permission> Permissions { get; set; }

    public virtual DbSet<Role> Roles { get; set; }

    public virtual DbSet<RoleHierarchy> RoleHierarchies { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UserAuth> UserAuths { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseNpgsql("Host=localhost;Database=Opportune Bridge;Username=postgres;Password=Tatva@123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasPostgresExtension("uuid-ossp");

        modelBuilder.Entity<AdminChildUser>(entity =>
        {
            entity.HasKey(e => e.AdminChildUserId).HasName("admin_child_user_pkey");

            entity.ToTable("admin_child_user", tb => tb.HasComment("Maps admin users to their child users, representing hierarchical user relationships for permission and management purposes."));

            entity.Property(e => e.AdminChildUserId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.AdminId).HasComment("User ID of the admin (parent user).");
            entity.Property(e => e.ChildId).HasComment("User ID of the child (subordinate user).");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Admin).WithMany(p => p.AdminChildUserAdmins).HasConstraintName("fk_admin_id");

            entity.HasOne(d => d.Child).WithMany(p => p.AdminChildUserChildren).HasConstraintName("fk_child_id");
        });

        modelBuilder.Entity<CustomPermission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("CustomPermissions_pkey");

            entity.Property(e => e.PermissionId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.AdminChildUser).WithMany(p => p.CustomPermissions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("fk_admin_child_user_id");

            entity.HasOne(d => d.Module).WithMany(p => p.CustomPermissions).HasConstraintName("fk_module_id");

            entity.HasOne(d => d.Role).WithMany(p => p.CustomPermissions).HasConstraintName("fk_role_id");
        });

        modelBuilder.Entity<ErrorLog>(entity =>
        {
            entity.HasKey(e => e.ErrorId).HasName("errorlog_pkey");

            entity.ToTable("ErrorLog", tb => tb.HasComment("This table logs application errors and exception events. Each row stores the error details, including the message, stack trace, type, time of occurrence, status code, controller/action context, resolution status, and number of occurrences. Deduplication and counting of repeated errors is handled by triggers."));

            entity.Property(e => e.ErrorId).HasDefaultValueSql("gen_random_uuid()");
            entity.Property(e => e.ErrorOccurAt).HasDefaultValueSql("now()");
            entity.Property(e => e.ErrorOccurCount)
                .HasDefaultValueSql("1")
                .HasComment("Counts how many times this specific error has occurred (duplicates are incremented by trigger).");
        });

        modelBuilder.Entity<Module>(entity =>
        {
            entity.HasKey(e => e.ModuleId).HasName("Module_pkey");

            entity.ToTable("Module", tb => tb.HasComment("Stores application modules and their metadata including descriptions."));

            entity.Property(e => e.ModuleId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.IsGlobal).HasDefaultValueSql("true");
            entity.Property(e => e.ModifiedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<Permission>(entity =>
        {
            entity.HasKey(e => e.PermissionId).HasName("Permissions_pkey");

            entity.ToTable(tb => tb.HasComment("Maps roles and optionally users to modules with specific CRUD permissions. Allows admins to assign custom permissions to their child users."));

            entity.Property(e => e.PermissionId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.ModifiedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Module).WithMany(p => p.Permissions).HasConstraintName("fk_module_id");

            entity.HasOne(d => d.Role).WithMany(p => p.Permissions).HasConstraintName("fk_role_id");
        });

        modelBuilder.Entity<Role>(entity =>
        {
            entity.HasKey(e => e.RoleId).HasName("Roles_pkey");

            entity.ToTable(tb => tb.HasComment("Stores the roles for users."));

            entity.Property(e => e.RoleId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.Description).HasDefaultValueSql("''::text");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<RoleHierarchy>(entity =>
        {
            entity.HasKey(e => e.RoleHierarchyId).HasName("RoleHierarchy_pkey");

            entity.ToTable("RoleHierarchy", tb => tb.HasComment("Maps parent roles to their child roles, defining the role hierarchy structure."));

            entity.Property(e => e.RoleHierarchyId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.ChildRole).WithMany(p => p.RoleHierarchyChildRoles).HasConstraintName("fk_child_role");

            entity.HasOne(d => d.ParentRole).WithMany(p => p.RoleHierarchyParentRoles).HasConstraintName("fk_parent_role");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("User_pkey");

            entity.ToTable(tb => tb.HasComment("Stores user profile and non-authentication information, including avatar image as bytea."));

            entity.Property(e => e.UserId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.IsActive).HasDefaultValueSql("true");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
        });

        modelBuilder.Entity<UserAuth>(entity =>
        {
            entity.HasKey(e => e.UserAuthId).HasName("UserAuth_pkey");

            entity.ToTable("UserAuth", tb => tb.HasComment("Stores sensitive authentication information, tokens, MFA, and session data for users."));

            entity.Property(e => e.UserAuthId).HasDefaultValueSql("uuid_generate_v4()");
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
            entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");

            entity.HasOne(d => d.Role).WithMany(p => p.UserAuths).HasConstraintName("fk_role_id");

            entity.HasOne(d => d.User).WithMany(p => p.UserAuths).HasConstraintName("fk_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
