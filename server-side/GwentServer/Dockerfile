FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

COPY ["API/API.csproj", "API/"]
COPY ["Core/Core.csproj", "Core/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["DataAccess/DataAccess.csproj", "DataAccess/"]
RUN dotnet restore "API/API.csproj"
COPY . .

WORKDIR "/src/API"
RUN dotnet build "API.csproj" -c Debug -o /app/build

FROM build AS debug
RUN dotnet publish "API.csproj" -c Debug -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=debug /app/publish .

EXPOSE 80

ENTRYPOINT ["dotnet", "API.dll"]