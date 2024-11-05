FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 7002

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

ARG BUILD_CONFIGURATION=Release

COPY ["CardsService/CardsService.csproj", "CardsService/"]
COPY ["Shared/Shared.csproj", "Shared/"]
RUN dotnet restore "CardsService/CardsService.csproj"
COPY . .
WORKDIR "/src/CardsService"
RUN dotnet build "CardsService.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
RUN dotnet publish "CardsService.csproj" -c $BUILD_CONFIGURATION -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CardsService.dll"]