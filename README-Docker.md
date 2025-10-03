# 🐳 Sistema SP - Docker

Sistema completo para psicólogos rodando em containers Docker.

## 🚀 Início Rápido

### **Windows**
```bash
docker-start.bat
```

### **Linux/Mac**
```bash
./docker-start.sh
```

## 📦 Containers Incluídos

### **1. PostgreSQL Database**
- **Container**: `sp-postgres`
- **Porta**: `5432`
- **Database**: `sp_database`
- **Username**: `sp_user`
- **Password**: `sp_password123`

### **2. Evolution API (WhatsApp)**
- **Container**: `sp-evolution-api`
- **Porta**: `8080`
- **API Key**: `sp-evolution-key-2025`
- **Instance**: `psicologo`

### **3. SP.Api (.NET 9.0)**
- **Container**: `sp-api`
- **Portas**: `7169` (HTTP), `5231` (HTTPS)
- **Environment**: `Production`

## 🔧 Comandos Docker

### **Iniciar Sistema**
```bash
docker-compose up -d
```

### **Parar Sistema**
```bash
docker-compose down
```

### **Ver Logs**
```bash
# Todos os containers
docker-compose logs -f

# Container específico
docker-compose logs -f sp-api
docker-compose logs -f postgres
docker-compose logs -f evolution-api
```

### **Reconstruir**
```bash
docker-compose up --build -d
```

### **Status dos Containers**
```bash
docker-compose ps
```

## 📱 URLs Disponíveis

- **API**: http://localhost:7169
- **Swagger**: http://localhost:7169/swagger
- **Health Check**: http://localhost:7169/health
- **Evolution API**: http://localhost:8080
- **PostgreSQL**: localhost:5432

## 🔑 Configurações

### **Banco de Dados**
```
Host: localhost (ou postgres dentro do Docker)
Port: 5432
Database: sp_database
Username: sp_user
Password: sp_password123
```

### **WhatsApp Evolution API**
```
URL: http://localhost:8080
API Key: sp-evolution-key-2025
Instance: psicologo
```

## 📱 Conectar WhatsApp Real

### **1. Criar Instância**
```bash
curl -X POST http://localhost:8080/instance/create \
  -H "Content-Type: application/json" \
  -H "apikey: sp-evolution-key-2025" \
  -d '{"instanceName": "psicologo"}'
```

### **2. Gerar QR Code**
```bash
curl -X GET http://localhost:8080/instance/connect/psicologo \
  -H "apikey: sp-evolution-key-2025"
```

### **3. Escanear QR Code**
- Abra WhatsApp no seu celular
- Vá em **Dispositivos Conectados**
- Escaneie o QR Code retornado

### **4. Testar Envio**
```bash
curl -X POST "http://localhost:7169/api/whatsapp/enviar" \
  -H "Content-Type: application/json" \
  -d '{"telefone":"48991871479","mensagem":"Teste real do WhatsApp!","tipo":1}'
```

## 🔧 Troubleshooting

### **Container não inicia**
```bash
# Ver logs detalhados
docker-compose logs container-name

# Verificar status
docker-compose ps
```

### **Erro de conexão com banco**
```bash
# Verificar se PostgreSQL está rodando
docker-compose logs postgres

# Testar conexão
docker exec -it sp-postgres psql -U sp_user -d sp_database
```

### **Evolution API não responde**
```bash
# Verificar logs
docker-compose logs evolution-api

# Testar health
curl http://localhost:8080/health
```

### **Reconstruir tudo do zero**
```bash
docker-compose down -v
docker-compose up --build -d
```

## 📊 Monitoramento

### **Health Checks**
- **API**: http://localhost:7169/health
- **Evolution**: http://localhost:8080/health
- **PostgreSQL**: Automático via Docker

### **Logs em Tempo Real**
```bash
docker-compose logs -f --tail=100
```

## 🎯 Próximos Passos

1. ✅ **Sistema rodando** via Docker
2. ✅ **Conectar WhatsApp** real
3. ✅ **Testar cobrança mensal**
4. ✅ **Configurar backup** automático
5. ✅ **Deploy em produção**

## 🚀 Vantagens do Docker

- ✅ **Ambiente isolado** e consistente
- ✅ **Fácil deploy** em qualquer servidor
- ✅ **Backup simples** dos volumes
- ✅ **Escalabilidade** horizontal
- ✅ **Rollback rápido** de versões
- ✅ **Desenvolvimento** idêntico à produção

## ✅ Status Atual da Configuração

### **Containers Funcionando**
- ✅ **PostgreSQL**: Rodando e saudável na porta 5432
- ✅ **SP.Api**: Rodando e saudável na porta 7169
- ⚠️ **Evolution API**: Reiniciando (problema de configuração de banco)

### **Funcionalidades Testadas**
- ✅ **Migrações automáticas**: Executadas na inicialização
- ✅ **API Health Check**: http://localhost:7169/health
- ✅ **Swagger UI**: http://localhost:7169/swagger
- ✅ **Endpoints da API**: Funcionando corretamente
- ✅ **Banco de dados**: Tabelas criadas e funcionais

### **URLs Importantes**
- **API Principal**: http://localhost:7169
- **Swagger UI**: http://localhost:7169/swagger
- **Health Check**: http://localhost:7169/health
- **Evolution API**: http://localhost:8080 (quando funcionando)

## 🔧 Comandos Úteis

### **Parar todos os containers**
```bash
docker-compose down
```

### **Ver logs de um container específico**
```bash
docker-compose logs sp-api
docker-compose logs postgres
docker-compose logs evolution-api
```

### **Reconstruir e reiniciar**
```bash
docker-compose up --build -d
```

### **Verificar status dos containers**
```bash
docker-compose ps
```

### **Acessar container PostgreSQL**
```bash
docker exec -it sp-postgres psql -U sp_user -d sp_database
```

### **Backup do banco**
```bash
docker exec sp-postgres pg_dump -U sp_user sp_database > backup.sql
```

### **Restaurar backup**
```bash
docker exec -i sp-postgres psql -U sp_user -d sp_database < backup.sql
```
