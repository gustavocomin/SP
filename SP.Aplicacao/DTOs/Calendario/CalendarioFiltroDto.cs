using SP.Dominio.Enums;

namespace SP.Aplicacao.DTOs.Calendario;

/// <summary>
/// DTO para filtros do calendário
/// </summary>
public class CalendarioFiltroDto
{
    /// <summary>
    /// Data de início do período
    /// </summary>
    public DateTime? DataInicio { get; set; }

    /// <summary>
    /// Data de fim do período
    /// </summary>
    public DateTime? DataFim { get; set; }

    /// <summary>
    /// IDs dos clientes para filtrar
    /// </summary>
    public List<int>? ClienteIds { get; set; }

    /// <summary>
    /// Status das sessões para filtrar
    /// </summary>
    public List<StatusSessao>? Status { get; set; }

    /// <summary>
    /// Incluir apenas sessões pagas
    /// </summary>
    public bool? ApenasPagas { get; set; }

    /// <summary>
    /// Incluir apenas sessões não pagas
    /// </summary>
    public bool? ApenasNaoPagas { get; set; }

    /// <summary>
    /// Incluir horários livres
    /// </summary>
    public bool IncluirHorariosLivres { get; set; } = true;

    /// <summary>
    /// Tipo de visualização
    /// </summary>
    public TipoVisualizacaoCalendario TipoVisualizacao { get; set; } = TipoVisualizacaoCalendario.Semanal;
}

/// <summary>
/// Enum para tipo de visualização do calendário
/// </summary>
public enum TipoVisualizacaoCalendario
{
    /// <summary>
    /// Visualização diária
    /// </summary>
    Diaria = 1,

    /// <summary>
    /// Visualização semanal
    /// </summary>
    Semanal = 2,

    /// <summary>
    /// Visualização mensal
    /// </summary>
    Mensal = 3
}

/// <summary>
/// DTO para navegação do calendário
/// </summary>
public class CalendarioNavegacaoDto
{
    /// <summary>
    /// Data atual sendo visualizada
    /// </summary>
    public DateTime DataAtual { get; set; }

    /// <summary>
    /// Data da semana anterior
    /// </summary>
    public DateTime SemanaAnterior { get; set; }

    /// <summary>
    /// Data da próxima semana
    /// </summary>
    public DateTime ProximaSemana { get; set; }

    /// <summary>
    /// Data de hoje
    /// </summary>
    public DateTime Hoje { get; set; } = DateTime.Today;

    /// <summary>
    /// Indica se pode navegar para trás
    /// </summary>
    public bool PodeVoltarSemana { get; set; } = true;

    /// <summary>
    /// Indica se pode navegar para frente
    /// </summary>
    public bool PodeAvancarSemana { get; set; } = true;

    /// <summary>
    /// Número da semana atual
    /// </summary>
    public int NumeroSemana { get; set; }

    /// <summary>
    /// Ano da semana atual
    /// </summary>
    public int Ano { get; set; }
}

/// <summary>
/// DTO para busca rápida no calendário
/// </summary>
public class CalendarioBuscaDto
{
    /// <summary>
    /// Termo de busca (nome do cliente, observações, etc.)
    /// </summary>
    public string? Termo { get; set; }

    /// <summary>
    /// Data específica para buscar
    /// </summary>
    public DateTime? Data { get; set; }

    /// <summary>
    /// Hora específica para buscar
    /// </summary>
    public TimeSpan? Hora { get; set; }

    /// <summary>
    /// Buscar apenas sessões disponíveis para reagendamento
    /// </summary>
    public bool ApenasDisponiveis { get; set; }

    /// <summary>
    /// Buscar apenas conflitos de horário
    /// </summary>
    public bool ApenasConflitos { get; set; }
}

/// <summary>
/// DTO para estatísticas do calendário
/// </summary>
public class CalendarioEstatisticasDto
{
    /// <summary>
    /// Período das estatísticas
    /// </summary>
    public string Periodo { get; set; } = string.Empty;

    /// <summary>
    /// Total de sessões agendadas
    /// </summary>
    public int TotalSessoesAgendadas { get; set; }

    /// <summary>
    /// Total de sessões realizadas
    /// </summary>
    public int TotalSessoesRealizadas { get; set; }

    /// <summary>
    /// Total de sessões canceladas
    /// </summary>
    public int TotalSessoesCanceladas { get; set; }

    /// <summary>
    /// Total de faltas
    /// </summary>
    public int TotalFaltas { get; set; }

    /// <summary>
    /// Taxa de comparecimento (%)
    /// </summary>
    public decimal TaxaComparecimento { get; set; }

    /// <summary>
    /// Taxa de cancelamento (%)
    /// </summary>
    public decimal TaxaCancelamento { get; set; }

    /// <summary>
    /// Valor total faturado
    /// </summary>
    public decimal ValorTotalFaturado { get; set; }

    /// <summary>
    /// Valor médio por sessão
    /// </summary>
    public decimal ValorMedioSessao { get; set; }

    /// <summary>
    /// Horário mais ocupado
    /// </summary>
    public string? HorarioMaisOcupado { get; set; }

    /// <summary>
    /// Dia da semana mais ocupado
    /// </summary>
    public string? DiaMaisOcupado { get; set; }

    /// <summary>
    /// Cliente com mais sessões
    /// </summary>
    public string? ClienteMaisSessoes { get; set; }

    /// <summary>
    /// Média de sessões por dia
    /// </summary>
    public decimal MediaSessoesPorDia { get; set; }

    /// <summary>
    /// Total de horários livres
    /// </summary>
    public int TotalHorariosLivres { get; set; }

    /// <summary>
    /// Percentual de ocupação
    /// </summary>
    public decimal PercentualOcupacao { get; set; }
}

/// <summary>
/// DTO para exportação do calendário
/// </summary>
public class CalendarioExportacaoDto
{
    /// <summary>
    /// Formato de exportação
    /// </summary>
    public FormatoExportacao Formato { get; set; } = FormatoExportacao.PDF;

    /// <summary>
    /// Data de início
    /// </summary>
    public DateTime DataInicio { get; set; }

    /// <summary>
    /// Data de fim
    /// </summary>
    public DateTime DataFim { get; set; }

    /// <summary>
    /// Incluir detalhes dos clientes
    /// </summary>
    public bool IncluirDetalhesClientes { get; set; } = true;

    /// <summary>
    /// Incluir valores financeiros
    /// </summary>
    public bool IncluirValores { get; set; } = false;

    /// <summary>
    /// Incluir observações
    /// </summary>
    public bool IncluirObservacoes { get; set; } = true;

    /// <summary>
    /// Incluir horários livres
    /// </summary>
    public bool IncluirHorariosLivres { get; set; } = false;

    /// <summary>
    /// Título do relatório
    /// </summary>
    public string? Titulo { get; set; }
}

/// <summary>
/// Enum para formato de exportação
/// </summary>
public enum FormatoExportacao
{
    /// <summary>
    /// Formato PDF
    /// </summary>
    PDF = 1,

    /// <summary>
    /// Formato Excel
    /// </summary>
    Excel = 2,

    /// <summary>
    /// Formato CSV
    /// </summary>
    CSV = 3,

    /// <summary>
    /// Formato iCalendar (.ics)
    /// </summary>
    ICalendar = 4
}
