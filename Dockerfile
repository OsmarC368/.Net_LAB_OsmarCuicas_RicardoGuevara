# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copiar los .csproj primero (capa de cache)
COPY ["Web/Web.csproj",                    "Web/"]
COPY ["Core/Core.csproj",                  "Core/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Services/Services.csproj",          "Services/"]

# Restaurar dependencias (usa el .sln si tienes referencias entre proyectos)
RUN dotnet restore "Web/Web.csproj"  
# O mejor aún si tienes .sln:
# COPY ./*.sln .
# RUN dotnet restore "TuSolution.sln"

# Copiar TODO el código
COPY . .

WORKDIR /src/Web
RUN dotnet build "Web.csproj" -c Release -o /app/build
#RUN dotnet publish "Web.csproj" -c Release -o /app/publish --no-restore
RUN dotnet publish "Web.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
EXPOSE 80
EXPOSE 443

COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "Web.dll"]