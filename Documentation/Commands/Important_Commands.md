# 🚀 Important Commands for Opportune Bridge Project

## 🛠️ Scaffolding
```bash
dotnet ef dbcontext scaffold "Host=localhost;Database=Opportune Bridge;Username=postgres;Password=Tatva@123" Npgsql.EntityFrameworkCore.PostgreSQL -c AppDbContext --context-dir . --output-dir ../Core/Entities --force --data-annotations
```

## 📦 Migrations
### Add a New Migration
```bash
dotnet ef migrations add <MigrationName> --context AppDbContext
```

### Apply Migrations
```bash
dotnet ef database update --context AppDbContext
```

## 🔍 Database Management
### Drop Database
```bash
dotnet ef database drop --force --context AppDbContext
```

### Update Database Schema
```bash
dotnet ef migrations script --context AppDbContext
```

## 📚 Table of Contents
1. [Scaffolding](#-scaffolding)
2. [Migrations](#-migrations)
3. [Database Management](#-database-management)

---

> **Note:** Replace `<MigrationName>` with your desired migration name.

---