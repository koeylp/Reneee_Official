#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Reneee.API/Reneee.API.csproj", "Reneee.API/"]
COPY ["Reneee.Application/Reneee.Application.csproj", "Reneee.Application/"]
COPY ["Reneee.Domain/Reneee.Domain.csproj", "Reneee.Domain/"]
COPY ["Reneee.Persistence/Reneee.Persistence.csproj", "Reneee.Persistence/"]
RUN dotnet restore "./Reneee.API/Reneee.API.csproj"
COPY . .
WORKDIR "/src/Reneee.API"
RUN dotnet build "./Reneee.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./Reneee.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Reneee.API.dll"]