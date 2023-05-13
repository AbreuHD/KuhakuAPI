#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["K-haku API/K-haku API.csproj", "K-haku API/"]
COPY ["K-haku.Core.Application/K-haku.Core.Application.csproj", "K-haku.Core.Application/"]
COPY ["K-haku.Core.Domain/K-haku.Core.Domain.csproj", "K-haku.Core.Domain/"]
COPY ["K-haku.Infraestructure.Identity/K-haku.Infraestructure.Identity.csproj", "K-haku.Infraestructure.Identity/"]
COPY ["K-haku.Infraestructure.Persistence/K-haku.Infraestructure.Persistence.csproj", "K-haku.Infraestructure.Persistence/"]
COPY ["K-haku.Infraestructure.Shared/K-haku.Infraestructure.Shared.csproj", "K-haku.Infraestructure.Shared/"]
RUN dotnet restore "K-haku API/K-haku API.csproj"
COPY . .
WORKDIR "/src/K-haku API"
RUN dotnet build "K-haku API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "K-haku API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "K-haku API.dll"]