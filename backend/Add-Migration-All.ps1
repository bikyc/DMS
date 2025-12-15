
param(
    [Parameter(Mandatory=$true)]
    [string]$Name
)

Write-Host "=== Adding migrations for all contexts: $Name ===" -ForegroundColor Cyan
Write-Host ""

# Add Patient migration
Write-Host "1/3 - Adding Patient migration..." -ForegroundColor Yellow
& ".\Add-Migration-Patient.ps1" -Name "${Name}_Patient"
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to add Patient migration" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host ""

# Add Document migration
Write-Host "2/3 - Adding Document migration..." -ForegroundColor Yellow
& ".\Add-Migration-Document.ps1" -Name "${Name}_Document"
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to add Document migration" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host ""

# Add User migration
Write-Host "3/3 - Adding User migration..." -ForegroundColor Yellow
& ".\Add-Migration-User.ps1" -Name "${Name}_User"
if ($LASTEXITCODE -ne 0) {
    Write-Host "Failed to add User migration" -ForegroundColor Red
    exit $LASTEXITCODE
}
Write-Host ""

Write-Host "=== All migrations added successfully ===" -ForegroundColor Green

