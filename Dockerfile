# Build Stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution file
COPY SP.sln ./

# Copy project files
COPY SP.Dominio/SP.Dominio.csproj SP.Dominio/
COPY SP.Infraestrutura/SP.Infraestrutura.csproj SP.Infraestrutura/
COPY SP.Aplicacao/SP.Aplicacao.csproj SP.Aplicacao/
COPY SP.Api/SP.Api.csproj SP.Api/

# Restore dependencies
RUN dotnet restore

# Copy source code
COPY . .

# Install EF Core tools
RUN dotnet tool install --global dotnet-ef
ENV PATH="$PATH:/root/.dotnet/tools"

# Build application
WORKDIR /src/SP.Api
RUN dotnet build -c Release -o /app/build

# Publish application
RUN dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS runtime
WORKDIR /app

# Install curl for health checks
RUN apt-get update && apt-get install -y curl && rm -rf /var/lib/apt/lists/*

# Copy published application
COPY --from=build /app/publish .

# Copy source for migrations
COPY --from=build /src ./src

# Copy entrypoint script
COPY entrypoint.sh ./
RUN chmod +x entrypoint.sh

# Create non-root user
RUN adduser --disabled-password --gecos '' appuser && chown -R appuser /app
USER appuser

# Expose ports
EXPOSE 8080
EXPOSE 8081

# Health check
HEALTHCHECK --interval=30s --timeout=10s --start-period=5s --retries=3 \
  CMD curl -f http://localhost:8080/health || exit 1

# Entry point
ENTRYPOINT ["./entrypoint.sh"]
