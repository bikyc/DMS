
param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

$projectPath = "DMS.Infrastructure/DMS.Infrastructure.csproj"
$startupPath = "DMS.Api/DMS.Api.csproj"
$outputDir = "DMS.Infrastructure/Migrations/User"

Write-Host "=== Adding User migration: $Name ===" -ForegroundColor Green

dotnet ef migrations add $Name `
    --context UserDbContext `
    --output-dir $outputDir `
    --project $projectPath `
    --startup-project $startupPath

if ($LASTEXITCODE -eq 0) {
    Write-Host "=== User migration added successfully ===" -ForegroundColor Green
} else {
    Write-Host "=== Error adding User migration ===" -ForegroundColor Red
    exit $LASTEXITCODE
}

