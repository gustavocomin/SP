# Cliente - Entidade

Esta pasta contém todos os arquivos relacionados à entidade **Cliente** do sistema SP.

## Arquivos

### `ClienteConfiguration.cs`
- Configuração do Entity Framework para a entidade Cliente
- Mapeamento de propriedades, índices e constraints
- Herda de `ConfigurationBase<Cliente>` para AutoInclude automático

### `ClienteRepository.cs`
- Implementação do repositório para operações de dados do Cliente
- Herda de `RepositoryBase<Cliente>` para operações básicas
- Implementa `IClienteRepository` com métodos específicos do domínio

### `IClienteRepository.cs`
- Interface que define o contrato do repositório Cliente
- Métodos específicos para consultas de negócio (CPF, Email, Status, etc.)

## Funcionalidades Específicas

### Consultas de Negócio
- Busca por CPF e Email
- Filtros por status financeiro
- Clientes inadimplentes
- Clientes com vencimento específico
- Busca por nome

### Validações
- Verificação de existência de CPF/Email
- Validações para evitar duplicatas

### Estatísticas
- Contagem por status
- Valor total de sessões
- Relatórios financeiros

## Exemplo de Uso

```csharp
// Injeção de dependência
public class ClienteService(IClienteRepository clienteRepository, IUnitOfWork unitOfWork)
{
    public async Task<Cliente> CriarAsync(Cliente cliente)
    {
        // Validar CPF único
        if (await clienteRepository.ExisteCPFAsync(cliente.CPF))
            throw new InvalidOperationException("CPF já cadastrado");

        await clienteRepository.AdicionarAsync(cliente);
        await unitOfWork.SaveChangesAsync();
        return cliente;
    }

    public async Task<List<Cliente>> ObterInadimplentesAsync() =>
        await clienteRepository.ObterClientesInadimplentesAsync();
}
```

## Configurações do Banco

### Índices Criados
- `IX_Clientes_CPF` (único)
- `IX_Clientes_Email` (único)
- `IX_Clientes_StatusFinanceiro`
- `IX_Clientes_Ativo`
- `IX_Clientes_Nome_Ativo` (composto)

### Campos com Valores Padrão
- `DataCadastro`: GETDATE()
- `StatusFinanceiro`: EmDia (1)
- `Ativo`: true
- `AceiteLgpd`: false
