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

# Installer les dépendances nécessaires pour Chromium
RUN apt-get update \
    && apt-get install -y wget gnupg \
    && wget -q -O - https://dl-ssl.google.com/linux/linux_signing_key.pub | apt-key add - \
    && sh -c 'echo "deb [arch=amd64] http://dl.google.com/linux/chrome/deb/ stable main" >> /etc/apt/sources.list.d/google.list' \
    && apt-get update \
    && apt-get install -y google-chrome-stable fonts-ipafont-gothic fonts-wqy-zenhei fonts-thai-tlwg fonts-kacst fonts-freefont-ttf libxss1 \
      --no-install-recommends \
    && rm -rf /var/lib/apt/lists/*
RUN npm init -y &&  \
    npm i puppeteer \
    # Add user so we don't need --no-sandbox.
    # same layer as npm install to keep re-chowned files from using up several hundred MBs more space
    && groupadd -r pptruser && useradd -r -g pptruser -G audio,video pptruser \
    && mkdir -p /home/pptruser/Downloads \
    && chown -R pptruser:pptruser /home/pptruser \
    && chown -R pptruser:pptruser /node_modules \
    && chown -R pptruser:pptruser /package.json \
    && chown -R pptruser:pptruser /package-lock.json
USER pptruser

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
 