
param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

$projectPath = "DMS.Infrastructure/DMS.Infrastructure.csproj"
$startupPath = "DMS.Api/DMS.Api.csproj"
$outputDir = "DMS.Infrastructure/Migrations/Patient"

Write-Host "=== Adding Patient migration: $Name ===" -ForegroundColor Green

dotnet ef migrations add $Name `
    --context PatientDbContext `
    --output-dir $outputDir `
    --project $projectPath `
    --startup-project $startupPath

if ($LASTEXITCODE -eq 0) {
    Write-Host "=== Patient migration added successfully ===" -ForegroundColor Green
} else {
    Write-Host "=== Error adding Patient migration ===" -ForegroundColor Red
    exit $LASTEXITCODE
}

