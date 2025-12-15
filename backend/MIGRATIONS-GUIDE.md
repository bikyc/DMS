# Database Migrations Guide

This project uses **multiple DbContexts** sharing the same database. Each context has its own migration history table to avoid conflicts.

## DbContexts

1. **PatientDbContext** - Manages `Patient` entities
2. **DocumentDbContext** - Manages `Document`, `DocumentCategory`, `DocumentTag`, `DocumentTagMap`, `PatientDocumentMap`, and `AuditLog` entities
3. **UserDbContext** - Manages `User` and `Role` entities

## Migration History Tables

Each context uses a separate migration history table:
- `__PatientMigrationsHistory`
- `__DocumentMigrationsHistory`
- `__UserMigrationsHistory`

## Migration Scripts

### Individual Context Migrations

#### Add Migration for Specific Context

```powershell
# Patient context
.\Add-Migration-Patient.ps1 -Name "MigrationName"

# Document context
.\Add-Migration-Document.ps1 -Name "MigrationName"

# User context
.\Add-Migration-User.ps1 -Name "MigrationName"
```

#### Add Migrations for All Contexts

```powershell
.\Add-Migration-All.ps1 -Name "MigrationName"
```

This creates three migrations:
- `MigrationName_Patient`
- `MigrationName_Document`
- `MigrationName_User`

### Database Updates

#### Update All Databases

```powershell
.\Update-Database-All.ps1
```

This applies pending migrations for all contexts.

#### Update Specific Context

```powershell
dotnet ef database update --context PatientDbContext --project DMS.Infrastructure/DMS.Infrastructure.csproj --startup-project DMS.Api/DMS.Api.csproj

dotnet ef database update --context DocumentDbContext --project DMS.Infrastructure/DMS.Infrastructure.csproj --startup-project DMS.Api/DMS.Api.csproj

dotnet ef database update --context UserDbContext --project DMS.Infrastructure/DMS.Infrastructure.csproj --startup-project DMS.Api/DMS.Api.csproj
```

### Remove Migrations

```powershell
# Remove last migration from all contexts
.\Remove-Migration-All.ps1

# Remove with force (if database was already updated)
.\Remove-Migration-All.ps1 -Force
```

## Migration Folders

Migrations are organized in separate folders:
- `DMS.Infrastructure/Migrations/Patient/`
- `DMS.Infrastructure/Migrations/Document/`
- `DMS.Infrastructure/Migrations/User/`

## Important Notes

1. **Cross-Context References**: Some entities reference entities in other contexts (e.g., `PatientDocumentMap` references `Patient` from PatientDbContext). These are handled by storing foreign keys without navigation properties across contexts.

2. **Database Sharing**: All contexts share the same database connection string, but maintain separate migration histories.

3. **Auto-Migration on Startup**: The application automatically applies pending migrations for all contexts on startup (see `Program.cs`).

## Initial Setup

To create initial migrations:

```powershell
# Create initial migrations for all contexts
.\Add-Migration-All.ps1 -Name "InitialCreate"

# Apply migrations to database
.\Update-Database-All.ps1
```

## Troubleshooting

### Migration Conflicts

If you encounter migration conflicts:
1. Ensure each context uses its designated migration history table (configured in `InfrastructureServiceRegistration.cs`)
2. Check that migrations are in the correct folder for each context
3. Verify connection strings match across all contexts

### Cross-Context Foreign Keys

Foreign keys that reference entities in other contexts are stored as simple properties. Navigation properties are ignored in the source context to avoid EF Core relationship tracking issues.

