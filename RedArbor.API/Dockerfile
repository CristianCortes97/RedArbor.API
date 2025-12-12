# ========== ETAPA 1: BUILD ==========
# Usar la imagen de .NET 8 SDK para compilar
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar archivos de proyecto (.csproj) primero para aprovechar cache de Docker
COPY ["RedArbor.API/RedArbor.API.csproj", "RedArbor.API/"]
COPY ["RedArbor.Application/RedArbor.Application.csproj", "RedArbor.Application/"]
COPY ["RedArbor.Domain/RedArbor.Domain.csproj", "RedArbor.Domain/"]
COPY ["RedArbor.Infraestructure/RedArbor.Infraestructure.csproj", "RedArbor.Infraestructure/"]

# Restaurar paquetes NuGet
RUN dotnet restore "RedArbor.API/RedArbor.API.csproj"

# Copiar todo el código fuente
COPY . .

# Compilar la aplicación
WORKDIR "/src/RedArbor.API"
RUN dotnet build "RedArbor.API.csproj" -c Release -o /app/build

# ========== ETAPA 2: PUBLISH ==========
# Publicar la aplicación
FROM build AS publish
RUN dotnet publish "RedArbor.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# ========== ETAPA 3: RUNTIME ==========
# Usar la imagen runtime más ligera para ejecutar
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app

# Exponer puertos
EXPOSE 80
EXPOSE 443

# Copiar los archivos publicados
COPY --from=publish /app/publish .

# Punto de entrada
ENTRYPOINT ["dotnet", "RedArbor.API.dll"]