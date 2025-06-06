# Etapa de compilación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /src

# Copiar archivos de proyecto y restaurar dependencias
COPY *.sln .                               
COPY src/WebAPI/WebAPI.csproj src/WebAPI/
COPY src/Application/Application.csproj src/Application/
COPY src/Domain/Domain.csproj src/Domain/
COPY src/Infrastructure/Infrastructure.csproj src/Infrastructure/
COPY tests/WebAPI.Tests/WebAPI.Tests.csproj tests/WebAPI.Tests/
COPY tests/Application.Tests/Application.Tests.csproj tests/Application.Tests/

RUN dotnet restore

# Copiar el resto del código fuente
COPY . .

# Publicar la aplicación
RUN dotnet publish "src/WebAPI/WebAPI.csproj" -c Release -o /app/out

# Etapa de ejecución
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/out .
ENTRYPOINT ["dotnet", "WebAPI.dll"]

