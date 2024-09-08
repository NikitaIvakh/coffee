FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8090
EXPOSE 8091

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["Services/IdentityService/Presentation/Identity.WebAPI/Identity.WebAPI.csproj", "Services/IdentityService/Presentation/Identity.WebAPI/"]
COPY ["Services/IdentityService/Core/Identity.Domain/Identity.Domain.csproj", "Services/IdentityService/Core/Identity.Domain/"]
COPY ["Services/IdentityService/Core/Identity.Application/Identity.Application.csproj", "Services/IdentityService/Core/Identity.Application/"]
COPY ["Services/IdentityService/Infrastructure/Identity.Infrastructure/Identity.Infrastructure.csproj", "Services/IdentityService/Infrastructure/Identity.Infrastructure/"]
RUN dotnet restore "Services/IdentityService/Presentation/Identity.WebAPI/Identity.WebAPI.csproj"
COPY . .
WORKDIR "/src/Services/IdentityService/Presentation/Identity.WebAPI"
RUN dotnet build "Identity.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "Identity.WebAPI.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Identity.WebAPI.dll"]
