# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081


# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["src/AccountMgmt.WebApi/AccountMgmt.WebApi.csproj", "src/AccountMgmt.WebApi/"]
COPY ["src/AccountMgmt.Application/AccountMgmt.Application.csproj", "src/AccountMgmt.Application/"]
COPY ["src/AccountMgmt.Infraestructure/AccountMgmt.Infraestructure.csproj", "src/AccountMgmt.Infraestructure/"]
COPY ["src/AccountMgmt.Domain/AccountMgmt.Domain.csproj", "src/AccountMgmt.Domain/"]
RUN dotnet restore "./src/AccountMgmt.WebApi/AccountMgmt.WebApi.csproj"
COPY . .
WORKDIR "/src/src/AccountMgmt.WebApi"
RUN dotnet build "./AccountMgmt.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AccountMgmt.WebApi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AccountMgmt.WebApi.dll"]