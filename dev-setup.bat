@echo off
echo 🚀 Configurando ambiente de desenvolvimento...

REM Verificar se Docker está rodando
docker version >nul 2>&1
if %errorlevel% neq 0 (
    echo ❌ Docker não está rodando. Inicie o Docker Desktop primeiro.
    pause
    exit /b 1
)

echo ✅ Docker está rodando
echo 📦 Iniciando PostgreSQL...

REM Iniciar apenas PostgreSQL
docker-compose up -d postgres

echo ⏳ Aguardando PostgreSQL ficar pronto...
timeout /t 10 /nobreak >nul

REM Verificar se PostgreSQL está saudável
docker-compose ps postgres | findstr "healthy" >nul
if %errorlevel% equ 0 (
    echo ✅ PostgreSQL está pronto!
    echo 🎯 Agora você pode rodar o projeto no Visual Studio (F5)
    echo 📊 PostgreSQL disponível em: localhost:5432
    echo 🗄️ Database: sp_database ^| User: sp_user
) else (
    echo ⚠️ PostgreSQL pode ainda estar inicializando...
    echo 💡 Verifique os logs: docker-compose logs postgres
)

pause
