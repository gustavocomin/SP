# Sistema para Psicólogos (SP)

Sistema completo de gestão para psicólogos desenvolvido em **C# 12** com **.NET 9.0**, seguindo os princípios de **Clean Architecture** e **Domain-Driven Design**.

## 📋 Índice

- [🏗️ Arquitetura](#️-arquitetura)
- [🚀 Tecnologias](#-tecnologias)
- [📁 Estrutura do Projeto](#-estrutura-do-projeto)
- [🎯 Funcionalidades](#-funcionalidades)
- [🌍 Entidades de Localização](#-entidades-de-localização)
- [🐳 Docker](#-docker)
- [⚙️ Configuração](#️-configuração)
- [🔧 Desenvolvimento](#-desenvolvimento)
- [📊 API Endpoints](#-api-endpoints)
- [🧪 Testes](#-testes)
- [📈 Padrões Implementados](#-padrões-implementados)
- [🎯 Princípios Arquiteturais](#-princípios-arquiteturais)

---

## 🎯 Objetivo

Facilitar o controle completo de consultórios de psicologia, oferecendo:
- **Gestão de clientes/pacientes** com dados completos
- **Controle de sessões** com periodicidade e status
- **Acompanhamento financeiro** com relatórios detalhados
- **Calendário integrado** com Google Calendar
- **WhatsApp Business** para comunicação
- **Cobrança mensal** automatizada
- **Localização geográfica** com países, estados e cidades

## 🏗️ Arquitetura

O sistema segue a **Clean Architecture** com as seguintes camadas:

```
┌─────────────────────────────────────────────────────────────┐
│                        SP.Api                               │
│                   (Camada de Apresentação)                  │
│  Controllers • Middlewares • Filters • Configuration       │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                       SP.IoC                               │
│                (Injeção de Dependência)                    │
│     AutoMapper • FluentValidation • DI Configuration       │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                   SP.Aplicacao                             │
│                 (Camada de Aplicação)                      │
│    DTOs • Services • Validators • Mappings • Use Cases     │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                  SP.Dominio                                │
│                 (Camada de Domínio)                        │
│        Entities • Enums • Business Rules • Contracts       │
└─────────────────────┬───────────────────────────────────────┘
                      │
┌─────────────────────▼───────────────────────────────────────┐
│                SP.Infraestrutura                           │
│               (Camada de Infraestrutura)                   │
│  Repositories • DbContext • Configurations • External APIs │
└─────────────────────────────────────────────────────────────┘
```

### 📁 Estrutura Detalhada

```
SP/
├── SP.Api/                    # 🌐 Camada de Apresentação
│   ├── Controllers/           # Endpoints REST
│   │   ├── ClientesController.cs
│   │   ├── SessoesController.cs
│   │   ├── FinanceiroController.cs
│   │   ├── CalendarioController.cs
│   │   └── WhatsAppController.cs
│   ├── Program.cs            # Configuração da aplicação
│   └── appsettings.json      # Configurações
├── SP.IoC/                   # ⚙️ Injeção de Dependência
│   └── DependencyInjectionConfig.cs # Configuração centralizada
├── SP.Aplicacao/             # 🎯 Camada de Aplicação
│   ├── DTOs/                 # Data Transfer Objects
│   │   ├── Clientes/         # DTOs de Cliente
│   │   ├── Sessoes/          # DTOs de Sessão
│   │   ├── Financeiro/       # DTOs Financeiros
│   │   ├── Calendario/       # DTOs de Calendário
│   │   └── WhatsApp/         # DTOs de WhatsApp
│   ├── Services/             # Application Services
│   │   ├── ClienteAppService.cs
│   │   ├── SessaoAppService.cs
│   │   ├── FinanceiroAppService.cs
│   │   ├── CalendarioAppService.cs
│   │   └── WhatsAppService.cs
│   ├── Validators/           # FluentValidation
│   └── Mappings/             # AutoMapper Profiles
├── SP.Dominio/               # 🏛️ Camada de Domínio
│   ├── Clientes/             # Agregado Cliente
│   │   └── Cliente.cs
│   ├── Sessoes/              # Agregado Sessão
│   │   └── Sessao.cs
│   ├── Localizacao/          # Entidades de Localização
│   │   ├── Pais.cs
│   │   ├── Estado.cs
│   │   └── Cidade.cs
│   └── Enums/                # Enumerações
│       ├── StatusFinanceiro.cs
│       ├── StatusSessao.cs
│       ├── PeriodicidadeSessao.cs
│       └── FormaPagamento.cs
├── SP.Infraestrutura/        # 🗄️ Camada de Infraestrutura
│   ├── Entities/             # Organizados por entidade
│   │   ├── Clientes/         # Repository + Configuration
│   │   ├── Sessoes/          # Repository + Configuration
│   │   └── Localizacao/      # Repository + Configuration
│   ├── Common/               # Componentes compartilhados
│   ├── Data/                 # DbContext e Migrations
│   └── UnitOfWork/           # Controle de transações
└── docker/                   # 🐳 Configuração Docker
    ├── docker-compose.yml
    └── docker-compose.override.yml
```

## 🚀 Tecnologias

### Backend
- **.NET 9.0** - Framework principal
- **C# 12** - Linguagem com recursos modernos
- **ASP.NET Core** - API REST
- **Entity Framework Core 9.0** - ORM
- **PostgreSQL** - Banco de dados relacional
- **AutoMapper 12.0.1** - Mapeamento de objetos
- **FluentValidation 11.11.0** - Validações robustas
- **Swagger/OpenAPI** - Documentação da API

### Infraestrutura
- **Docker** - Containerização
- **Docker Compose** - Orquestração de serviços
- **PostgreSQL 15** - Banco de dados em container
- **Evolution API** - WhatsApp Business API

### Padrões e Práticas
- **Clean Architecture** - Arquitetura limpa
- **Domain-Driven Design (DDD)** - Design orientado ao domínio
- **Repository Pattern** - Abstração de dados
- **Unit of Work** - Controle de transações
- **Application Services** - Casos de uso
- **Result Pattern** - Padronização de retornos
- **Dependency Injection** - Inversão de controle
- **SOLID Principles** - Princípios de design

---

## 🎯 Funcionalidades

### ✅ Gestão de Clientes
- **CRUD Completo**: Criar, ler, atualizar, remover
- **Validações Robustas**: CPF, Email, Telefone, Endereço
- **Soft Delete**: Remoção lógica preservando histórico
- **Consultas Específicas**: Por CPF, Email, Nome
- **Filtros Financeiros**: Inadimplentes, Em dia, Vencimentos
- **Operações em Lote**: Ativar/Desativar múltiplos clientes
- **Localização**: Integração com países, estados e cidades

### ✅ Gestão de Sessões
- **CRUD Completo**: Agendamento, reagendamento, cancelamento
- **Periodicidade**: Diário, Bisemanal, Semanal, Quinzenal, Mensal, Livre
- **Status de Sessão**: Agendada, Realizada, Cancelada, Faltou
- **Controle de Pagamento**: Pago/Não pago por sessão
- **Sessões Recorrentes**: Geração automática baseada na periodicidade
- **Integração Google Calendar**: Sincronização de eventos
- **Auto-relacionamento**: Sessões originais e reagendadas

### ✅ Controle Financeiro
- **Status Financeiro**: Em dia, Pendente, Inadimplente, Suspenso
- **Valor por Sessão**: Configurável por cliente
- **Dia de Vencimento**: Controle personalizado
- **Formas de Pagamento**: PIX, Cartão, Dinheiro, Transferência
- **Relatórios Mensais/Anuais**: Receitas, estatísticas, comparativos
- **Dashboard Financeiro**: Métricas em tempo real
- **Cálculos Dinâmicos**: Baseados em dados de sessões

### ✅ Módulo Calendário
- **Visualização Semanal/Diária**: Interface de calendário
- **Detecção de Conflitos**: Prevenção de agendamentos sobrepostos
- **Estatísticas de Agenda**: Ocupação, horários livres
- **Integração Google Calendar**: Sincronização bidirecional
- **Filtros Avançados**: Por cliente, status, período

### ✅ Integração WhatsApp
- **Múltiplos Provedores**: WhatsApp Business API, Twilio, Evolution API
- **Cobrança Mensal**: Envio automático de mensagens de cobrança
- **Templates Personalizáveis**: Mensagens configuráveis
- **Histórico de Envios**: Controle de mensagens enviadas
- **Configuração Flexível**: Múltiplos provedores simultâneos

### ✅ Validações e Segurança
- **CPF**: Validação de formato e dígitos verificadores
- **Email**: Formato válido e unicidade
- **LGPD**: Aceite obrigatório de termos
- **Dados Pessoais**: Validação completa de campos
- **Tratamento de Erros**: Respostas padronizadas
- **Logs de Auditoria**: Rastreamento de alterações

---

## 🌍 Entidades de Localização

O sistema inclui um módulo completo de localização geográfica:

### 🌎 País (Pais)
```csharp
public class Pais
{
    public int Id { get; set; }
    public string Nome { get; set; }           // Brasil, Estados Unidos
    public string CodigoISO { get; set; }      // BR, US
    public string CodigoISO3 { get; set; }     // BRA, USA
    public string CodigoTelefone { get; set; } // +55, +1
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public virtual ICollection<Estado> Estados { get; set; }
}
```

### 🏛️ Estado (Estado)
```csharp
public class Estado
{
    public int Id { get; set; }
    public string Nome { get; set; }        // São Paulo, Rio de Janeiro
    public string Sigla { get; set; }       // SP, RJ
    public string? CodigoIBGE { get; set; } // Código IBGE
    public string? Regiao { get; set; }     // Sudeste, Nordeste
    public int PaisId { get; set; }
    public virtual Pais Pais { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
    public virtual ICollection<Cidade> Cidades { get; set; }
}
```

### 🏙️ Cidade (Cidade)
```csharp
public class Cidade
{
    public int Id { get; set; }
    public string Nome { get; set; }         // São Paulo, Rio de Janeiro
    public string? CodigoIBGE { get; set; }  // Código IBGE (7 dígitos)
    public string? CEP { get; set; }         // CEP principal
    public decimal? Latitude { get; set; }   // Coordenadas GPS
    public decimal? Longitude { get; set; }  // Coordenadas GPS
    public int? Populacao { get; set; }      // População estimada
    public decimal? Area { get; set; }       // Área em km²
    public int EstadoId { get; set; }
    public virtual Estado Estado { get; set; }
    public bool Ativo { get; set; }
    public DateTime DataCriacao { get; set; }
}
```

### 🔗 Integração com Cliente
```csharp
public class Cliente
{
    // ... outros campos

    // Nova estrutura com relacionamento
    public int? CidadeId { get; set; }
    public virtual Cidade? Cidade { get; set; }

    // Campos legados (mantidos por compatibilidade)
    public string? Estado { get; set; }
    public string? CidadeNome { get; set; }

    // ... outros campos de endereço
}
```

### 📊 Benefícios da Estrutura de Localização
- **Padronização**: Dados geográficos consistentes
- **Relacionamentos**: Hierarquia País → Estado → Cidade
- **Geolocalização**: Coordenadas GPS para mapeamento
- **Estatísticas**: Dados populacionais e geográficos
- **Compatibilidade**: Campos legados mantidos
- **Performance**: Índices otimizados para consultas
- **Integridade**: Chaves estrangeiras com restrições

---

## 🐳 Docker

### Configuração de Serviços

O sistema utiliza Docker Compose para orquestração dos serviços:

```yaml
services:
  postgres:          # PostgreSQL 15
    ports: ["5433:5432"]

  evolution-api:     # WhatsApp Business API
    ports: ["8080:8080"]

  sp-api:           # API .NET (desabilitada por padrão)
    ports: ["5000:8080"]
```

### Comandos Docker

```bash
# Iniciar PostgreSQL
docker-compose up -d postgres

# Verificar status
docker-compose ps

# Parar todos os serviços
docker-compose down

# Logs do PostgreSQL
docker-compose logs postgres

# Acessar PostgreSQL
docker exec -it sp-postgres psql -U sp_user -d sp_database
```

### Configuração de Desenvolvimento Híbrido

O projeto está configurado para **desenvolvimento híbrido**:
- **PostgreSQL**: Roda em Docker (porta 5433)
- **API .NET**: Roda no Visual Studio (porta 5000)
- **Evolution API**: Desabilitada por padrão

---

## ⚙️ Configuração

### Banco de Dados (PostgreSQL)
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Database=sp_database;Username=sp_user;Password=sp_password123;Port=5433"
  }
}
```

### Migrations Entity Framework

```bash
# Aplicar migrations (automático no startup)
cd SP.Api && dotnet run

# Aplicar migrations manualmente
cd SP.Api && dotnet ef database update --project ../SP.Infraestrutura

# Criar nova migration
cd SP.Api && dotnet ef migrations add NomeMigration --project ../SP.Infraestrutura

# Listar migrations
cd SP.Api && dotnet ef migrations list --project ../SP.Infraestrutura
```

### Startup Automático do PostgreSQL

O projeto está configurado para iniciar automaticamente o PostgreSQL antes do build:

```xml
<Target Name="StartPostgreSQL" BeforeTargets="Build">
  <Exec Command="docker-compose up -d postgres"
        ContinueOnError="true"
        WorkingDirectory="$(MSBuildProjectDirectory)\.." />
</Target>
```

---

## 🔧 Desenvolvimento

### Pré-requisitos
- **.NET 9.0 SDK**
- **Docker** e **Docker Compose**
- **Visual Studio 2022** ou **VS Code**
- **Git**

### Configuração do Ambiente

1. **Clone o repositório**
   ```bash
   git clone <url-do-repositorio>
   cd SP
   ```

2. **Inicie o PostgreSQL**
   ```bash
   docker-compose up -d postgres
   ```

3. **Execute a aplicação**
   ```bash
   cd SP.Api
   dotnet run
   ```

4. **Acesse a documentação**
   - Swagger: http://localhost:5000/swagger
   - API: http://localhost:5000/api

### Estrutura de Desenvolvimento

```bash
# Build do projeto
dotnet build

# Executar testes
dotnet test

# Restaurar dependências
dotnet restore

# Limpar build
dotnet clean
```
   ```

---

## 📊 API Endpoints

### 👥 Clientes (`/api/clientes`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/` | Listar todos os clientes |
| `GET` | `/{id}` | Buscar cliente por ID |
| `GET` | `/cpf/{cpf}` | Buscar cliente por CPF |
| `GET` | `/email/{email}` | Buscar cliente por Email |
| `GET` | `/inadimplentes` | Listar clientes inadimplentes |
| `GET` | `/vencimentos-hoje` | Clientes com vencimento hoje |
| `POST` | `/` | Criar novo cliente |
| `PUT` | `/{id}` | Atualizar cliente |
| `DELETE` | `/{id}` | Remover cliente (soft delete) |
| `PATCH` | `/{id}/ativar` | Ativar cliente |
| `PATCH` | `/{id}/desativar` | Desativar cliente |
| `PATCH` | `/ativar-lote` | Ativar múltiplos clientes |
| `PATCH` | `/desativar-lote` | Desativar múltiplos clientes |

### 📅 Sessões (`/api/sessoes`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/` | Listar todas as sessões |
| `GET` | `/{id}` | Buscar sessão por ID |
| `GET` | `/cliente/{clienteId}` | Sessões de um cliente |
| `GET` | `/periodo` | Sessões por período |
| `GET` | `/nao-pagas` | Sessões não pagas |
| `POST` | `/` | Criar nova sessão |
| `PUT` | `/{id}` | Atualizar sessão |
| `DELETE` | `/{id}` | Remover sessão |
| `PATCH` | `/{id}/marcar-pago` | Marcar como pago |
| `PATCH` | `/{id}/marcar-nao-pago` | Marcar como não pago |
| `PATCH` | `/{id}/reagendar` | Reagendar sessão |

### 💰 Financeiro (`/api/financeiro`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/relatorio-mensal/{ano}/{mes}` | Relatório mensal |
| `GET` | `/relatorio-anual/{ano}` | Relatório anual |
| `GET` | `/dashboard` | Dashboard financeiro |
| `GET` | `/comparativo-mensal` | Comparativo entre meses |

### 📆 Calendário (`/api/calendario`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/semana/{data}` | Visualização semanal |
| `GET` | `/dia/{data}` | Visualização diária |
| `GET` | `/conflitos` | Detectar conflitos |
| `GET` | `/estatisticas` | Estatísticas da agenda |
| `POST` | `/google/sync` | Sincronizar com Google Calendar |

### 📱 WhatsApp (`/api/whatsapp`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `POST` | `/enviar-cobranca-mensal` | Enviar cobrança mensal |
| `POST` | `/enviar-mensagem` | Enviar mensagem individual |
| `GET` | `/historico/{clienteId}` | Histórico de mensagens |
| `GET` | `/status` | Status da integração |

### 🔧 Sistema (`/api/sistema`)

| Método | Endpoint | Descrição |
|--------|----------|-----------|
| `GET` | `/health` | Health check |
| `GET` | `/info` | Informações do sistema |

---

## 🧪 Testes

### Estrutura de Testes

```bash
# Executar todos os testes
dotnet test

# Executar testes com cobertura
dotnet test --collect:"XPlat Code Coverage"

# Executar testes específicos
dotnet test --filter "ClassName=ClienteAppServiceTests"
```

### Tipos de Testes

- **Testes Unitários**: Lógica de negócio e validações
- **Testes de Integração**: Repositórios e banco de dados
- **Testes de API**: Endpoints e controllers
- **Testes de Performance**: Carga e stress

---

## 📈 Padrões Implementados

### 🏗️ Arquiteturais
- **Clean Architecture**: Separação de responsabilidades
- **Domain-Driven Design**: Modelagem orientada ao domínio
- **CQRS**: Separação de comandos e consultas (preparado)
- **Event Sourcing**: Auditoria de eventos (preparado)

### 🔧 Estruturais
- **Repository Pattern**: Abstração de acesso a dados
- **Unit of Work**: Controle de transações
- **Application Service**: Orquestração de casos de uso
- **Result Pattern**: Padronização de retornos
- **Factory Pattern**: Criação de objetos complexos

### 🎯 Comportamentais
- **Strategy Pattern**: Múltiplos provedores WhatsApp
- **Observer Pattern**: Eventos de domínio (preparado)
- **Command Pattern**: Operações encapsuladas
- **Specification Pattern**: Consultas complexas (preparado)

---

## 🎯 Princípios Arquiteturais

### Clean Architecture
- **Independência de Frameworks**: O domínio não depende de frameworks
- **Testabilidade**: Cada camada pode ser testada isoladamente
- **Independência de UI**: A lógica não depende da interface
- **Independência de Banco**: O domínio não conhece o banco de dados
- **Independência de Agentes Externos**: Regras de negócio isoladas

### Domain-Driven Design (DDD)
- **Linguagem Ubíqua**: Termos do negócio no código
- **Agregados**: Cliente e Sessão como agregados
- **Entidades**: Com identidade e ciclo de vida
- **Value Objects**: Enums e objetos imutáveis
- **Repositórios**: Abstração de persistência

### SOLID Principles
- **S**ingle Responsibility: Cada classe tem uma responsabilidade
- **O**pen/Closed: Aberto para extensão, fechado para modificação
- **L**iskov Substitution: Subtipos substituíveis
- **I**nterface Segregation: Interfaces específicas
- **D**ependency Inversion: Dependências invertidas

---

## 🚀 Próximos Passos

### 🔄 Melhorias Planejadas
- **CQRS**: Command Query Responsibility Segregation
- **Event Sourcing**: Para auditoria completa
- **Domain Events**: Para notificações
- **Microservices**: Para escalabilidade
- **API Gateway**: Para roteamento
- **Message Queues**: Para processamento assíncrono

### 📱 Funcionalidades Futuras
- **App Mobile**: React Native ou Flutter
- **Relatórios Avançados**: Power BI integration
- **IA/ML**: Análise preditiva de agendamentos
- **Multi-tenancy**: Suporte a múltiplos consultórios
- **Backup Automático**: Estratégia de backup
- **Monitoramento**: Application Insights

---

**Este sistema garante uma solução robusta, testável e evolutiva para o gerenciamento completo de consultórios de psicologia** 🏗️

---

## 📄 Licença

Este projeto está sob a licença MIT. Veja o arquivo [LICENSE](LICENSE) para mais detalhes.

## 🤝 Contribuição

Contribuições são bem-vindas! Por favor, leia o [CONTRIBUTING.md](CONTRIBUTING.md) para detalhes sobre nosso código de conduta e o processo para enviar pull requests.

## 📞 Suporte

Para suporte, abra uma issue no GitHub ou entre em contato através do email: suporte@sp-sistema.com

