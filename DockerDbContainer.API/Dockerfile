# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM docker-images.artifactory.danskenet.net/db/dotnet/core/aspnet:8.0.10 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# This stage is used to build the service project
FROM docker-images.artifactory.danskenet.net/db/dotnet/core/aspnet:8.0.10 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["DockerDbContainer.API/DockerDbContainer.API.csproj", "DockerDbContainer.API/"]
RUN dotnet restore "./DockerDbContainer.API/DockerDbContainer.API.csproj"
COPY . .
WORKDIR "/src/DockerDbContainer.API"
RUN dotnet build "./DockerDbContainer.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DockerDbContainer.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DockerDbContainer.API.dll"]