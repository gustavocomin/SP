#!/bin/bash
set -e

echo "🚀 Iniciando SP.Api..."

# Iniciar aplicação (migrações são executadas automaticamente no Program.cs)
echo "🎯 Iniciando aplicação..."
exec dotnet SP.Api.dll
