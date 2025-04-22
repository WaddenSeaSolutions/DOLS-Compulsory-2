# Use base runtime for final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Node-enabled build environment
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS with-node
RUN apt-get update && \
    apt-get install -y curl && \
    curl -sL https://deb.nodesource.com/setup_20.x | bash - && \
    apt-get install -y nodejs && \
    npm install -g @angular/cli

# Build stage
FROM with-node AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src

# Copy csproj files and restore .NET dependencies
COPY ["DOLS-Compulsory-2.Server/DOLS-Compulsory-2.Server.csproj", "DOLS-Compulsory-2.Server/"]
COPY ["dols-compulsory-2.client/dols-compulsory-2.client.esproj", "dols-compulsory-2.client/"]
RUN dotnet restore "./DOLS-Compulsory-2.Server/DOLS-Compulsory-2.Server.csproj"

# Copy everything else *except node_modules*
COPY . .

# Build Angular client
WORKDIR /src/dols-compulsory-2.client
RUN rm -rf node_modules && npm ci && npm run build

# Build .NET server
WORKDIR /src/DOLS-Compulsory-2.Server
RUN dotnet build "./DOLS-Compulsory-2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Publish stage
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./DOLS-Compulsory-2.Server.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Final runtime image
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "DOLS-Compulsory-2.Server.dll"]
