using SP.Aplicacao.DTOs.Calendario;
using SP.Aplicacao.DTOs.Common;

namespace SP.Aplicacao.Services.Interfaces;

/// <summary>
/// Interface para serviços de calendário
/// </summary>
public interface ICalendarioAppService
{
    #region Visualização do Calendário

    /// <summary>
    /// Obtém a visualização semanal do calendário
    /// </summary>
    /// <param name="dataReferencia">Data de referência para a semana</param>
    /// <param name="filtro">Filtros opcionais</param>
    /// <returns>Calendário semanal com sessões e horários livres</returns>
    Task<ResultadoDto<CalendarioSemanalDto>> ObterCalendarioSemanalAsync(
        DateTime dataReferencia, 
        CalendarioFiltroDto? filtro = null);

    /// <summary>
    /// Obtém a visualização de um dia específico
    /// </summary>
    /// <param name="data">Data específica</param>
    /// <param name="filtro">Filtros opcionais</param>
    /// <returns>Detalhes do dia com sessões e horários livres</returns>
    Task<ResultadoDto<CalendarioDiaDto>> ObterCalendarioDiaAsync(
        DateTime data, 
        CalendarioFiltroDto? filtro = null);

    /// <summary>
    /// Obtém horários livres para um período
    /// </summary>
    /// <param name="dataInicio">Data de início</param>
    /// <param name="dataFim">Data de fim</param>
    /// <param name="duracaoMinutos">Duração desejada em minutos</param>
    /// <returns>Lista de horários livres disponíveis</returns>
    Task<ResultadoDto<List<CalendarioHorarioLivreDto>>> ObterHorariosLivresAsync(
        DateTime dataInicio, 
        DateTime dataFim, 
        int duracaoMinutos = 50);

    /// <summary>
    /// Verifica conflitos de horário para uma nova sessão
    /// </summary>
    /// <param name="dataHora">Data e hora da sessão</param>
    /// <param name="duracaoMinutos">Duração da sessão</param>
    /// <param name="sessaoId">ID da sessão (para edição)</param>
    /// <returns>Lista de conflitos encontrados</returns>
    Task<ResultadoDto<List<CalendarioSessaoDto>>> VerificarConflitosAsync(
        DateTime dataHora, 
        int duracaoMinutos, 
        int? sessaoId = null);

    #endregion

    #region Navegação e Busca

    /// <summary>
    /// Obtém informações de navegação do calendário
    /// </summary>
    /// <param name="dataAtual">Data atual sendo visualizada</param>
    /// <returns>Informações para navegação</returns>
    Task<ResultadoDto<CalendarioNavegacaoDto>> ObterNavegacaoAsync(DateTime dataAtual);

    /// <summary>
    /// Busca sessões no calendário
    /// </summary>
    /// <param name="busca">Parâmetros de busca</param>
    /// <returns>Sessões encontradas</returns>
    Task<ResultadoDto<List<CalendarioSessaoDto>>> BuscarSessoesAsync(CalendarioBuscaDto busca);

    /// <summary>
    /// Obtém estatísticas do calendário para um período
    /// </summary>
    /// <param name="dataInicio">Data de início</param>
    /// <param name="dataFim">Data de fim</param>
    /// <returns>Estatísticas do período</returns>
    Task<ResultadoDto<CalendarioEstatisticasDto>> ObterEstatisticasAsync(
        DateTime dataInicio, 
        DateTime dataFim);

    #endregion

    #region Google Calendar Integration

    /// <summary>
    /// Configura a integração com Google Calendar
    /// </summary>
    /// <param name="config">Configurações do Google Calendar</param>
    /// <returns>Resultado da configuração</returns>
    Task<ResultadoDto<GoogleCalendarConfigDto>> ConfigurarGoogleCalendarAsync(
        GoogleCalendarConfigDto config);

    /// <summary>
    /// Obtém a configuração atual do Google Calendar
    /// </summary>
    /// <returns>Configuração atual</returns>
    Task<ResultadoDto<GoogleCalendarConfigDto>> ObterConfiguracaoGoogleCalendarAsync();

    /// <summary>
    /// Sincroniza sessões com Google Calendar
    /// </summary>
    /// <param name="parametros">Parâmetros de sincronização</param>
    /// <returns>Resultado da sincronização</returns>
    Task<ResultadoDto<ResultadoSincronizacaoDto>> SincronizarComGoogleCalendarAsync(
        SincronizarGoogleCalendarDto parametros);

    /// <summary>
    /// Cria um evento no Google Calendar para uma sessão
    /// </summary>
    /// <param name="sessaoId">ID da sessão</param>
    /// <returns>Resultado da criação</returns>
    Task<ResultadoDto<GoogleCalendarEventDto>> CriarEventoGoogleCalendarAsync(int sessaoId);

    /// <summary>
    /// Atualiza um evento no Google Calendar
    /// </summary>
    /// <param name="sessaoId">ID da sessão</param>
    /// <returns>Resultado da atualização</returns>
    Task<ResultadoDto<GoogleCalendarEventDto>> AtualizarEventoGoogleCalendarAsync(int sessaoId);

    /// <summary>
    /// Remove um evento do Google Calendar
    /// </summary>
    /// <param name="sessaoId">ID da sessão</param>
    /// <returns>Resultado da remoção</returns>
    Task<ResultadoDto<bool>> RemoverEventoGoogleCalendarAsync(int sessaoId);

    /// <summary>
    /// Obtém eventos do Google Calendar para um período
    /// </summary>
    /// <param name="dataInicio">Data de início</param>
    /// <param name="dataFim">Data de fim</param>
    /// <returns>Lista de eventos</returns>
    Task<ResultadoDto<List<GoogleCalendarEventDto>>> ObterEventosGoogleCalendarAsync(
        DateTime dataInicio, 
        DateTime dataFim);

    #endregion

    #region Configurações de Horário

    /// <summary>
    /// Obtém as configurações de horário de trabalho
    /// </summary>
    /// <returns>Configurações de horário por dia da semana</returns>
    Task<ResultadoDto<List<HorarioTrabalhoDto>>> ObterHorariosTrabalhoAsync();

    /// <summary>
    /// Atualiza as configurações de horário de trabalho
    /// </summary>
    /// <param name="horarios">Novos horários de trabalho</param>
    /// <returns>Resultado da atualização</returns>
    Task<ResultadoDto<List<HorarioTrabalhoDto>>> AtualizarHorariosTrabalhoAsync(
        List<HorarioTrabalhoDto> horarios);

    #endregion

    #region Exportação

    /// <summary>
    /// Exporta o calendário em diferentes formatos
    /// </summary>
    /// <param name="parametros">Parâmetros de exportação</param>
    /// <returns>Arquivo exportado</returns>
    Task<ResultadoDto<byte[]>> ExportarCalendarioAsync(CalendarioExportacaoDto parametros);

    #endregion
}
