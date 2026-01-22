# -----------------------------
# get connection string from .env file
# -----------------------------
$envPath = Join-Path $PSScriptRoot "..\.env"
Resolve-Path $envPath
Get-Content $envPath | ForEach-Object {
    if ($_ -and -not $_.StartsWith('#')) {
        $key, $value = $_ -split '=', 2
        [Environment]::SetEnvironmentVariable($key, $value)
    }
}

$connectionString = $connectionString = $env:CONNECTION_STRING.Trim('"')

if (-not $connectionString) {
    throw "CONNECTION_STRING not found"
}

# ---------------------------------
# parse connection string function
# ---------------------------------
function Get-ConnValue($key) {
    return ($connectionString -split ';' |
        ForEach-Object { $_.Trim() } |
        Where-Object { $_ -like "$key=*" } |
        ForEach-Object { ($_ -split '=', 2)[1] })
}

# -----------------------------
# extract variables from .env file
# -----------------------------
$dbName   = Get-ConnValue "Database"
$user     = Get-ConnValue "User Id"
$password = Get-ConnValue "Password"
$serverRaw = Get-ConnValue "Server"
$server    = $serverRaw.Split(',')[0]
$port      = $serverRaw.Split(',')[1]

if (-not $dbName -or -not $user -or -not $password -or -not $server -or -not $port) {
    throw "invalid CONNECTION_STRING"
}

#Kill any runnning container
docker kill fb-db-local 

#Remove existance containers
docker rm fb-db-local 

#Remove existance images
docker rmi fb/db-local 

#Remove existance network
docker network rm fb-network

#Create network for fb
docker network create fb-network

#Build images
docker build --progress=plain -t fb/db-local -f Dockerfile.db .

#Run container for db local
docker run --network fb-network -d `
  -e "ACCEPT_EULA=Y" `
  -e "MSSQL_SA_PASSWORD=$password" `
  -p ${port}:1433 `
  --name 'fb-db-local' `
  fb/db-local

Write-Host "Waiting for SQL Server to initialize..."
Start-Sleep -Seconds 10
Write-Host "SQL Server is ready!"
Write-Host "
IF DB_ID(N'$dbName') IS NULL
BEGIN
    CREATE DATABASE [$dbName];
END
"
docker exec fb-db-local /opt/mssql-tools18/bin/sqlcmd `
    -S localhost `
    -U $user `
    -P $password `
    -C `
    -Q "
IF DB_ID(N'$dbName') IS NULL
BEGIN
    CREATE DATABASE [$dbName];
END
"

Write-Host "database '$dbName' is ready"

dotnet ef database update --project .\src\FirstNETWebApp.csproj

Write-Host "migration for '$dbName' have done"
