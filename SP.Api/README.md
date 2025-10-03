# SP.Api - Sistema para Psicólogos

API REST para o Sistema de Gestão Financeira para Psicólogos, construída com ASP.NET Core 9.0 e PostgreSQL.

## 🚀 Configuração e Execução

### Pré-requisitos
- .NET 9.0 SDK
- PostgreSQL 12+ instalado e rodando
- Banco de dados `SistemaPsico` criado

### Connection String
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=SistemaPsico;Username=postgres;Password=postgres;Port=5432"
  }
}
```

### Executar a API
```bash
# Na pasta raiz do projeto
dotnet run --project SP.Api

# A API estará disponível em:
# http://localhost:5231
# https://localhost:7231
```

### Swagger/OpenAPI
Acesse a documentação interativa em:
- **Development**: http://localhost:5231/swagger

## 📊 Endpoints Disponíveis

### Clientes
| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/api/clientes` | Lista todos os clientes ativos |
| `GET` | `/api/clientes/{id}` | Obtém cliente por ID |
| `POST` | `/api/clientes` | Cria novo cliente |
| `PUT` | `/api/clientes/{id}` | Atualiza cliente |
| `DELETE` | `/api/clientes/{id}` | Remove cliente (soft delete) |
| `GET` | `/api/clientes/cpf/{cpf}` | Busca por CPF |
| `GET` | `/api/clientes/inadimplentes` | Lista clientes inadimplentes |
| `GET` | `/api/clientes/vencimento-hoje` | Clientes com vencimento hoje |
| `GET` | `/api/clientes/estatisticas` | Estatísticas gerais |

## 🧪 Testando a API

### 1. Listar Clientes
```bash
curl -X GET "http://localhost:5231/api/clientes"
```

### 2. Criar Cliente
```bash
curl -X POST "http://localhost:5231/api/clientes" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@email.com",
    "telefone": "(11) 99999-9999",
    "cpf": "123.456.789-00",
    "dataNascimento": "1985-05-15",
    "valorSessao": 150.00,
    "formaPagamentoPreferida": "PIX",
    "aceiteLgpd": true
  }'
```

### 3. Estatísticas
```bash
curl -X GET "http://localhost:5231/api/clientes/estatisticas"
```

## 🗄️ Banco de Dados

### Migrations
```bash
# Criar nova migration
dotnet ef migrations add NomeDaMigration --project SP.Infraestrutura --startup-project SP.Api

# Aplicar migrations
dotnet ef database update --project SP.Infraestrutura --startup-project SP.Api

# Remover última migration
dotnet ef migrations remove --project SP.Infraestrutura --startup-project SP.Api
```

### Estrutura da Tabela Clientes
- **Dados Pessoais**: Nome, Email, Telefone, CPF, Data de Nascimento
- **Endereço**: Estado, Cidade, CEP, Endereço, Bairro, Número, Complemento
- **Financeiro**: Valor da Sessão, Forma de Pagamento, Dia de Vencimento, Status
- **Controle**: Data de Cadastro, Data de Atualização, Ativo
- **Específicos**: Contato de Emergência, Profissão
- **LGPD**: Aceite de termos

## 🏗️ Arquitetura

### Camadas
- **SP.Api**: Apresentação (Controllers, Routing)
- **SP.Aplicacao**: Casos de uso (Services, DTOs, Validações)
- **SP.Infraestrutura**: Acesso a dados (Repositories, EF Core)
- **SP.Dominio**: Regras de negócio (Entities, Enums)

### Padrões Utilizados
- **Clean Architecture**: Separação clara de responsabilidades
- **Application Services**: Orquestração de casos de uso
- **Repository Pattern**: Abstração do acesso a dados
- **Unit of Work**: Controle de transações
- **AutoMapper**: Mapeamento entre DTOs e entidades
- **FluentValidation**: Validações expressivas
- **Result Pattern**: Padronização de retornos
- **Dependency Injection**: Inversão de controle
- **Entity Framework Core**: ORM para PostgreSQL

## 🔧 Funcionalidades Implementadas

### ✅ CRUD Completo de Clientes
- Criação, leitura, atualização e remoção
- Validações robustas com FluentValidation
- Soft delete (remoção lógica)
- Mapeamento automático entre DTOs e entidades

### ✅ Consultas Específicas
- Busca por CPF e Email
- Busca por nome (like)
- Clientes inadimplentes e em dia
- Clientes com vencimento (hoje e próximos dias)
- Estatísticas financeiras completas

### ✅ Validações Avançadas
- **CPF**: Formato e dígitos verificadores
- **Email**: Formato válido e unicidade
- **Telefone**: Formato brasileiro
- **Endereço**: Campos obrigatórios
- **Financeiro**: Valores e formas de pagamento
- **LGPD**: Aceite obrigatório

### ✅ Operações em Lote
- Ativar/desativar múltiplos clientes
- Atualizar status financeiro
- Tratamento de transações

## 🚀 Próximos Passos

### 1. Entidades Adicionais
- **Sessões**: Agendamento e controle de sessões
- **Pagamentos**: Controle financeiro detalhado
- **Profissionais**: Dados do psicólogo

### 2. Funcionalidades
- Autenticação e autorização
- Relatórios financeiros
- Notificações de vencimento
- Dashboard administrativo

### 3. Melhorias Técnicas
- DTOs para entrada/saída
- Validações com FluentValidation
- Logging estruturado
- Testes automatizados

## 📝 Logs e Monitoramento

A API está configurada para logar:
- Comandos SQL (em Development)
- Erros de aplicação
- Informações de inicialização

## 🔒 Segurança

### Implementado
- Validação de entrada
- Tratamento de exceções
- Soft delete para preservar dados

### A Implementar
- Autenticação JWT
- Autorização baseada em roles
- Rate limiting
- CORS configurado
