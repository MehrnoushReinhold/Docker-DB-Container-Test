# Start Docker Compose
docker-compose up -d

# Wait for SQL Server to be ready
do {
    Start-Sleep -Seconds 1
    $result = docker exec sqlserver /opt/mssql-tools/bin/sqlcmd -S localhost -U sa -P YourStrong!Passw0rd -Q 'SELECT 1' 2>$null
} until ($result -ne $null)

Write-Host "SQL Server is ready. Starting API..."

# Start the ASP.NET Core API
dotnet run --project YourApiProject.csproj