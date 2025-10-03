# Módulo Financeiro

Este módulo implementa relatórios financeiros completos baseados nos dados das sessões, sem necessidade de tabelas adicionais.

## 📊 Funcionalidades

### ✅ Relatórios Mensais
- **Faturamento**: Valor total faturado no mês
- **Pagamentos**: Valor total pago no mês
- **A Receber**: Valor pendente de pagamento
- **Estatísticas**: Percentual de pagamento, valor médio por sessão
- **Detalhamento por Cliente**: Quantidade, valores e última data de pagamento
- **Formas de Pagamento**: Distribuição por PIX, Cartão, Dinheiro, etc.
- **Sessões Não Pagas**: Lista com dias em atraso

### ✅ Relatórios Anuais
- **Consolidação Anual**: Totais do ano completo
- **Melhor Mês**: Mês com maior faturamento
- **Evolução Mensal**: Crescimento mês a mês
- **Comparativos**: Percentuais de crescimento

### ✅ Dashboard Financeiro
- **Valor Total em Aberto**: Todas as sessões não pagas
- **Crescimento Mensal**: Comparação com mês anterior
- **Top Clientes**: Maiores devedores
- **Próximos Vencimentos**: Baseado no dia de vencimento do cliente
- **Métricas Rápidas**: KPIs principais

## 🎯 Endpoints da API

### Relatórios Básicos
```
GET /api/financeiro/dashboard           # Dashboard principal
GET /api/financeiro/mensal/atual        # Relatório do mês atual
GET /api/financeiro/anual/atual         # Relatório do ano atual
GET /api/financeiro/mensal/{ano}/{mes}  # Relatório de mês específico
GET /api/financeiro/anual/{ano}         # Relatório de ano específico
```

### Comparativos e Análises
```
GET /api/financeiro/comparativo         # Comparar dois meses
GET /api/financeiro/ultimos-12-meses    # Últimos 12 meses
GET /api/financeiro/evolucao-anual      # Evolução dos últimos anos
GET /api/financeiro/resumo              # Resumo executivo
GET /api/financeiro/metricas            # Dados para gráficos
```

## 📈 Exemplos de Uso

### Dashboard Financeiro
```json
{
  "valorTotalEmAberto": 1500.00,
  "quantidadeSessoesNaoPagas": 10,
  "faturamentoMesAtual": 3000.00,
  "faturamentoMesAnterior": 2800.00,
  "crescimentoMensal": 7.14,
  "valorMedioSessao": 150.00,
  "clientesComMaiorDebito": [
    {
      "clienteId": 1,
      "nomeCliente": "João Silva",
      "valorEmAberto": 450.00,
      "quantidadeSessoesNaoPagas": 3,
      "diasEmAtraso": 15
    }
  ],
  "proximosVencimentos": [
    {
      "clienteId": 2,
      "nomeCliente": "Maria Santos",
      "dataVencimento": "2025-10-10",
      "valorAVencer": 300.00,
      "diasAteVencimento": 7
    }
  ]
}
```

### Relatório Mensal
```json
{
  "ano": 2025,
  "mes": 10,
  "nomeMes": "Outubro",
  "totalSessoesRealizadas": 20,
  "valorTotalFaturado": 3000.00,
  "valorTotalPago": 2400.00,
  "valorTotalAReceber": 600.00,
  "percentualPagamento": 80.0,
  "valorMedioSessao": 150.00,
  "detalhesPorCliente": [
    {
      "cliente": {
        "id": 1,
        "nome": "João Silva"
      },
      "quantidadeSessoes": 4,
      "valorTotalFaturado": 600.00,
      "valorTotalPago": 450.00,
      "valorEmAberto": 150.00,
      "percentualPagamento": 75.0,
      "ultimaDataPagamento": "2025-10-01"
    }
  ],
  "detalhesPorFormaPagamento": [
    {
      "formaPagamento": "PIX",
      "quantidadeSessoes": 12,
      "valorTotal": 1800.00,
      "percentualDoTotal": 75.0
    },
    {
      "formaPagamento": "Cartão",
      "quantidadeSessoes": 4,
      "valorTotal": 600.00,
      "percentualDoTotal": 25.0
    }
  ],
  "sessoesNaoPagas": [
    {
      "id": 15,
      "nomeCliente": "João Silva",
      "dataSessao": "2025-09-25",
      "valor": 150.00,
      "diasEmAtraso": 8
    }
  ]
}
```

## 🔧 Arquitetura

### Camada de Aplicação
- **`FinanceiroAppService`**: Orquestração dos cálculos financeiros
- **DTOs Específicos**: Estruturas otimizadas para cada tipo de relatório
- **Cálculos Dinâmicos**: Baseados nos dados das sessões em tempo real

### Camada de Infraestrutura
- **Métodos Especializados**: Queries otimizadas no `SessaoRepository`
- **Agregações Eficientes**: GroupBy e Sum direto no banco
- **Performance**: Índices otimizados para consultas financeiras

### Principais DTOs
- **`RelatorioFinanceiroMensalDto`**: Relatório completo do mês
- **`RelatorioFinanceiroAnualDto`**: Consolidação anual
- **`DashboardFinanceiroDto`**: Métricas principais
- **`ComparativoMensalDto`**: Comparação entre períodos

## 💡 Vantagens da Abordagem

### ✅ Sem Tabelas Adicionais
- **Simplicidade**: Dados calculados dinamicamente das sessões
- **Consistência**: Sempre reflete o estado atual
- **Manutenibilidade**: Menos complexidade no banco de dados

### ✅ Performance Otimizada
- **Queries Eficientes**: Agregações direto no banco
- **Índices Estratégicos**: Otimizados para consultas financeiras
- **Cache Natural**: Resultados podem ser facilmente cacheados

### ✅ Flexibilidade Total
- **Períodos Customizados**: Qualquer mês/ano
- **Filtros Dinâmicos**: Por cliente, status, forma de pagamento
- **Métricas Personalizadas**: Fácil adicionar novos cálculos

## 🚀 Casos de Uso

### 1. Dashboard Diário
```csharp
var dashboard = await financeiroService.ObterDashboardFinanceiroAsync();
// Mostra valor em aberto, crescimento mensal, próximos vencimentos
```

### 2. Fechamento Mensal
```csharp
var relatorio = await financeiroService.ObterRelatorioMensalAsync(2025, 10);
// Relatório completo para fechamento contábil
```

### 3. Análise de Crescimento
```csharp
var ultimos12 = await financeiroService.ObterUltimos12MesesAsync();
// Gráfico de evolução do faturamento
```

### 4. Cobrança de Clientes
```csharp
var dashboard = await financeiroService.ObterDashboardFinanceiroAsync();
var clientesEmAtraso = dashboard.ClientesComMaiorDebito;
// Lista para ações de cobrança
```

## 📊 Métricas Calculadas

### Faturamento
- **Valor Faturado**: Soma das sessões realizadas
- **Valor Pago**: Soma das sessões pagas
- **Valor a Receber**: Diferença entre faturado e pago
- **Percentual de Pagamento**: (Pago / Faturado) * 100

### Crescimento
- **Crescimento Mensal**: Comparação com mês anterior
- **Evolução Anual**: Crescimento mês a mês no ano
- **Tendências**: Identificação de padrões sazonais

### Clientes
- **Ranking de Débito**: Clientes com maior valor em aberto
- **Histórico de Pagamento**: Última data de pagamento por cliente
- **Dias em Atraso**: Cálculo automático baseado na data da sessão

## 🔄 Integração com Sessões

O módulo financeiro utiliza os seguintes campos da entidade `Sessao`:

- **`DataHoraRealizada`**: Para determinar o período de faturamento
- **`Valor`**: Valor da sessão para cálculos
- **`Pago`**: Status de pagamento
- **`DataPagamento`**: Data do pagamento
- **`FormaPagamento`**: Forma utilizada (PIX, Cartão, etc.)
- **`Status`**: Apenas sessões "Realizadas" são consideradas
- **`ConsiderarFaturamento`**: Flag para incluir/excluir do faturamento

## 🎯 Próximas Melhorias

1. **Cache de Relatórios**: Redis para relatórios históricos
2. **Exportação PDF**: Relatórios em formato PDF
3. **Alertas Automáticos**: Notificações de vencimento
4. **Projeções**: Previsões baseadas em histórico
5. **Integração Contábil**: Export para sistemas contábeis
