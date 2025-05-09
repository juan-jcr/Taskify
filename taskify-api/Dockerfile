FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar proyectos principales
COPY WebAPI/WebAPI.csproj WebAPI/
COPY Application/Application.csproj Application/
COPY Domain/Domain.csproj Domain/
COPY Infrastructure/Infrastructure.csproj Infrastructure/

# Restaurar dependencias
RUN dotnet restore WebAPI/WebAPI.csproj

# Copiar todo el código fuente
COPY . .

# Habilitar hot-reload para desarrollo
CMD dotnet watch run --project WebAPI/WebAPI.csproj --urls http://0.0.0.0:5000
