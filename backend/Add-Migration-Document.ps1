
param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

$projectPath = "DMS.Infrastructure/DMS.Infrastructure.csproj"
$startupPath = "DMS.Api/DMS.Api.csproj"
$outputDir = "DMS.Infrastructure/Migrations/Document"

Write-Host "=== Adding Document migration: $Name ===" -ForegroundColor Green

dotnet ef migrations add $Name `
    --context DocumentDbContext `
    --output-dir $outputDir `
    --project $projectPath `
    --startup-project $startupPath

if ($LASTEXITCODE -eq 0) {
    Write-Host "=== Document migration added successfully ===" -ForegroundColor Green
} else {
    Write-Host "=== Error adding Document migration ===" -ForegroundColor Red
    exit $LASTEXITCODE
}

