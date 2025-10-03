# Módulo Calendário

Este módulo implementa um sistema completo de calendário para psicólogos, com visualização semanal, integração com Google Calendar e gestão de horários.

## 📅 Funcionalidades

### ✅ Visualização do Calendário

#### **1. Visualização Semanal**
- **Semana Completa**: Segunda a domingo com todas as sessões
- **Horários Livres**: Cálculo automático de disponibilidade
- **Cores por Status**: Visual diferenciado para cada status de sessão
- **Resumo Semanal**: Estatísticas e métricas da semana
- **Navegação**: Semana anterior/próxima com validações

#### **2. Visualização Diária**
- **Agenda do Dia**: Todas as sessões organizadas por horário
- **Horários Disponíveis**: Slots livres para agendamento
- **Resumo do Dia**: Total de sessões, valores e estatísticas
- **Identificação**: Destaque para o dia atual

#### **3. Detalhes das Sessões**
- **Informações Completas**: Cliente, horário, duração, valor
- **Status Visual**: Cores diferenciadas por status
- **Contato Rápido**: Telefone do cliente disponível
- **Observações**: Notas e anotações clínicas
- **Pagamento**: Status de pagamento visível

### ✅ Gestão de Horários

#### **1. Verificação de Conflitos**
- **Detecção Automática**: Identifica sobreposições de horário
- **Validação em Tempo Real**: Antes de criar/editar sessões
- **Lista de Conflitos**: Mostra sessões que conflitam
- **Sugestões**: Horários alternativos disponíveis

#### **2. Horários de Trabalho**
- **Configuração por Dia**: Horário de início e fim
- **Intervalo de Almoço**: Período de pausa configurável
- **Duração das Sessões**: Tempo padrão personalizável
- **Intervalo entre Sessões**: Tempo de preparação

#### **3. Cálculo de Disponibilidade**
- **Horários Livres**: Slots disponíveis automaticamente
- **Duração Flexível**: Considera diferentes durações
- **Horários Preferenciais**: Destaque para melhores horários
- **Fim de Semana**: Configuração separada para sábado/domingo

### ✅ Integração Google Calendar

#### **1. Sincronização Bidirecional**
- **Exportar Sessões**: Criar eventos no Google Calendar
- **Importar Eventos**: Sincronizar agenda externa
- **Atualização Automática**: Mudanças refletidas em ambos
- **Controle de Conflitos**: Evita duplicações

#### **2. Configuração de Integração**
- **Autenticação OAuth**: Login seguro com Google
- **Seleção de Calendário**: Escolher calendário específico
- **Lembretes Automáticos**: Email e popup configuráveis
- **Sincronização Seletiva**: Controle sobre o que sincronizar

#### **3. Gestão de Eventos**
- **Criação Automática**: Eventos criados para novas sessões
- **Atualização em Tempo Real**: Mudanças sincronizadas
- **Remoção Inteligente**: Exclusão coordenada
- **Histórico de Sincronização**: Log de operações

### ✅ Busca e Navegação

#### **1. Busca Avançada**
- **Por Cliente**: Nome do paciente
- **Por Data/Hora**: Período específico
- **Por Observações**: Conteúdo das anotações
- **Por Status**: Filtro por situação da sessão

#### **2. Navegação Intuitiva**
- **Semana Anterior/Próxima**: Navegação temporal
- **Ir para Data**: Salto direto para data específica
- **Voltar para Hoje**: Retorno rápido ao presente
- **Limites de Navegação**: Controle de período válido

#### **3. Filtros Dinâmicos**
- **Por Cliente**: Visualizar agenda de paciente específico
- **Por Status**: Apenas sessões confirmadas, pendentes, etc.
- **Por Pagamento**: Sessões pagas ou pendentes
- **Combinação**: Múltiplos filtros simultâneos

### ✅ Estatísticas e Relatórios

#### **1. Métricas do Calendário**
- **Taxa de Comparecimento**: Percentual de presença
- **Taxa de Cancelamento**: Percentual de cancelamentos
- **Horário Mais Ocupado**: Pico de atividade
- **Dia Mais Ocupado**: Dia da semana preferido
- **Cliente com Mais Sessões**: Paciente mais frequente

#### **2. Análise de Ocupação**
- **Percentual de Ocupação**: Uso da agenda
- **Horários Livres**: Disponibilidade total
- **Média de Sessões**: Por dia/semana/mês
- **Valor Médio**: Por sessão no período

#### **3. Relatórios Personalizados**
- **Período Flexível**: Qualquer intervalo de datas
- **Exportação**: PDF, Excel, CSV, iCalendar
- **Detalhamento**: Nível de informação configurável
- **Agendamento**: Relatórios automáticos

## 🌐 Endpoints da API

### Visualização Básica
```
✅ GET /api/calendario/semana-atual        # Semana atual completa
✅ GET /api/calendario/semanal             # Semana específica
✅ GET /api/calendario/hoje                # Dia atual
✅ GET /api/calendario/dia                 # Dia específico
✅ GET /api/calendario/horarios-livres     # Horários disponíveis
✅ GET /api/calendario/verificar-conflitos # Verificar sobreposições
```

### Navegação e Busca
```
✅ GET /api/calendario/navegacao           # Informações de navegação
✅ GET /api/calendario/buscar              # Busca avançada
✅ GET /api/calendario/estatisticas        # Estatísticas do período
✅ GET /api/calendario/estatisticas/mes-atual # Estatísticas do mês
```

### Google Calendar
```
✅ GET /api/calendario/google-calendar/config        # Configuração atual
✅ POST /api/calendario/google-calendar/config       # Configurar integração
✅ POST /api/calendario/google-calendar/sincronizar  # Sincronizar agenda
✅ POST /api/calendario/google-calendar/evento/{id}  # Criar evento
✅ DELETE /api/calendario/google-calendar/evento/{id} # Remover evento
```

### Configurações
```
✅ GET /api/calendario/horarios-trabalho   # Horários de trabalho
✅ PUT /api/calendario/horarios-trabalho   # Atualizar horários
✅ POST /api/calendario/exportar           # Exportar calendário
✅ GET /api/calendario/exportar/semana-atual/pdf # PDF da semana
```

### Endpoints de Conveniência
```
✅ GET /api/calendario/proximas-sessoes    # Próximas sessões
✅ GET /api/calendario/resumo-hoje         # Resumo do dia
✅ GET /api/calendario/horario-disponivel  # Verificar disponibilidade
```

## 📊 Exemplos de Uso

### Calendário Semanal
```json
{
  "dataInicioSemana": "2025-09-29T00:00:00-03:00",
  "dataFimSemana": "2025-10-05T00:00:00-03:00",
  "numeroSemana": 40,
  "mesAno": "Outubro 2025",
  "dias": [
    {
      "data": "2025-10-03T00:00:00-03:00",
      "diaSemana": "Sexta",
      "diaMes": 3,
      "ehHoje": true,
      "ehFimDeSemana": false,
      "sessoes": [],
      "horariosLivres": [
        {
          "horaInicio": "09:00",
          "horaFim": "09:50",
          "duracaoMinutos": 50,
          "horarioPreferencial": true
        }
      ],
      "resumo": {
        "totalSessoes": 0,
        "sessoesConfirmadas": 0,
        "sessoesPendentes": 0,
        "valorTotal": 0,
        "primeiraSessao": null,
        "ultimaSessao": null,
        "horariosLivres": 10
      }
    }
  ],
  "resumo": {
    "totalSessoes": 1,
    "valorTotal": 150.00,
    "diaMaisSessoes": "Sábado",
    "maxSessoesDia": 1,
    "mediaSessoesDia": 0.14,
    "totalHorariosLivres": 69
  }
}
```

### Verificação de Conflitos
```json
[
  {
    "id": 1,
    "clienteId": 1,
    "nomeCliente": "João Silva",
    "dataHora": "2025-10-04T14:00:00",
    "hora": "14:00",
    "duracaoMinutos": 50,
    "horaFim": "14:50",
    "status": "Realizada",
    "cor": "#27ae60"
  }
]
```

### Estatísticas do Período
```json
{
  "periodo": "01/10/2025 - 31/10/2025",
  "totalSessoesAgendadas": 1,
  "totalSessoesRealizadas": 1,
  "totalSessoesCanceladas": 0,
  "totalFaltas": 0,
  "taxaComparecimento": 100,
  "taxaCancelamento": 0,
  "valorTotalFaturado": 150.00,
  "valorMedioSessao": 150.00,
  "horarioMaisOcupado": "14:00",
  "diaMaisOcupado": "Sábado",
  "clienteMaisSessoes": "João Silva",
  "mediaSessoesPorDia": 0.033,
  "percentualOcupacao": 0.45
}
```

## 🎨 Cores por Status

- **Agendada**: `#3498db` (Azul)
- **Confirmada**: `#2ecc71` (Verde)
- **Realizada**: `#27ae60` (Verde escuro)
- **Cancelada Cliente**: `#e74c3c` (Vermelho)
- **Cancelada Psicólogo**: `#f39c12` (Laranja)
- **Falta**: `#95a5a6` (Cinza)
- **Reagendada**: `#9b59b6` (Roxo)

## 🔧 Arquitetura

### Camada de Aplicação
- **`CalendarioAppService`**: Orquestração da lógica de calendário
- **DTOs Especializados**: 15+ estruturas para diferentes visualizações
- **Cálculos Dinâmicos**: Horários livres e conflitos em tempo real
- **Integração Externa**: Preparado para Google Calendar API

### Camada de Infraestrutura
- **Métodos Otimizados**: Queries eficientes no `SessaoRepository`
- **Detecção de Conflitos**: Algoritmo de sobreposição de horários
- **Filtros Avançados**: Múltiplos critérios de busca
- **Performance**: Consultas otimizadas com Include

### Principais DTOs
- **`CalendarioSemanalDto`**: Visualização completa da semana
- **`CalendarioDiaDto`**: Detalhes de um dia específico
- **`CalendarioSessaoDto`**: Sessão otimizada para calendário
- **`CalendarioEstatisticasDto`**: Métricas e análises
- **`GoogleCalendarConfigDto`**: Configuração de integração

## 💡 Vantagens da Implementação

### ✅ Performance Otimizada
- **Cálculos Eficientes**: Horários livres calculados dinamicamente
- **Queries Inteligentes**: Apenas dados necessários carregados
- **Cache Friendly**: Estrutura preparada para cache
- **Paginação**: Suporte para grandes volumes de dados

### ✅ Flexibilidade Total
- **Horários Personalizáveis**: Configuração por dia da semana
- **Durações Variáveis**: Sessões de diferentes tamanhos
- **Filtros Combinados**: Múltiplos critérios simultâneos
- **Exportação Múltipla**: Vários formatos disponíveis

### ✅ Integração Preparada
- **Google Calendar**: Base para integração real
- **Outros Calendários**: Estrutura extensível
- **APIs Externas**: Interface padronizada
- **Webhooks**: Preparado para notificações

## 🚀 Casos de Uso

### 1. Visualização da Agenda
```csharp
var calendario = await calendarioService.ObterCalendarioSemanalAsync(DateTime.Today);
// Mostra semana completa com sessões e horários livres
```

### 2. Agendamento Inteligente
```csharp
var conflitos = await calendarioService.VerificarConflitosAsync(dataHora, 50);
if (!conflitos.Dados.Any())
{
    // Horário disponível para agendamento
}
```

### 3. Análise de Produtividade
```csharp
var estatisticas = await calendarioService.ObterEstatisticasAsync(inicioMes, fimMes);
// Métricas de ocupação, comparecimento e faturamento
```

### 4. Busca Rápida
```csharp
var busca = new CalendarioBuscaDto { Termo = "João", Data = DateTime.Today };
var sessoes = await calendarioService.BuscarSessoesAsync(busca);
// Encontra sessões do cliente João para hoje
```

## 🔄 Integração com Outros Módulos

O módulo de calendário integra perfeitamente com:

- **Módulo de Sessões**: Visualização e gestão de agendamentos
- **Módulo Financeiro**: Valores e status de pagamento
- **Módulo de Clientes**: Informações de contato e histórico
- **Futuro Módulo de Notificações**: Lembretes e alertas

## 🎯 Próximas Melhorias

1. **Google Calendar API Real**: Implementação completa da integração
2. **Notificações Push**: Lembretes automáticos
3. **Calendário Mensal**: Visualização de mês completo
4. **Recorrência**: Sessões recorrentes automáticas
5. **Mobile App**: Interface otimizada para dispositivos móveis
6. **Relatórios Avançados**: Análises preditivas e tendências
