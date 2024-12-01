# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["TuringIA.Server/TuringIA.Server.csproj", "TuringIA.Server/"]
RUN dotnet restore "./TuringIA.Server/TuringIA.Server.csproj"
COPY . .
WORKDIR "/src/TuringIA.Server"
RUN dotnet build "./TuringIA.Server.csproj" -c Release -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final
FROM build AS publish
RUN dotnet publish "./TuringIA.Server.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "TuringIA.Server.dll"]
