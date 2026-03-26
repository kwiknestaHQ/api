# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/Gateway/KwikNestaGateway.API/KwikNestaGateway.API.csproj", "Gateway/KwikNestaGateway.API/"]
# Mediator
COPY ["src/KwikNesta.Mediator/KwikNesta.Mediator.csproj", "KwikNesta.Mediator/"]
# Shared
COPY ["src/KwikNesta.Shared/KwikNesta.Shared.csproj", "KwikNesta.Shared/"]
# Identity
COPY ["src/Identity/KwikNestaIdentity.Application/KwikNestaIdentity.Application.csproj", "Identity/KwikNestaIdentity.Application/"]
COPY ["src/Identity/KwikNestaIdentity.Domain/KwikNestaIdentity.Domain.csproj", "Identity/KwikNestaIdentity.Domain/"]
COPY ["src/Identity/KwikNestaIdentity.Infrastructure/KwikNestaIdentity.Infrastructure.csproj", "Identity/KwikNestaIdentity.Infrastructure/"]
COPY ["src/Identity/KwikNestaIdentity.Infrastructure.Data/KwikNestaIdentity.Infrastructure.Data.csproj", "Identity/KwikNestaIdentity.Infrastructure.Data/"]
# Infra
COPY ["src/Infra/KwikNestaInfra.Application/KwikNestaInfra.Application.csproj", "Infra/KwikNestaInfra.Application/"]
COPY ["src/Infra/KwikNestaInfra.Domain/KwikNestaInfra.Domain.csproj", "Infra/KwikNestaInfra.Domain/"]
COPY ["src/Infra/KwikNestaInfra.Infrastructure/KwikNestaInfra.Infrastructure.csproj", "Infra/KwikNestaInfra.Infrastructure/"]
COPY ["src/Infra/KwikNestaInfra.Infrastructure.Data/KwikNestaInfra.Infrastructure.Data.csproj", "Infra/KwikNestaInfra.Infrastructure.Data/"]
# Properties
COPY ["src/Properties/KwikNestaProperty.Application/KwikNestaProperty.Application.csproj", "Properties/KwikNestaProperty.Application/"]
COPY ["src/Properties/KwikNestaProperty.Domain/KwikNestaProperty.Domain.csproj", "Properties/KwikNestaProperty.Domain/"]
COPY ["src/Properties/KwikNestaProperty.Infrastructure/KwikNestaProperty.Infrastructure.csproj", "Properties/KwikNestaProperty.Infrastructure/"]
COPY ["src/Properties/KwikNestaProperty.Infrastructure.Data/KwikNestaProperty.Infrastructure.Data.csproj", "Properties/KwikNestaProperty.Infrastructure.Data/"]

# Restore deps
RUN dotnet restore "Gateway/KwikNestaGateway.API/KwikNestaGateway.API.csproj"
COPY src/ .
WORKDIR "/src/Gateway/KwikNestaGateway.API"
RUN dotnet build "KwikNestaGateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "KwikNestaGateway.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "KwikNestaGateway.API.dll"]