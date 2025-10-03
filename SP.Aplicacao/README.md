# SP.Aplicacao - Camada de Aplicação

Esta camada implementa os casos de uso e orquestra as operações de negócio do sistema. Ela atua como intermediária entre a API e a Infraestrutura, fornecendo uma interface limpa e validada para as operações.

## 🏗️ Arquitetura

### Responsabilidades
- **Orquestração**: Coordena operações entre diferentes repositórios
- **Validação**: Valida dados de entrada usando FluentValidation
- **Mapeamento**: Converte entre DTOs e entidades de domínio
- **Tratamento de Erros**: Padroniza respostas de erro
- **Transações**: Gerencia transações através do Unit of Work

### Padrões Implementados
- **Application Services**: Serviços de aplicação para cada agregado
- **DTOs**: Objetos de transferência de dados
- **AutoMapper**: Mapeamento automático entre objetos
- **FluentValidation**: Validações fluentes e expressivas
- **Result Pattern**: Padronização de retornos com sucesso/erro

## 📁 Estrutura

```
SP.Aplicacao/
├── DTOs/                           # Data Transfer Objects
│   ├── Common/
│   │   └── ResultadoDto.cs         # Padrão de resultado
│   └── Clientes/
│       ├── ClienteDto.cs           # DTO completo
│       ├── ClienteResumoDto.cs     # DTO resumido
│       ├── CriarClienteDto.cs      # DTO para criação
│       ├── AtualizarClienteDto.cs  # DTO para atualização
│       └── EstatisticasClientesDto.cs # DTO de estatísticas
├── Services/                       # Application Services
│   ├── Interfaces/
│   │   └── IClienteAppService.cs   # Interface do serviço
│   └── ClienteAppService.cs        # Implementação do serviço
├── Validators/                     # Validações FluentValidation
│   └── Clientes/
│       ├── CriarClienteValidator.cs
│       └── AtualizarClienteValidator.cs
├── Mappings/                       # Profiles do AutoMapper
│   └── ClienteProfile.cs
├── DependencyInjection.cs          # Configuração de DI
└── README.md
```

## 🎯 DTOs (Data Transfer Objects)

### ClienteDto
DTO completo com todos os dados do cliente para exibição.

### ClienteResumoDto
DTO resumido para listagens e consultas rápidas.

### CriarClienteDto
DTO para criação de novos clientes (sem ID, sem campos de controle).

### AtualizarClienteDto
DTO para atualização de clientes existentes (com ID, sem campos de controle).

### EstatisticasClientesDto
DTO com estatísticas agregadas dos clientes.

### ResultadoDto<T>
Padrão de resultado que encapsula:
- **Sucesso**: Indica se a operação foi bem-sucedida
- **Dados**: Dados retornados (quando sucesso)
- **Mensagem**: Mensagem informativa
- **Erros**: Lista de erros (quando falha)

## ✅ Validações

### CriarClienteValidator
- **Nome**: Obrigatório, 2-100 caracteres
- **Email**: Obrigatório, formato válido, máximo 100 caracteres
- **Telefone**: Obrigatório, formato (XX) XXXXX-XXXX
- **CPF**: Obrigatório, formato XXX.XXX.XXX-XX, validação de dígitos
- **Data Nascimento**: Obrigatória, anterior a hoje, máximo 120 anos
- **Endereço**: Todos os campos obrigatórios com tamanhos específicos
- **Valor Sessão**: Maior que zero, máximo R$ 9.999,99
- **Forma Pagamento**: Valores válidos (DINHEIRO, PIX, etc.)
- **Dia Vencimento**: Entre 1 e 31
- **LGPD**: Aceite obrigatório

### AtualizarClienteValidator
Mesmas validações do CriarClienteValidator, plus:
- **ID**: Obrigatório, maior que zero
- **Status Financeiro**: Enum válido

## 🔄 Mapeamentos (AutoMapper)

### Cliente ↔ DTOs
```csharp
// Entidade -> DTO
Cliente -> ClienteDto
Cliente -> ClienteResumoDto

// DTO -> Entidade
CriarClienteDto -> Cliente (com defaults)
AtualizarClienteDto -> Cliente (preservando campos de controle)
```

## 🚀 Application Services

### IClienteAppService
Interface que define todos os casos de uso relacionados a clientes:

#### CRUD Básico
- `ObterTodosAsync()`: Lista todos os clientes ativos
- `ObterPorIdAsync(id)`: Obtém cliente por ID
- `CriarAsync(dto)`: Cria novo cliente
- `AtualizarAsync(dto)`: Atualiza cliente existente
- `RemoverAsync(id)`: Remove cliente (soft delete)

#### Consultas Específicas
- `ObterPorCPFAsync(cpf)`: Busca por CPF
- `ObterPorEmailAsync(email)`: Busca por email
- `BuscarPorNomeAsync(nome)`: Busca por nome (like)

#### Consultas Financeiras
- `ObterInadimplentesAsync()`: Clientes inadimplentes
- `ObterEmDiaAsync()`: Clientes em dia
- `ObterComVencimentoHojeAsync()`: Vencimento hoje
- `ObterComVencimentoProximosDiasAsync(dias)`: Vencimento próximo

#### Estatísticas
- `ObterEstatisticasAsync()`: Estatísticas completas

#### Operações em Lote
- `AtivarClientesAsync(ids)`: Ativa múltiplos clientes
- `DesativarClientesAsync(ids)`: Desativa múltiplos clientes
- `AtualizarStatusFinanceiroAsync(id, status)`: Atualiza status

## 🛡️ Tratamento de Erros

### Tipos de Erro Tratados
1. **Validação**: Erros de validação de entrada
2. **Negócio**: Regras de negócio violadas (CPF duplicado, etc.)
3. **Não Encontrado**: Entidades não localizadas
4. **Sistema**: Erros inesperados do sistema

### Padrão de Resposta
```csharp
// Sucesso
{
  "sucesso": true,
  "dados": { ... },
  "mensagem": "Operação realizada com sucesso",
  "erros": []
}

// Erro
{
  "sucesso": false,
  "dados": null,
  "mensagem": null,
  "erros": ["Erro 1", "Erro 2"]
}
```

## 🔧 Configuração

### No Program.cs da API
```csharp
using SP.Aplicacao;

// Adicionar a camada de aplicação
builder.Services.AddAplicacao();
```

### Dependências Registradas
- **AutoMapper**: Mapeamento automático
- **FluentValidation**: Validadores
- **Application Services**: Serviços de aplicação

## 📊 Exemplo de Uso

### Na API Controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class ClientesController(IClienteAppService clienteAppService) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> ObterTodos()
    {
        var resultado = await clienteAppService.ObterTodosAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarClienteDto dto)
    {
        var resultado = await clienteAppService.CriarAsync(dto);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return CreatedAtAction(nameof(ObterPorId), 
            new { id = resultado.Dados!.Id }, resultado.Dados);
    }
}
```

## 🎯 Vantagens da Arquitetura

### ✅ **Separação de Responsabilidades**
- API: Apenas apresentação e roteamento
- Aplicação: Orquestração e validação
- Infraestrutura: Acesso a dados

### ✅ **Testabilidade**
- Services facilmente testáveis
- Mocks simples das interfaces
- Validações isoladas

### ✅ **Reutilização**
- Services podem ser usados por diferentes APIs
- DTOs padronizados
- Validações centralizadas

### ✅ **Manutenibilidade**
- Código organizado por responsabilidade
- Fácil localização de funcionalidades
- Padrões consistentes

## 🚀 Próximos Passos

1. **Adicionar mais entidades** (Sessao, Pagamento)
2. **Implementar cache** para consultas frequentes
3. **Adicionar logs estruturados** nos services
4. **Criar testes unitários** para os services
5. **Implementar paginação** nas consultas de lista
