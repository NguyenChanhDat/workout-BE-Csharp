#Kill any runnning container
docker kill fb-db-local 
dịch
#Remove existance containers
docker rm fb-db-local 

#Remove existance images
docker rmi fb/db-local 

#Create network for fb
docker network create fb-network

#Build images
docker build --progress=plain -t fb/db-local -f Dockerfile.db .

#Run container for db local
docker run --network fb-network -d `
  -e 'ACCEPT_EULA=Y' `
  -e 'MSSQL_SA_PASSWORD=Dat20112011' `
  -p 1401:1433 `
  --name 'fb-db-local' `
  fb/db-local

Write-Host "Waiting for SQL Server to initialize..."
Start-Sleep -Seconds 10
Write-Host "SQL Server is ready!"