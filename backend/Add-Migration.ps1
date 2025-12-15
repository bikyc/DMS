# Legacy script - Use Add-Migration-Document.ps1 instead
# This script is kept for backward compatibility

param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

Write-Host "=== WARNING: This is a legacy script ===" -ForegroundColor Yellow
Write-Host "Consider using Add-Migration-Document.ps1 for Document context" -ForegroundColor Yellow
Write-Host "Or use Add-Migration-All.ps1 for all contexts" -ForegroundColor Yellow
Write-Host ""

& ".\Add-Migration-Document.ps1" -Name $Name
