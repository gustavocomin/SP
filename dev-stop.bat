@echo off
echo 🛑 Parando ambiente de desenvolvimento...

REM Parar PostgreSQL
docker-compose stop postgres

echo ✅ PostgreSQL parado!
echo 💡 Para iniciar novamente, execute: dev-setup.bat

pause
