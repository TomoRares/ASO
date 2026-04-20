Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host "   LISTA SERVICIILOR SISTEM ACTIVE (RUNNING) - WINDOWS   " -ForegroundColor White -BackgroundColor Blue
Write-Host "==========================================================" -ForegroundColor Cyan
Write-Host ""

$serviciiActive = Get-Service | Where-Object { $_.Status -eq "Running" } | Select-Object `
    @{Name="Nume Serviciu"; Expression={$_.Name}},
    @{Name="Nume Afisat"; Expression={$_.DisplayName}},
    @{Name="Tip Pornire"; Expression={$_.StartType}}

$serviciiActive | Format-Table -AutoSize

$total = ($serviciiActive).Count
Write-Host "----------------------------------------------------------" -ForegroundColor Cyan
Write-Host "Total servicii active identificate: $total" -ForegroundColor Green
Write-Host "----------------------------------------------------------" -ForegroundColor Cyan

$path = "$PSScriptRoot\Servicii_Active.txt"
$serviciiActive | Out-File -FilePath $path
Write-Host "Raportul a fost salvat automat in: $path" -ForegroundColor Gray
