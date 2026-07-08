# SecureShield IIS Deployment Script
# Run as Administrator on the production Windows Server

param(
    [string]$SiteName = "SecureShield",
    [string]$PhysicalPath = "C:\Deploy\SecureShield",
    [string]$AppPoolName = "SecureShieldAppPool",
    [string]$HostName = "secureshield.com.bd",
    [string]$ConnectionString = "",
    [string]$JwtSecret = "",
    [string]$SmtpHost = "smtp.yourprovider.com",
    [string]$SmtpUser = "noreply@secureshield.com.bd",
    [string]$SmtpPassword = "",
    [string]$RecaptchaSiteKey = "",
    [string]$RecaptchaSecretKey = ""
)

$ErrorActionPreference = "Stop"
Import-Module WebAdministration

Write-Host "==================================" -ForegroundColor Cyan
Write-Host "  SecureShield IIS Deployment" -ForegroundColor Cyan
Write-Host "==================================" -ForegroundColor Cyan

# --- 1. Check prerequisites ---
Write-Host "`n[1/8] Checking prerequisites..." -ForegroundColor Yellow

if (-not (Get-Command dotnet -ErrorAction SilentlyContinue)) {
    throw ".NET SDK not found. Install .NET 10 SDK first."
}

$aspnetCoreModule = Get-WebGlobalModule -Name "AspNetCoreModuleV2" -ErrorAction SilentlyContinue
if (-not $aspnetCoreModule) {
    throw "ASP.NET Core Hosting Bundle not installed. Download from https://dotnet.microsoft.com/download/dotnet/10.0"
}

$urlRewrite = Get-WebGlobalModule -Name "RewriteModule" -ErrorAction SilentlyContinue
if (-not $urlRewrite) {
    Write-Warning "URL Rewrite Module not installed — installing..."
    # Download URL Rewrite 2.1
    $installer = "$env:TEMP\rewrite_2.1_amd64_en-US.msi"
    Invoke-WebRequest "https://download.microsoft.com/download/1/2/8/128E2E22-C1B9-44A4-BE2A-5859ED207D2C/rewrite_amd64_en-US.msi" -OutFile $installer
    Start-Process msiexec.exe -ArgumentList "/i", $installer, "/quiet" -Wait
}

Write-Host "  Prerequisites OK." -ForegroundColor Green

# --- 2. Publish the app ---
Write-Host "`n[2/8] Publishing SecureShield.Web..." -ForegroundColor Yellow

$repoRoot = Split-Path -Parent $MyInvocation.MyCommand.Path
$webProject = Join-Path $repoRoot "src\SecureShield.Web\SecureShield.Web.csproj"

if (-not (Test-Path $webProject)) {
    Write-Warning "Web project not found at $webProject — assuming you're running from already-published location."
} else {
    dotnet publish $webProject -c Release -o $PhysicalPath
    if ($LASTEXITCODE -ne 0) { throw "Publish failed." }
}

Write-Host "  Published to $PhysicalPath" -ForegroundColor Green

# --- 3. Update appsettings.json ---
Write-Host "`n[3/8] Updating appsettings.json..." -ForegroundColor Yellow

$settingsPath = Join-Path $PhysicalPath "appsettings.json"
$json = Get-Content $settingsPath -Raw | ConvertFrom-Json

if ($ConnectionString) {
    $json.ConnectionStrings.Default = $ConnectionString
}
if ($JwtSecret) {
    $json.Jwt.Secret = $JwtSecret
}
if ($SmtpHost) {
    $json.Smtp.Host = $SmtpHost
}
if ($SmtpUser) {
    $json.Smtp.User = $SmtpUser
}
if ($SmtpPassword) {
    $json.Smtp.Password = $SmtpPassword
}
if ($RecaptchaSiteKey) {
    $json.Recaptcha.SiteKey = $RecaptchaSiteKey
}
if ($RecaptchaSecretKey) {
    $json.Recaptcha.SecretKey = $RecaptchaSecretKey
}

$json | ConvertTo-Json -Depth 10 | Set-Content $settingsPath -Encoding UTF8
Write-Host "  Settings updated." -ForegroundColor Green

# --- 4. Create uploads folder ---
Write-Host "`n[4/8] Creating uploads folder..." -ForegroundColor Yellow

$uploadsPath = Join-Path $PhysicalPath "wwwroot\uploads\cv"
New-Item -ItemType Directory -Force -Path $uploadsPath | Out-Null
Write-Host "  Created $uploadsPath" -ForegroundColor Green

# --- 5. Create App Pool ---
Write-Host "`n[5/8] Creating App Pool..." -ForegroundColor Yellow

if (-not (Test-Path "IIS:\AppPools\$AppPoolName")) {
    New-WebAppPool -Name $AppPoolName
}
Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name managedRuntimeVersion -Value ""  # No Managed Code (out-of-process)
Set-ItemProperty "IIS:\AppPools\$AppPoolName" -Name startMode -Value "AlwaysRunning"
Write-Host "  App pool '$AppPoolName' ready." -ForegroundColor Green

# --- 6. Create Website ---
Write-Host "`n[6/8] Creating Website..." -ForegroundColor Yellow

$sitePath = "IIS:\Sites\$SiteName"
if (Test-Path $sitePath) {
    Write-Warning "  Site '$SiteName' already exists — removing."
    Remove-Website -Name $SiteName
}

New-Website -Name $SiteName -PhysicalPath $PhysicalPath -ApplicationPool $AppPoolName -Force
Set-ItemProperty $sitePath -Name binddings -Value @(
    @{ protocol = "http"; bindingInformation = "*:80:$HostName" },
    @{ protocol = "https"; bindingInformation = "*:443:$HostName" }
)

Write-Host "  Website '$SiteName' created with host $HostName" -ForegroundColor Green

# --- 7. Set NTFS permissions ---
Write-Host "`n[7/8] Setting NTFS permissions..." -ForegroundColor Yellow

$acl = Get-Acl $PhysicalPath
$rule = New-Object System.Security.AccessControl.FileSystemAccessRule(
    "IIS AppPool\$AppPoolName",
    "ReadAndExecute",
    "ContainerInherit,ObjectInherit",
    "None",
    "Allow"
)
$acl.AddAccessRule($rule)
Set-Acl -Path $PhysicalPath -AclObject $acl

# Allow modify on uploads folder
$uploadAcl = Get-Acl $uploadsPath
$uploadRule = New-Object System.Security.AccessControl.FileSystemAccessRule(
    "IIS AppPool\$AppPoolName",
    "Modify",
    "ContainerInherit,ObjectInherit",
    "None",
    "Allow"
)
$uploadAcl.AddAccessRule($uploadRule)
Set-Acl -Path $uploadsPath -AclObject $uploadAcl

Write-Host "  Permissions set." -ForegroundColor Green

# --- 8. Enable WebSockets (required for Blazor Server) ---
Write-Host "`n[8/8] Enabling WebSockets..." -ForegroundColor Yellow

Set-ItemProperty $sitePath -Name webSockets.enabled -Value $true
Set-ItemProperty $sitePath -Name webSockets.pingInterval -Value "00:00:30"

Write-Host "  WebSockets enabled." -ForegroundColor Green

# --- Done ---
Write-Host "`n==================================" -ForegroundColor Green
Write-Host "  Deployment Complete!" -ForegroundColor Green
Write-Host "==================================" -ForegroundColor Green
Write-Host "`nSite:        https://$HostName" -ForegroundColor White
Write-Host "Admin URL:   https://$HostName/admin/login" -ForegroundColor White
Write-Host "Admin user:  admin@secureshield.com.bd" -ForegroundColor White
Write-Host "Password:    Admin@123 (CHANGE IMMEDIATELY!)" -ForegroundColor Yellow
Write-Host "`nNext steps:" -ForegroundColor Cyan
Write-Host "1. Open https://$HostName to verify the site loads"
Write-Host "2. Open https://$HostName/admin/login and change the admin password"
Write-Host "3. Configure a valid SSL certificate for the https binding"
Write-Host "4. Set up nightly SQL backups"
Write-Host "5. Set up Cloudflare or another CDN in front of IIS"
Write-Host ""

# Optional: open the site
Start-Process "https://$HostName"
