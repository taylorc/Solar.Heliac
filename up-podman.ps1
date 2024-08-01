Param(
    [switch]$skipDeploy = $false
)

Write-Host "🚢 Starting Podman Compose" -ForegroundColor Green
podman machine start
podman compose up -d

if (-not $skipDeploy) {
    $upScriptPath = $Script:MyInvocation.MyCommand.Path | Split-Path

    Write-Host "🚀 Creating and Seeding Database" -ForegroundColor Green
    Set-Location ./tools/Database/
    dotnet run

    Set-Location $upScriptPath
}
