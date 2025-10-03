# Entities - Organização por Entidade

Esta pasta contém todas as entidades do sistema, organizadas por domínio. Cada entidade tem sua própria pasta com todos os arquivos relacionados.

## Estrutura Padrão

Cada entidade deve seguir esta estrutura:

```
Entities/
└── [NomeEntidade]/
    ├── [Entidade]Configuration.cs    # Configuração EF
    ├── [Entidade]Repository.cs       # Implementação do repositório
    ├── I[Entidade]Repository.cs      # Interface do repositório
    └── README.md                     # Documentação específica
```

## Entidades Implementadas

### ✅ Clientes
- **Pasta**: `Clientes/`
- **Descrição**: Gestão de clientes/pacientes do psicólogo
- **Funcionalidades**: CRUD, validações de CPF/Email, consultas por status financeiro

## Como Adicionar Nova Entidade

### 1. Criar a Pasta
```bash
mkdir SP.Infraestrutura/Entities/[NomeEntidade]
```

### 2. Criar a Configuration
```csharp
// [Entidade]Configuration.cs
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SP.Dominio.[Entidade];
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.[NomeEntidade]
{
    public class [Entidade]Configuration : ConfigurationBase<[Entidade]>
    {
        public override void ConfigurarEntidade(EntityTypeBuilder<[Entidade]> builder)
        {
            builder.ToTable("[NomeTabela]");
            
            builder.HasKey(e => e.Id);
            
            // Configurações específicas...
        }
    }
}
```

### 3. Criar a Interface do Repository
```csharp
// I[Entidade]Repository.cs
using SP.Dominio.[Entidade];

namespace SP.Infraestrutura.Entities.[NomeEntidade]
{
    public interface I[Entidade]Repository
    {
        // Métodos básicos herdados do RepositoryBase
        Task<[Entidade]?> ObterPorIdAsync(int id);
        Task<[Entidade]> ObterPorIdObrigatorioAsync(int id);
        Task<List<[Entidade]>> ObterTodosAsync();
        // ... outros métodos básicos
        
        // Métodos específicos da entidade
        Task<List<[Entidade]>> ObterPor[Criterio]Async([Tipo] criterio);
    }
}
```

### 4. Criar o Repository
```csharp
// [Entidade]Repository.cs
using SP.Dominio.[Entidade];
using SP.Infraestrutura.Data.Context;
using SP.Infraestrutura.Common.Base;

namespace SP.Infraestrutura.Entities.[NomeEntidade]
{
    public class [Entidade]Repository(SPContext context) 
        : RepositoryBase<[Entidade]>(context), I[Entidade]Repository
    {
        // Implementar métodos específicos
        public async Task<List<[Entidade]>> ObterPor[Criterio]Async([Tipo] criterio) =>
            await DbSet.Where(e => e.[Propriedade] == criterio).ToListAsync();
    }
}
```

### 5. Registrar no DependencyInjection
```csharp
// DependencyInjection.cs
services.AddScoped<I[Entidade]Repository, [Entidade]Repository>();
```

### 6. Adicionar DbSet no Context
```csharp
// SPContext.cs
public DbSet<[Entidade]> [NomeEntidade] { get; set; }
```

## Vantagens desta Organização

### ✅ **Coesão Alta**
- Tudo relacionado a uma entidade fica junto
- Facilita manutenção e evolução

### ✅ **Escalabilidade**
- Cada entidade é independente
- Facilita trabalho em equipe

### ✅ **Descoberta de Código**
- Mais fácil encontrar arquivos relacionados
- Padrão intuitivo para novos desenvolvedores

### ✅ **Preparação para Microserviços**
- Cada pasta pode virar um microserviço no futuro
- Facilita extração de contextos

## Próximas Entidades Sugeridas

### 🔄 Sessoes
- Agendamento e controle de sessões
- Relacionamento com Cliente
- Status da sessão (Agendada, Realizada, Cancelada)

### 🔄 Pagamentos
- Controle financeiro das sessões
- Relacionamento com Cliente e Sessao
- Formas de pagamento e status

### 🔄 Profissionais
- Dados do psicólogo
- Configurações e preferências
- Agenda e disponibilidade
