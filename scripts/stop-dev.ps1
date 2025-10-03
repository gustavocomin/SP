# Script para parar PostgreSQL de desenvolvimento
Write-Host "🛑 Parando PostgreSQL de desenvolvimento..." -ForegroundColor Yellow

# Parar PostgreSQL
docker-compose stop postgres

Write-Host "✅ PostgreSQL parado!" -ForegroundColor Green
Write-Host "💡 Para iniciar novamente, execute: .\scripts\start-dev.ps1" -ForegroundColor Cyan
