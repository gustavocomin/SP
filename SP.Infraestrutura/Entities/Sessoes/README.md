# Entidade Sessões

Esta pasta contém todos os componentes relacionados à entidade **Sessão** do sistema.

## 📁 Estrutura

```
Sessoes/
├── Sessao.cs                    # Entidade de domínio (referência)
├── SessaoConfiguration.cs       # Configuração do Entity Framework
├── ISessaoRepository.cs         # Interface do repositório
├── SessaoRepository.cs          # Implementação do repositório
└── README.md                    # Este arquivo
```

## 🎯 Funcionalidades

### ✅ Gestão de Sessões
- **CRUD Completo**: Criar, ler, atualizar, remover sessões
- **Agendamento**: Controle de data/hora e duração
- **Status**: Agendada, Confirmada, Realizada, Cancelada, Falta, Reagendada
- **Periodicidade**: Diário, Bisemanal, Semanal, Quinzenal, Mensal, Livre
- **Relacionamento**: Vinculação com clientes

### ✅ Controle Financeiro
- **Valor por Sessão**: Configurável individualmente
- **Status de Pagamento**: Pago/Não pago
- **Forma de Pagamento**: PIX, Cartão, Dinheiro, Transferência
- **Faturamento**: Controle do que deve ser considerado

### ✅ Funcionalidades Avançadas
- **Reagendamento**: Manter histórico da sessão original
- **Conflito de Horário**: Verificação automática
- **Sessões Recorrentes**: Geração automática baseada na periodicidade
- **Anotações Clínicas**: Privadas do psicólogo
- **Soft Delete**: Remoção lógica preservando histórico

## 🔍 Consultas Disponíveis

### Por Cliente
- `ObterSessoesPorClienteAsync(clienteId)` - Todas as sessões do cliente
- `ObterSessoesNaoPagasPorClienteAsync(clienteId)` - Sessões não pagas
- `ObterSessoesClienteMesAsync(clienteId, ano, mes)` - Sessões do mês

### Por Período
- `ObterSessoesPorPeriodoAsync(dataInicio, dataFim)` - Sessões no período
- `ObterSessoesParaFaturamentoAsync(ano, mes)` - Para faturamento mensal

### Por Status
- `ObterSessoesPorStatusAsync(status)` - Por status específico
- `ObterSessoesHojeAsync()` - Sessões de hoje
- `ObterProximasSessoesAsync()` - Próximos 7 dias
- `ObterSessoesParaConfirmacaoAsync()` - Próximas 24h

### Financeiras
- `ObterSessoesNaoPagasAsync()` - Todas não pagas
- `ObterValorTotalPeriodoAsync(inicio, fim)` - Valor total do período
- `ObterValorTotalNaoPagasAsync()` - Valor total em aberto

## 🛠️ Operações em Lote

### Pagamentos
- `MarcarSessoesComoPagasAsync(ids, formaPagamento)` - Marcar múltiplas como pagas

### Cancelamentos
- `CancelarSessoesAsync(ids, motivo, status)` - Cancelar múltiplas sessões

### Geração Automática
- `GerarSessoesRecorrentesAsync(...)` - Criar sessões baseadas na periodicidade

## 📊 Estatísticas

- **Contadores**: Por status, por cliente
- **Valores**: Totais, médias, em aberto
- **Períodos**: Hoje, próximos dias, mês atual

## 🔧 Configurações do Entity Framework

### Índices Otimizados
- `ix_sessoes_cliente_id` - Consultas por cliente
- `ix_sessoes_data_hora_agendada` - Consultas por data
- `ix_sessoes_status` - Consultas por status
- `ix_sessoes_pago` - Consultas financeiras
- `ix_sessoes_cliente_data` - Único por cliente/data

### Relacionamentos
- **Cliente**: Obrigatório (Restrict)
- **Sessão Original**: Opcional para reagendamentos (Restrict)

### Validações
- **Data/Hora**: Obrigatória
- **Duração**: Padrão 50 minutos
- **Valor**: Precisão decimal (18,2)
- **Status**: Enum com padrão "Agendada"
- **Periodicidade**: Enum com padrão "Semanal"

## 🎯 Casos de Uso Principais

### 1. Agendamento Simples
```csharp
var sessao = new CriarSessaoDto
{
    ClienteId = 1,
    DataHoraAgendada = DateTime.Now.AddDays(1),
    Valor = 150.00m,
    Periodicidade = PeriodicidadeSessao.Semanal
};
```

### 2. Geração Recorrente
```csharp
var recorrente = new GerarSessoesRecorrentesDto
{
    ClienteId = 1,
    DataInicio = DateTime.Today,
    DataFim = DateTime.Today.AddMonths(3),
    Periodicidade = PeriodicidadeSessao.Semanal,
    HorarioSessao = new TimeSpan(14, 0, 0), // 14:00
    Valor = 150.00m
};
```

### 3. Controle Financeiro
```csharp
// Marcar como realizada
await MarcarComoRealizadaAsync(sessaoId, DateTime.Now, 50, "Sessão produtiva");

// Marcar como paga
await MarcarComoPagaAsync(sessaoId, "PIX");
```

## 📈 Métricas e Relatórios

### Dashboard
- Sessões hoje
- Próximas sessões
- Sessões não pagas
- Valor total em aberto

### Faturamento Mensal
- Sessões realizadas no mês
- Valor total faturado
- Sessões por cliente
- Formas de pagamento

## 🔄 Fluxo de Estados

```
Agendada → Confirmada → Realizada → [Paga]
    ↓           ↓           ↓
Cancelada   Cancelada   Cancelada
    ↓           ↓
Reagendada  Reagendada
```

## 🚀 Próximas Melhorias

- **Notificações**: Lembretes automáticos
- **Integração**: Calendário externo
- **Relatórios**: PDF para clientes
- **Analytics**: Padrões de agendamento
- **API Externa**: Sincronização com agenda
