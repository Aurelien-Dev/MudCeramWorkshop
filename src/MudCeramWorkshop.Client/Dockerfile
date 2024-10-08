# See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

# This stage is used when running from VS in fast mode (Default for Debug configuration)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 80
EXPOSE 443


# This stage is used to build the service project
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
RUN rm -rf /src/*
COPY ["MudCeramWorkshop.Client/MudCeramWorkshop.Client.csproj", "MudCeramWorkshop.Client/"]
COPY ["MudCeramWorkshop.Data.Repository/MudCeramWorkshop.Data.Repository.csproj", "MudCeramWorkshop.Data.Repository/"]
COPY ["MudCeramWorkshop.Data.Domain/MudCeramWorkshop.Data.Domain.csproj", "MudCeramWorkshop.Data.Domain/"]
RUN dotnet restore "./MudCeramWorkshop.Client/MudCeramWorkshop.Client.csproj"
COPY . .
WORKDIR "/src/MudCeramWorkshop.Client"
RUN dotnet build "./MudCeramWorkshop.Client.csproj" -c $BUILD_CONFIGURATION -o /app/build

# This stage is used to publish the service project to be copied to the final stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./MudCeramWorkshop.Client.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# This stage is used in production or when running from VS in regular mode (Default when not using the Debug configuration)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
VOLUME ["/app/data"] 
ENTRYPOINT ["dotnet", "MudCeramWorkshop.Client.dll"]
 