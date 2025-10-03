using Microsoft.AspNetCore.Mvc;
using SP.Aplicacao.DTOs.Calendario;
using SP.Aplicacao.Services.Interfaces;

namespace SP.Api.Controllers;

/// <summary>
/// Controller para operações de calendário
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class CalendarioController(ICalendarioAppService calendarioService) : ControllerBase
{
    #region Visualização do Calendário

    /// <summary>
    /// Obtém a visualização semanal do calendário
    /// </summary>
    /// <param name="dataReferencia">Data de referência para a semana (formato: yyyy-MM-dd)</param>
    /// <param name="clienteIds">IDs dos clientes para filtrar (opcional)</param>
    /// <param name="incluirHorariosLivres">Incluir horários livres disponíveis</param>
    /// <returns>Calendário semanal com sessões e horários livres</returns>
    [HttpGet("semanal")]
    public async Task<IActionResult> ObterCalendarioSemanal(
        [FromQuery] DateTime? dataReferencia = null,
        [FromQuery] int[]? clienteIds = null,
        [FromQuery] bool incluirHorariosLivres = true)
    {
        var data = dataReferencia ?? DateTime.Today;
        
        var filtro = new CalendarioFiltroDto
        {
            ClienteIds = clienteIds?.ToList(),
            IncluirHorariosLivres = incluirHorariosLivres,
            TipoVisualizacao = TipoVisualizacaoCalendario.Semanal
        };

        var resultado = await calendarioService.ObterCalendarioSemanalAsync(data, filtro);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém a visualização de um dia específico
    /// </summary>
    /// <param name="data">Data específica (formato: yyyy-MM-dd)</param>
    /// <param name="clienteIds">IDs dos clientes para filtrar (opcional)</param>
    /// <param name="incluirHorariosLivres">Incluir horários livres disponíveis</param>
    /// <returns>Detalhes do dia com sessões e horários livres</returns>
    [HttpGet("dia")]
    public async Task<IActionResult> ObterCalendarioDia(
        [FromQuery] DateTime data,
        [FromQuery] int[]? clienteIds = null,
        [FromQuery] bool incluirHorariosLivres = true)
    {
        var filtro = new CalendarioFiltroDto
        {
            ClienteIds = clienteIds?.ToList(),
            IncluirHorariosLivres = incluirHorariosLivres,
            TipoVisualizacao = TipoVisualizacaoCalendario.Diaria
        };

        var resultado = await calendarioService.ObterCalendarioDiaAsync(data, filtro);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém a semana atual
    /// </summary>
    /// <param name="incluirHorariosLivres">Incluir horários livres disponíveis</param>
    /// <returns>Calendário da semana atual</returns>
    [HttpGet("semana-atual")]
    public async Task<IActionResult> ObterSemanaAtual([FromQuery] bool incluirHorariosLivres = true)
    {
        var filtro = new CalendarioFiltroDto
        {
            IncluirHorariosLivres = incluirHorariosLivres,
            TipoVisualizacao = TipoVisualizacaoCalendario.Semanal
        };

        var resultado = await calendarioService.ObterCalendarioSemanalAsync(DateTime.Today, filtro);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém o dia de hoje
    /// </summary>
    /// <param name="incluirHorariosLivres">Incluir horários livres disponíveis</param>
    /// <returns>Detalhes do dia atual</returns>
    [HttpGet("hoje")]
    public async Task<IActionResult> ObterHoje([FromQuery] bool incluirHorariosLivres = true)
    {
        var filtro = new CalendarioFiltroDto
        {
            IncluirHorariosLivres = incluirHorariosLivres,
            TipoVisualizacao = TipoVisualizacaoCalendario.Diaria
        };

        var resultado = await calendarioService.ObterCalendarioDiaAsync(DateTime.Today, filtro);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém horários livres para um período
    /// </summary>
    /// <param name="dataInicio">Data de início (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Data de fim (formato: yyyy-MM-dd)</param>
    /// <param name="duracaoMinutos">Duração desejada em minutos (padrão: 50)</param>
    /// <returns>Lista de horários livres disponíveis</returns>
    [HttpGet("horarios-livres")]
    public async Task<IActionResult> ObterHorariosLivres(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim,
        [FromQuery] int duracaoMinutos = 50)
    {
        var resultado = await calendarioService.ObterHorariosLivresAsync(dataInicio, dataFim, duracaoMinutos);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Verifica conflitos de horário para uma nova sessão
    /// </summary>
    /// <param name="dataHora">Data e hora da sessão (formato: yyyy-MM-ddTHH:mm:ss)</param>
    /// <param name="duracaoMinutos">Duração da sessão em minutos</param>
    /// <param name="sessaoId">ID da sessão (para edição, opcional)</param>
    /// <returns>Lista de conflitos encontrados</returns>
    [HttpGet("verificar-conflitos")]
    public async Task<IActionResult> VerificarConflitos(
        [FromQuery] DateTime dataHora,
        [FromQuery] int duracaoMinutos,
        [FromQuery] int? sessaoId = null)
    {
        var resultado = await calendarioService.VerificarConflitosAsync(dataHora, duracaoMinutos, sessaoId);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    #endregion

    #region Navegação e Busca

    /// <summary>
    /// Obtém informações de navegação do calendário
    /// </summary>
    /// <param name="dataAtual">Data atual sendo visualizada (formato: yyyy-MM-dd)</param>
    /// <returns>Informações para navegação</returns>
    [HttpGet("navegacao")]
    public async Task<IActionResult> ObterNavegacao([FromQuery] DateTime? dataAtual = null)
    {
        var data = dataAtual ?? DateTime.Today;
        var resultado = await calendarioService.ObterNavegacaoAsync(data);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Busca sessões no calendário
    /// </summary>
    /// <param name="termo">Termo de busca (nome do cliente, observações, etc.)</param>
    /// <param name="data">Data específica para buscar (formato: yyyy-MM-dd)</param>
    /// <param name="hora">Hora específica para buscar (formato: HH:mm)</param>
    /// <param name="apenasDisponiveis">Buscar apenas sessões disponíveis para reagendamento</param>
    /// <returns>Sessões encontradas</returns>
    [HttpGet("buscar")]
    public async Task<IActionResult> BuscarSessoes(
        [FromQuery] string? termo = null,
        [FromQuery] DateTime? data = null,
        [FromQuery] TimeSpan? hora = null,
        [FromQuery] bool apenasDisponiveis = false)
    {
        var busca = new CalendarioBuscaDto
        {
            Termo = termo,
            Data = data,
            Hora = hora,
            ApenasDisponiveis = apenasDisponiveis
        };

        var resultado = await calendarioService.BuscarSessoesAsync(busca);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém estatísticas do calendário para um período
    /// </summary>
    /// <param name="dataInicio">Data de início (formato: yyyy-MM-dd)</param>
    /// <param name="dataFim">Data de fim (formato: yyyy-MM-dd)</param>
    /// <returns>Estatísticas do período</returns>
    [HttpGet("estatisticas")]
    public async Task<IActionResult> ObterEstatisticas(
        [FromQuery] DateTime dataInicio,
        [FromQuery] DateTime dataFim)
    {
        var resultado = await calendarioService.ObterEstatisticasAsync(dataInicio, dataFim);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém estatísticas do mês atual
    /// </summary>
    /// <returns>Estatísticas do mês atual</returns>
    [HttpGet("estatisticas/mes-atual")]
    public async Task<IActionResult> ObterEstatisticasMesAtual()
    {
        var hoje = DateTime.Today;
        var inicioMes = new DateTime(hoje.Year, hoje.Month, 1);
        var fimMes = inicioMes.AddMonths(1).AddDays(-1);

        var resultado = await calendarioService.ObterEstatisticasAsync(inicioMes, fimMes);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    #endregion

    #region Google Calendar Integration

    /// <summary>
    /// Obtém a configuração atual do Google Calendar
    /// </summary>
    /// <returns>Configuração atual</returns>
    [HttpGet("google-calendar/config")]
    public async Task<IActionResult> ObterConfiguracaoGoogleCalendar()
    {
        var resultado = await calendarioService.ObterConfiguracaoGoogleCalendarAsync();
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Configura a integração com Google Calendar
    /// </summary>
    /// <param name="config">Configurações do Google Calendar</param>
    /// <returns>Resultado da configuração</returns>
    [HttpPost("google-calendar/config")]
    public async Task<IActionResult> ConfigurarGoogleCalendar([FromBody] GoogleCalendarConfigDto config)
    {
        var resultado = await calendarioService.ConfigurarGoogleCalendarAsync(config);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Sincroniza sessões com Google Calendar
    /// </summary>
    /// <param name="parametros">Parâmetros de sincronização</param>
    /// <returns>Resultado da sincronização</returns>
    [HttpPost("google-calendar/sincronizar")]
    public async Task<IActionResult> SincronizarComGoogleCalendar([FromBody] SincronizarGoogleCalendarDto parametros)
    {
        var resultado = await calendarioService.SincronizarComGoogleCalendarAsync(parametros);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Cria um evento no Google Calendar para uma sessão
    /// </summary>
    /// <param name="sessaoId">ID da sessão</param>
    /// <returns>Resultado da criação</returns>
    [HttpPost("google-calendar/evento/{sessaoId}")]
    public async Task<IActionResult> CriarEventoGoogleCalendar(int sessaoId)
    {
        var resultado = await calendarioService.CriarEventoGoogleCalendarAsync(sessaoId);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Remove um evento do Google Calendar
    /// </summary>
    /// <param name="sessaoId">ID da sessão</param>
    /// <returns>Resultado da remoção</returns>
    [HttpDelete("google-calendar/evento/{sessaoId}")]
    public async Task<IActionResult> RemoverEventoGoogleCalendar(int sessaoId)
    {
        var resultado = await calendarioService.RemoverEventoGoogleCalendarAsync(sessaoId);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
        
        return BadRequest(resultado.Erros);
    }

    #endregion

    #region Configurações de Horário

    /// <summary>
    /// Obtém as configurações de horário de trabalho
    /// </summary>
    /// <returns>Configurações de horário por dia da semana</returns>
    [HttpGet("horarios-trabalho")]
    public async Task<IActionResult> ObterHorariosTrabalho()
    {
        var resultado = await calendarioService.ObterHorariosTrabalhoAsync();

        if (resultado.Sucesso)
            return Ok(resultado.Dados);

        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Atualiza as configurações de horário de trabalho
    /// </summary>
    /// <param name="horarios">Novos horários de trabalho</param>
    /// <returns>Resultado da atualização</returns>
    [HttpPut("horarios-trabalho")]
    public async Task<IActionResult> AtualizarHorariosTrabalho([FromBody] List<HorarioTrabalhoDto> horarios)
    {
        var resultado = await calendarioService.AtualizarHorariosTrabalhoAsync(horarios);

        if (resultado.Sucesso)
            return Ok(resultado.Dados);

        return BadRequest(resultado.Erros);
    }

    #endregion

    #region Exportação

    /// <summary>
    /// Exporta o calendário em diferentes formatos
    /// </summary>
    /// <param name="parametros">Parâmetros de exportação</param>
    /// <returns>Arquivo exportado</returns>
    [HttpPost("exportar")]
    public async Task<IActionResult> ExportarCalendario([FromBody] CalendarioExportacaoDto parametros)
    {
        var resultado = await calendarioService.ExportarCalendarioAsync(parametros);

        if (resultado.Sucesso)
        {
            var nomeArquivo = $"calendario_{parametros.DataInicio:yyyyMMdd}_{parametros.DataFim:yyyyMMdd}";
            var contentType = parametros.Formato switch
            {
                FormatoExportacao.PDF => "application/pdf",
                FormatoExportacao.Excel => "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                FormatoExportacao.CSV => "text/csv",
                FormatoExportacao.ICalendar => "text/calendar",
                _ => "application/octet-stream"
            };

            var extensao = parametros.Formato switch
            {
                FormatoExportacao.PDF => ".pdf",
                FormatoExportacao.Excel => ".xlsx",
                FormatoExportacao.CSV => ".csv",
                FormatoExportacao.ICalendar => ".ics",
                _ => ".bin"
            };

            return File(resultado.Dados!, contentType, $"{nomeArquivo}{extensao}");
        }

        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Exporta a semana atual em PDF
    /// </summary>
    /// <returns>PDF da semana atual</returns>
    [HttpGet("exportar/semana-atual/pdf")]
    public async Task<IActionResult> ExportarSemanaAtualPdf()
    {
        var hoje = DateTime.Today;
        var inicioSemana = hoje.AddDays(-(int)hoje.DayOfWeek + 1); // Segunda-feira
        var fimSemana = inicioSemana.AddDays(6); // Domingo

        var parametros = new CalendarioExportacaoDto
        {
            Formato = FormatoExportacao.PDF,
            DataInicio = inicioSemana,
            DataFim = fimSemana,
            IncluirDetalhesClientes = true,
            IncluirObservacoes = true,
            IncluirHorariosLivres = false,
            Titulo = $"Agenda Semanal - {inicioSemana:dd/MM} a {fimSemana:dd/MM/yyyy}"
        };

        var resultado = await calendarioService.ExportarCalendarioAsync(parametros);

        if (resultado.Sucesso)
        {
            return File(resultado.Dados!, "application/pdf", $"agenda_semanal_{inicioSemana:yyyyMMdd}.pdf");
        }

        return BadRequest(resultado.Erros);
    }

    #endregion

    #region Endpoints de Conveniência

    /// <summary>
    /// Obtém próximas sessões (próximos 7 dias)
    /// </summary>
    /// <param name="limite">Número máximo de sessões a retornar (padrão: 10)</param>
    /// <returns>Lista das próximas sessões</returns>
    [HttpGet("proximas-sessoes")]
    public async Task<IActionResult> ObterProximasSessoes([FromQuery] int limite = 10)
    {
        var busca = new CalendarioBuscaDto
        {
            Data = DateTime.Today,
            ApenasDisponiveis = false
        };

        var resultado = await calendarioService.BuscarSessoesAsync(busca);

        if (resultado.Sucesso)
        {
            var proximasSessoes = resultado.Dados!
                .Where(s => s.DataHora >= DateTime.Now)
                .OrderBy(s => s.DataHora)
                .Take(limite)
                .ToList();

            return Ok(proximasSessoes);
        }

        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Obtém resumo do dia atual
    /// </summary>
    /// <returns>Resumo das sessões de hoje</returns>
    [HttpGet("resumo-hoje")]
    public async Task<IActionResult> ObterResumoHoje()
    {
        var resultado = await calendarioService.ObterCalendarioDiaAsync(DateTime.Today);

        if (resultado.Sucesso)
        {
            var resumo = new
            {
                Data = DateTime.Today.ToString("dd/MM/yyyy"),
                DiaSemana = resultado.Dados!.DiaSemana,
                TotalSessoes = resultado.Dados.Sessoes.Count,
                PrimeiraSessao = resultado.Dados.Sessoes.FirstOrDefault()?.Hora,
                UltimaSessao = resultado.Dados.Sessoes.LastOrDefault()?.Hora,
                ValorTotal = resultado.Dados.Sessoes.Sum(s => s.Valor),
                SessoesConfirmadas = resultado.Dados.Sessoes.Count(s => s.Status == "Confirmada" || s.Status == "Realizada"),
                SessoesPendentes = resultado.Dados.Sessoes.Count(s => s.Status == "Agendada"),
                HorariosLivres = resultado.Dados.HorariosLivres.Count
            };

            return Ok(resumo);
        }

        return BadRequest(resultado.Erros);
    }

    /// <summary>
    /// Verifica se um horário está disponível
    /// </summary>
    /// <param name="dataHora">Data e hora a verificar (formato: yyyy-MM-ddTHH:mm:ss)</param>
    /// <param name="duracaoMinutos">Duração em minutos (padrão: 50)</param>
    /// <returns>Indica se o horário está disponível</returns>
    [HttpGet("horario-disponivel")]
    public async Task<IActionResult> VerificarHorarioDisponivel(
        [FromQuery] DateTime dataHora,
        [FromQuery] int duracaoMinutos = 50)
    {
        var resultado = await calendarioService.VerificarConflitosAsync(dataHora, duracaoMinutos);

        if (resultado.Sucesso)
        {
            var disponivel = !resultado.Dados!.Any();
            return Ok(new { Disponivel = disponivel, Conflitos = resultado.Dados });
        }

        return BadRequest(resultado.Erros);
    }

    #endregion
}
