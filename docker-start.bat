@echo off
echo 🐳 Iniciando Sistema SP com Docker...
echo.

echo 📋 Verificando Docker...
docker --version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker não está instalado ou não está rodando!
    echo    Instale o Docker Desktop: https://www.docker.com/products/docker-desktop
    pause
    exit /b 1
)

echo ✅ Docker encontrado!
echo.

echo 🛑 Parando containers existentes...
docker-compose down

echo.
echo 🏗️ Construindo e iniciando containers...
docker-compose up --build -d

echo.
echo ⏳ Aguardando containers ficarem prontos...
timeout /t 10 /nobreak >nul

echo.
echo 📊 Status dos containers:
docker-compose ps

echo.
echo 🎉 Sistema iniciado com sucesso!
echo.
echo 📱 URLs disponíveis:
echo    • API: http://localhost:7169
echo    • Swagger: http://localhost:7169/swagger
echo    • Evolution API: http://localhost:8080
echo    • PostgreSQL: localhost:5432
echo.
echo 🔑 Credenciais do banco:
echo    • Database: sp_database
echo    • Username: sp_user
echo    • Password: sp_password123
echo.
echo 📱 WhatsApp Evolution API:
echo    • API Key: sp-evolution-key-2025
echo    • Instance: psicologo
echo.
echo 💡 Para conectar WhatsApp:
echo    1. Acesse: http://localhost:8080
echo    2. Crie instância: POST /instance/create
echo    3. Gere QR Code: GET /instance/connect/psicologo
echo    4. Escaneie com seu WhatsApp
echo.
echo ⚠️  Para parar o sistema: docker-compose down
echo.
pause
