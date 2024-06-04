FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Workshop.API/Workshop.API.csproj", "Workshop.API/"]
COPY ["Workshop.Application/Workshop.Application.csproj", "Workshop.Application/"]
COPY ["Workshop.Domain/Workshop.Domain.csproj", "Workshop.Domain/"]
COPY ["Workshop.Infrastructure/Workshop.Infrastructure.csproj", "Workshop.Infrastructure/"]
RUN dotnet restore "Workshop.API/Workshop.API.csproj"
COPY . .
WORKDIR "/src/Workshop.API"
RUN dotnet build "Workshop.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Workshop.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Workshop.API.dll"]
