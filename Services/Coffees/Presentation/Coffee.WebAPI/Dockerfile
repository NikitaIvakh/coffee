FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 7070
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/Presentation/Coffee.WebAPI/Coffee.WebAPI.csproj", "Services/Presentation/Coffee.WebAPI/"]
COPY ["Services/Infrastructure/Coffee.Infrastructure/Coffee.Infrastructure.csproj", "Services/Infrastructure/Coffee.Infrastructure/"]
COPY ["Services/Core/Coffee.Domain/Coffee.Domain.csproj", "Services/Core/Coffee.Domain/"]
COPY ["Services/Core/Coffee.Application/Coffee.Application.csproj", "Services/Core/Coffee.Application/"]
COPY ["cert.pfx", "/app/cert.pfx"]
RUN dotnet restore "Services/Presentation/Coffee.WebAPI/Coffee.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/Presentation/Coffee.WebAPI"
RUN dotnet build "Coffee.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Coffee.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Coffee.WebAPI.dll"]
