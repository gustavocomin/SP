# SP - Sistema para Psicólogos

Sistema de gestão financeira para psicólogos, desenvolvido com .NET 9.0, PostgreSQL e arquitetura limpa.

## 🎯 Objetivo

Facilitar o controle financeiro de sessões de psicoterapia, oferecendo:
- Gestão de clientes/pacientes
- Controle de sessões e agendamentos
- Acompanhamento financeiro
- Relatórios e estatísticas

## 🏗️ Arquitetura

O projeto segue os princípios de **Clean Architecture** com separação clara de responsabilidades:

```
SP/
├── SP.Api/                    # 🌐 Camada de Apresentação
│   ├── Controllers/           # Endpoints REST
│   ├── appsettings.json      # Configurações
│   └── README.md             # Documentação da API
├── SP.Aplicacao/             # 🎯 Camada de Aplicação
│   ├── DTOs/                 # Data Transfer Objects
│   ├── Services/             # Application Services
│   ├── Validators/           # Validações FluentValidation
│   ├── Mappings/             # AutoMapper Profiles
│   └── README.md             # Documentação da aplicação
├── SP.Infraestrutura/        # 🗄️ Camada de Infraestrutura
│   ├── Entities/             # Organizados por entidade
│   │   └── Clientes/         # Repository + Configuration
│   ├── Common/               # Componentes compartilhados
│   ├── Data/                 # DbContext e Migrations
│   ├── UnitOfWork/           # Controle de transações
│   └── README.md             # Documentação da infraestrutura
├── SP.Dominio/               # 🏛️ Camada de Domínio
│   ├── Clientes/             # Entidades de negócio
│   ├── Enums/                # Enumerações
│   └── README.md             # Documentação do domínio
└── SP.sln                    # Solution
```

## 🚀 Tecnologias

### Backend
- **.NET 9.0** - Framework principal
- **ASP.NET Core** - API REST
- **Entity Framework Core 9.0** - ORM
- **PostgreSQL** - Banco de dados
- **AutoMapper** - Mapeamento de objetos
- **FluentValidation** - Validações
- **Swagger/OpenAPI** - Documentação da API

### Padrões e Práticas
- **Clean Architecture** - Arquitetura limpa
- **Repository Pattern** - Abstração de dados
- **Unit of Work** - Controle de transações
- **Application Services** - Casos de uso
- **Result Pattern** - Padronização de retornos
- **Dependency Injection** - Inversão de controle
- **C# 12** - Sintaxe moderna

## 📊 Funcionalidades Implementadas

### ✅ Gestão de Clientes
- **CRUD Completo**: Criar, ler, atualizar, remover
- **Validações Robustas**: CPF, Email, Telefone, Endereço
- **Soft Delete**: Remoção lógica preservando histórico
- **Consultas Específicas**: Por CPF, Email, Nome
- **Filtros Financeiros**: Inadimplentes, Em dia, Vencimentos
- **Operações em Lote**: Ativar/Desativar múltiplos clientes

### ✅ Controle Financeiro
- **Status Financeiro**: Em dia, Pendente, Inadimplente, Suspenso
- **Valor por Sessão**: Configurável por cliente
- **Dia de Vencimento**: Controle personalizado
- **Formas de Pagamento**: PIX, Cartão, Dinheiro, Transferência
- **Estatísticas**: Totais, médias, vencimentos

### ✅ Validações e Segurança
- **CPF**: Validação de formato e dígitos verificadores
- **Email**: Formato válido e unicidade
- **LGPD**: Aceite obrigatório de termos
- **Dados Pessoais**: Validação completa de campos
- **Tratamento de Erros**: Respostas padronizadas

## 🗄️ Banco de Dados

### PostgreSQL
- **Host**: localhost
- **Porta**: 5432
- **Database**: SistemaPsico
- **Username**: postgres
- **Password**: postgres

### Migrations
```bash
# Aplicar migrations
dotnet ef database update --project SP.Infraestrutura --startup-project SP.Api

# Criar nova migration
dotnet ef migrations add NomeMigration --project SP.Infraestrutura --startup-project SP.Api
```

## 🚀 Como Executar

### Pré-requisitos
- .NET 9.0 SDK
- PostgreSQL 12+
- Git

### Passos
1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd SP
   ```

2. **Configure o banco**
   - Instale PostgreSQL
   - Crie database `SistemaPsico`
   - Configure connection string em `SP.Api/appsettings.json`

3. **Execute as migrations**
   ```bash
   dotnet ef database update --project SP.Infraestrutura --startup-project SP.Api
   ```

4. **Execute a aplicação**
   ```bash
   dotnet run --project SP.Api
   ```

5. **Acesse a API**
   - **Swagger**: http://localhost:5231/swagger
   - **API**: http://localhost:5231/api

## 📚 Documentação

Cada camada possui documentação específica:

- **[SP.Api/README.md](SP.Api/README.md)** - Endpoints e uso da API
- **[SP.Aplicacao/README.md](SP.Aplicacao/README.md)** - Services e DTOs
- **[SP.Infraestrutura/README.md](SP.Infraestrutura/README.md)** - Repositórios e EF
- **[SP.Dominio/README.md](SP.Dominio/README.md)** - Entidades e regras

## 🧪 Testes

### Testando a API
```bash
# Listar clientes
curl -X GET "http://localhost:5231/api/clientes"

# Criar cliente
curl -X POST "http://localhost:5231/api/clientes" \
  -H "Content-Type: application/json" \
  -d '{
    "nome": "João Silva",
    "email": "joao@email.com",
    "telefone": "(11) 99999-9999",
    "cpf": "123.456.789-09",
    "dataNascimento": "1985-05-15",
    "valorSessao": 150.00,
    "formaPagamentoPreferida": "PIX",
    "aceiteLgpd": true
  }'

# Estatísticas
curl -X GET "http://localhost:5231/api/clientes/estatisticas"
```

## 🎯 Próximos Passos

### 🔄 Entidades Planejadas
- **Sessões**: Agendamento e controle de sessões
- **Pagamentos**: Controle financeiro detalhado
- **Profissionais**: Dados do psicólogo

### 🚀 Funcionalidades Futuras
- **Autenticação JWT**: Login e autorização
- **Dashboard**: Interface administrativa
- **Relatórios**: Financeiros e estatísticos
- **Notificações**: Vencimentos e lembretes
- **API de CEP**: Preenchimento automático de endereço
- **Backup**: Rotinas de backup automático

### 🧪 Melhorias Técnicas
- **Testes Unitários**: Cobertura completa
- **Testes de Integração**: API e banco
- **Cache**: Redis para consultas frequentes
- **Logs**: Estruturados com Serilog
- **Monitoramento**: Health checks
- **Docker**: Containerização

## 📄 Licença

Este projeto está sob licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 👥 Contribuição

Contribuições são bem-vindas! Por favor:
1. Faça um fork do projeto
2. Crie uma branch para sua feature
3. Commit suas mudanças
4. Push para a branch
5. Abra um Pull Request

## 📞 Suporte

Para dúvidas ou suporte, abra uma issue no repositório.

---

**Desenvolvido com ❤️ para facilitar a gestão financeira de psicólogos**