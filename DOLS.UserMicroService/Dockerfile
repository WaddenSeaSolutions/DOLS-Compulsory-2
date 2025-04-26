# Brug en officiel .NET 9 SDK som build image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
WORKDIR /app

# Kopier projektfilen og gendan afhængigheder
COPY DOLS.UserMicroService/*.csproj ./
RUN dotnet restore

# Kopier resten af applikationen og byg den
COPY DOLS.UserMicroService/. ./
RUN dotnet publish -c Release -o /out

# Brug en officiel .NET 9 runtime som runtime image
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Kopier den publicerede applikation fra build image
COPY --from=build /out ./

# Eksponer porten, som applikationen lytter på
EXPOSE 8082

# Sæt miljøvariabler
ENV ASPNETCORE_URLS=http://+:8082
ENV ASPNETCORE_ENVIRONMENT=Production

# Start applikationen
ENTRYPOINT ["dotnet", "DOLS.UserMicroService.dll"]
