# Build container. Load full SDK as base image.
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# TODO: Adapt the directories!
COPY tschess.Application     tschess.Application
COPY tschess.Backend         tschess.Backend

# Compile the app
RUN dotnet restore "tschess.Backend"
RUN dotnet build   "tschess.Backend" -c Release -o /app/build
RUN dotnet publish "tschess.Backend" -c Release -o /app/publish /p:UseAppHost=false

# App container. Only needs runtime (smaller image)
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS final
EXPOSE 80
EXPOSE 443
WORKDIR /app

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "tschess.Backend.dll"]