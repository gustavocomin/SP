# Script para iniciar PostgreSQL para desenvolvimento
Write-Host "🚀 Iniciando PostgreSQL para desenvolvimento..." -ForegroundColor Green

# Verificar se Docker está rodando
try {
    docker version | Out-Null
    Write-Host "✅ Docker está rodando" -ForegroundColor Green
} catch {
    Write-Host "❌ Docker não está rodando. Inicie o Docker Desktop primeiro." -ForegroundColor Red
    exit 1
}

# Iniciar apenas PostgreSQL
Write-Host "📦 Iniciando PostgreSQL..." -ForegroundColor Yellow
docker-compose up -d postgres

# Aguardar PostgreSQL ficar saudável
Write-Host "⏳ Aguardando PostgreSQL ficar pronto..." -ForegroundColor Yellow
$timeout = 30
$elapsed = 0

do {
    Start-Sleep -Seconds 2
    $elapsed += 2
    $status = docker-compose ps postgres --format "table {{.Health}}" | Select-String "healthy"
    
    if ($status) {
        Write-Host "✅ PostgreSQL está pronto!" -ForegroundColor Green
        break
    }
    
    if ($elapsed -ge $timeout) {
        Write-Host "⚠️ Timeout aguardando PostgreSQL. Verifique os logs:" -ForegroundColor Yellow
        docker-compose logs postgres
        break
    }
    
    Write-Host "⏳ Aguardando... ($elapsed/$timeout segundos)" -ForegroundColor Yellow
} while ($true)

Write-Host "🎯 Pronto! Agora você pode rodar o projeto no Visual Studio (F5)" -ForegroundColor Green
Write-Host "📊 PostgreSQL disponível em: localhost:5432" -ForegroundColor Cyan
Write-Host "🗄️ Database: sp_database | User: sp_user" -ForegroundColor Cyan
