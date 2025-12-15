
Write-Host "=== Updating database for all contexts ===" -ForegroundColor Cyan
Write-Host ""

$projectPath = "DMS.Infrastructure/DMS.Infrastructure.csproj"
$startupPath = "DMS.Api/DMS.Api.csproj"

$contexts = @(
    @{Name="Patient"; Context="PatientDbContext"},
    @{Name="Document"; Context="DocumentDbContext"},
    @{Name="User"; Context="UserDbContext"}
)

foreach ($ctx in $contexts) {
    Write-Host "Updating database for $($ctx.Name) context..." -ForegroundColor Yellow
    
    dotnet ef database update `
        --context $($ctx.Context) `
        --project $projectPath `
        --startup-project $startupPath
    
    if ($LASTEXITCODE -eq 0) {
        Write-Host "$($ctx.Name) database updated successfully" -ForegroundColor Green
    } else {
        Write-Host "Failed to update $($ctx.Name) database" -ForegroundColor Red
        exit $LASTEXITCODE
    }
    Write-Host ""
}

Write-Host "=== All databases updated successfully ===" -ForegroundColor Green

