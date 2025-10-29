# load environment variables from .env
$envFile = Join-Path $PSScriptRoot "..\.env"

if (Test-Path $envFile) {
    Get-Content $envFile | ForEach-Object {
        if ($_ -match '^\s*([^#=]+?)\s*=\s*(.*)\s*$') {
            $key = $matches[1].Trim()
            $value = $matches[2].Trim().Trim('"')
            [System.Environment]::SetEnvironmentVariable($key, $value)
        }
    }
} else {
    Write-Error ".env file not found at path: $envFile"
    exit 1
}

# read connection string
$connectionString = $env:CONNECTION_STRING

if (-not $connectionString) {
    Write-Error "CONNECTION_STRING not found in .env file"
    exit 1
}

# run scaffold command
dotnet ef dbcontext scaffold `
    "$connectionString" `
    Microsoft.EntityFrameworkCore.SqlServer `
    --output-dir Models `
    --context-dir ./infrastructure/database/entityFramework/ `
    --context AppDbContext `
    --no-onconfiguring `
    --force `
    --project .\src\FirstNETWebApp.csproj
