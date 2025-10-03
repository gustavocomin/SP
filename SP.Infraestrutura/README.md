# SP.Infraestrutura

Camada de infraestrutura do sistema SP (Sistema para Psicólogos), implementando o padrão Repository e configurações do Entity Framework Core.

## Estrutura Organizada por Entidade

```
SP.Infraestrutura/
├── Data/
│   └── Context/
│       └── SPContext.cs                    # DbContext principal
├── Entities/                               # 🆕 Organização por entidade
│   └── Clientes/
│       ├── ClienteConfiguration.cs        # Configuração EF
│       ├── ClienteRepository.cs           # Implementação do repositório
│       ├── IClienteRepository.cs          # Interface do repositório
│       └── README.md                      # Documentação específica
├── Common/                                 # 🆕 Componentes compartilhados
│   ├── Base/
│   │   ├── RepositoryBase.cs              # Repository base genérico
│   │   └── ConfigurationBase.cs           # Configuration base
│   └── Exceptions/
│       └── EntityNotFoundException.cs     # Exceções customizadas
├── UnitOfWork/
│   ├── IUnitOfWork.cs                     # Interface Unit of Work
│   └── UnitOfWork.cs                      # Implementação Unit of Work
├── DependencyInjection.cs                 # Configuração de DI
└── README.md
```

## Funcionalidades Implementadas

### SPContext
- Configuração automática de convenções (VARCHAR, DECIMAL)
- Aplicação automática de todas as configurações de entidades
- DbSet para Cliente (preparado para Sessao e Pagamento)

### ClienteConfiguration
- Mapeamento completo da entidade Cliente
- Índices otimizados para consultas frequentes:
  - CPF (único)
  - Email (único)
  - StatusFinanceiro
  - Nome + Ativo (composto)
- Configurações de tipos apropriados (DATE, DECIMAL, VARCHAR)
- Valores padrão para campos de auditoria

### RepositoryBase<T>
- Operações CRUD básicas
- Soft delete (remoção lógica)
- Métodos para entidades com propriedade "Ativo"
- Contadores e validações

### ClienteRepository
- Implementa IClienteRepository
- Consultas específicas para o domínio:
  - Busca por CPF/Email
  - Filtros por status financeiro
  - Clientes inadimplentes
  - Clientes com vencimento
- Validações de unicidade
- Estatísticas e relatórios
- Atualização automática de DataUltimaAtualizacao

## Como usar

### 1. Configuração na API/Startup

```csharp
// Program.cs ou Startup.cs
services.AddInfraestrutura(configuration);

// Ou com connection string direta
services.AddInfraestrutura("Server=...;Database=...;");
```

### 2. Injeção de Dependência

```csharp
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.UnitOfWork;

public class ClienteController : ControllerBase
{
    private readonly IClienteRepository _clienteRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ClienteController(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
    {
        _clienteRepository = clienteRepository;
        _unitOfWork = unitOfWork;
    }
}
```

### 3. Uso Moderno com Unit of Work

```csharp
// Criar cliente
var cliente = new Cliente { Nome = "João", CPF = "123.456.789-00" };
await _clienteRepository.AdicionarAsync(cliente);
await _unitOfWork.SaveChangesAsync();

// Buscar por CPF (com tratamento de exceção)
var cliente = await _clienteRepository.ObterPorCPFAsync("123.456.789-00");

// Buscar obrigatório (lança exceção se não encontrar)
var cliente = await _clienteRepository.ObterPorIdObrigatorioAsync(1);

// Clientes inadimplentes
var inadimplentes = await _clienteRepository.ObterClientesInadimplentesAsync();

// Operações em lote
var clientes = new List<Cliente> { cliente1, cliente2, cliente3 };
await _clienteRepository.AdicionarRangeAsync(clientes);
await _unitOfWork.SaveChangesAsync();

// Com transação
await _unitOfWork.ExecuteInTransactionAsync(async () =>
{
    await _clienteRepository.AdicionarAsync(cliente);
    await _clienteRepository.AdicionarAsync(outroCliente);
    await _unitOfWork.SaveChangesAsync();
});
```

## Migrations

Para criar e aplicar migrations:

```bash
# Adicionar migration (executar da pasta raiz do projeto)
dotnet ef migrations add NomeDaMigration --project SP.Infraestrutura --startup-project SP.Api

# Atualizar banco
dotnet ef database update --project SP.Infraestrutura --startup-project SP.Api

# Remover última migration
dotnet ef migrations remove --project SP.Infraestrutura --startup-project SP.Api
```

## Dependências

- Microsoft.EntityFrameworkCore (9.0.0)
- Microsoft.EntityFrameworkCore.SqlServer (9.0.0)
- Microsoft.EntityFrameworkCore.Tools (9.0.0)
- Microsoft.EntityFrameworkCore.Design (9.0.0)

## Melhorias Implementadas (Versão Moderna)

### ✅ Organização por Entidade
- **Coesão Alta**: Tudo relacionado a uma entidade fica junto
- **Escalabilidade**: Facilita crescimento do projeto
- **Manutenibilidade**: Mais fácil encontrar e modificar código
- **Trabalho em Equipe**: Menos conflitos, cada dev pode focar em uma entidade

### ✅ Sintaxe C# 12
- **Primary Constructors**: `ClienteRepository(SPContext context)`
- **Collection Expressions**: `List<T> entities = await query.ToListAsync()`
- **Expression-bodied members**: Métodos mais concisos

### ✅ Arquitetura Aprimorada
- **Unit of Work Pattern**: Separação de responsabilidades
- **Exception Handling**: `EntityNotFoundException` específica
- **ConfigurationBase**: AutoInclude automático para navegações
- **Repository Base**: Operações genéricas otimizadas

### ✅ Funcionalidades Adicionais
- **Operações em lote**: `AddRangeAsync`, `UpdateRange`, `RemoveRange`
- **Soft Delete**: Remoção lógica automática
- **Transações**: Suporte completo com rollback automático
- **Validações**: Métodos de existência e contagem

## Próximos Passos

1. Criar entidades Sessao e Pagamento
2. Implementar repositórios correspondentes
3. Configurar relacionamentos entre entidades
4. Adicionar mais índices conforme necessário
5. Implementar auditoria automática (CreatedBy, UpdatedBy)
6. Criar camada de Aplicação com DTOs
7. Implementar API REST
