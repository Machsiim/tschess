docker rm -f chess_sqlserver2019  &> /dev/nul
docker run -d -p 1433:1433 --name chess_sqlserver2019 -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=SqlServer2019" mcr.microsoft.com/azure-sql-edge
dotnet build tschess/tschess.Backend --no-incremental --force
dotnet watch run -c Debug --project tschess/tschess.Backend
