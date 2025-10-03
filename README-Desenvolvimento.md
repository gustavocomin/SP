# 🚀 Desenvolvimento - Sistema SP

## 🔧 Configuração Híbrida (PostgreSQL Docker + API Visual Studio)

### **🎯 Método Automático (Recomendado)**

#### **1. Iniciar Desenvolvimento**
```bash
# Duplo clique no arquivo ou execute no terminal
dev-setup.bat
```

#### **2. Rodar no Visual Studio**
- Abrir `SP.sln` no Visual Studio
- Pressionar `F5` ou clicar em ▶️ **Run**
- O PostgreSQL já estará rodando automaticamente!

#### **3. Parar Desenvolvimento**
```bash
# Duplo clique no arquivo ou execute no terminal
dev-stop.bat
```

---

### **🔧 Método Manual**

#### **Iniciar PostgreSQL**
```bash
docker-compose up -d postgres
```

#### **Verificar Status**
```bash
docker-compose ps postgres
```

#### **Parar PostgreSQL**
```bash
docker-compose stop postgres
```

---

### **✨ Funcionalidades Automáticas**

#### **Build Events Configurados**
- ✅ **PostgreSQL inicia automaticamente** antes do build
- ✅ **Não precisa lembrar** de iniciar manualmente
- ✅ **Desenvolvimento mais fluido**

#### **Override Docker Compose**
- ✅ **Apenas PostgreSQL** roda no Docker
- ✅ **API roda no Visual Studio** (debug completo)
- ✅ **Hot reload** funcionando

---

### **🔍 Verificar se Está Funcionando**

#### **1. PostgreSQL Rodando**
```bash
docker-compose ps
# Deve mostrar: sp-postgres Up (healthy)
```

#### **2. API Funcionando**
- Abrir: https://localhost:7169/swagger
- Testar endpoint: `/health`

#### **3. Conexão com Banco**
- Swagger deve carregar sem erros
- Endpoint `/api/clientes` deve retornar `[]`

---

### **🚨 Troubleshooting**

#### **Problema: "Connection refused"**
```bash
# Verificar PostgreSQL
docker-compose ps postgres

# Se não estiver rodando
docker-compose up -d postgres
```

#### **Problema: "Database does not exist"**
```bash
# Verificar logs
docker-compose logs postgres

# Recriar se necessário
docker-compose down postgres
docker-compose up -d postgres
```

#### **Problema: Porta 5432 ocupada**
```bash
# Verificar o que está usando a porta
netstat -ano | findstr :5432

# Parar outros PostgreSQL se necessário
```

---

### **📊 URLs Importantes**

- **API**: https://localhost:7169
- **Swagger**: https://localhost:7169/swagger
- **Health Check**: https://localhost:7169/health
- **PostgreSQL**: localhost:5432

### **🗄️ Credenciais do Banco**

- **Host**: localhost
- **Port**: 5432
- **Database**: sp_database
- **Username**: sp_user
- **Password**: sp_password123

---

### **🎉 Pronto para Desenvolver!**

Com essa configuração você tem:
- 🔥 **Hot reload** no Visual Studio
- 🐛 **Debug completo** com breakpoints
- 🗄️ **Banco consistente** no Docker
- ⚡ **Desenvolvimento ágil**
- 🚀 **Inicialização automática**
