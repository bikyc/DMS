
param(
    [Parameter(Mandatory=$false)]
    [switch]$Force
)

Write-Host "=== Removing last migrations from all contexts ===" -ForegroundColor Cyan
Write-Host ""

$projectPath = "DMS.Infrastructure/DMS.Infrastructure.csproj"
$startupPath = "DMS.Api/DMS.Api.csproj"

$contexts = @(
    @{Name="Patient"; Context="PatientDbContext"},
    @{Name="Document"; Context="DocumentDbContext"},
    @{Name="User"; Context="UserDbContext"}
)

foreach ($ctx in $contexts) {
    Write-Host "Removing last migration from $($ctx.Name) context..." -ForegroundColor Yellow
    
    $command = "dotnet ef migrations remove --context $($ctx.Context) --project $projectPath --startup-project $startupPath"
    if ($Force) {
        $command += " --force"
    }
    
    Invoke-Expression $command
    
    if ($LASTEXITCODE -ne 0) {
        Write-Host "Failed to remove $($ctx.Name) migration" -ForegroundColor Red
        # Continue with other contexts even if one fails
    }
    Write-Host ""
}

Write-Host "=== Migration removal completed ===" -ForegroundColor Green

