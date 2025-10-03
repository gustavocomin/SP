using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.WhatsApp;
using SP.Aplicacao.DTOs.WhatsApp.Enums;

namespace SP.Aplicacao.Services.Interfaces
{
    /// <summary>
    /// Interface para serviços de WhatsApp
    /// </summary>
    public interface IWhatsAppService
    {
        #region Envio de Mensagens

        /// <summary>
        /// Envia mensagem de texto simples
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarMensagemAsync(EnviarWhatsAppDto mensagem);

        /// <summary>
        /// Envia mensagem usando template
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarTemplateAsync(
            string telefone, 
            string nomeTemplate, 
            List<string> parametros,
            int? clienteId = null,
            int? sessaoId = null);

        /// <summary>
        /// Envia mídia (imagem, documento, vídeo, áudio)
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarMidiaAsync(
            string telefone,
            string urlMidia,
            string tipoMidia,
            string? legenda = null,
            int? clienteId = null,
            int? sessaoId = null);

        /// <summary>
        /// Agenda mensagem para envio futuro
        /// </summary>
        Task<ResultadoDto<int>> AgendarMensagemAsync(EnviarWhatsAppDto mensagem);

        #endregion

        #region Mensagens Pré-definidas para Psicólogos

        /// <summary>
        /// Envia lembrete de sessão
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarLembreteSessaoAsync(
            int sessaoId, 
            int horasAntecedencia = 24);

        /// <summary>
        /// Envia confirmação de agendamento
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarConfirmacaoAgendamentoAsync(int sessaoId);

        /// <summary>
        /// Envia notificação de cancelamento
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarNotificacaoCancelamentoAsync(
            int sessaoId, 
            string motivo);

        /// <summary>
        /// Envia cobrança de pagamento
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarCobrancaPagamentoAsync(
            int clienteId, 
            decimal valor, 
            DateTime dataVencimento);

        /// <summary>
        /// Envia mensagem de boas-vindas para novo cliente
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarBoasVindasAsync(int clienteId);

        /// <summary>
        /// Envia pesquisa de satisfação pós-sessão
        /// </summary>
        Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarPesquisaSatisfacaoAsync(int sessaoId);

        #endregion

        #region Configuração

        /// <summary>
        /// Obtém configuração atual do WhatsApp
        /// </summary>
        Task<ResultadoDto<ConfiguracaoWhatsAppDto>> ObterConfiguracaoAsync();

        /// <summary>
        /// Atualiza configuração do WhatsApp
        /// </summary>
        Task<ResultadoDto<ConfiguracaoWhatsAppDto>> AtualizarConfiguracaoAsync(ConfiguracaoWhatsAppDto config);

        /// <summary>
        /// Testa conexão com o provedor ativo
        /// </summary>
        Task<ResultadoDto<bool>> TestarConexaoAsync();

        /// <summary>
        /// Valida número de telefone
        /// </summary>
        Task<ResultadoDto<bool>> ValidarTelefoneAsync(string telefone);

        #endregion

        #region Templates

        /// <summary>
        /// Lista templates disponíveis
        /// </summary>
        Task<ResultadoDto<List<TemplateWhatsAppDto>>> ListarTemplatesAsync();

        /// <summary>
        /// Cria novo template
        /// </summary>
        Task<ResultadoDto<TemplateWhatsAppDto>> CriarTemplateAsync(TemplateWhatsAppDto template);

        /// <summary>
        /// Atualiza template existente
        /// </summary>
        Task<ResultadoDto<TemplateWhatsAppDto>> AtualizarTemplateAsync(int id, TemplateWhatsAppDto template);

        /// <summary>
        /// Remove template
        /// </summary>
        Task<ResultadoDto<bool>> RemoverTemplateAsync(int id);

        #endregion

        #region Histórico e Estatísticas

        /// <summary>
        /// Obtém histórico de mensagens
        /// </summary>
        Task<ResultadoDto<List<HistoricoWhatsAppDto>>> ObterHistoricoAsync(
            DateTime? dataInicio = null,
            DateTime? dataFim = null,
            int? clienteId = null,
            StatusEnvioWhatsApp? status = null,
            int pagina = 1,
            int tamanhoPagina = 50);

        /// <summary>
        /// Obtém estatísticas de envio
        /// </summary>
        Task<ResultadoDto<EstatisticasWhatsAppDto>> ObterEstatisticasAsync(
            DateTime dataInicio,
            DateTime dataFim);

        /// <summary>
        /// Obtém status de mensagem específica
        /// </summary>
        Task<ResultadoDto<StatusEnvioWhatsApp>> ObterStatusMensagemAsync(string messageId);

        #endregion

        #region Webhook e Callbacks

        /// <summary>
        /// Processa webhook de status de mensagem
        /// </summary>
        Task<ResultadoDto<bool>> ProcessarWebhookStatusAsync(string payload);

        /// <summary>
        /// Processa mensagem recebida
        /// </summary>
        Task<ResultadoDto<bool>> ProcessarMensagemRecebidaAsync(string payload);

        #endregion

        #region Cobrança Mensal

        /// <summary>
        /// Envia cobrança mensal personalizada para um cliente específico
        /// </summary>
        Task<ResultadoWhatsAppDto> EnviarCobrancaMensalAsync(int clienteId, int mes, int ano, string? chavePix = null);

        /// <summary>
        /// Envia cobrança mensal para todos os clientes que tiveram sessões no mês
        /// </summary>
        Task<ResultadoCobrancaMensalDto> EnviarCobrancaMensalLoteAsync(int mes, int ano, string? chavePix = null);

        /// <summary>
        /// Obtém dados da cobrança mensal de um cliente
        /// </summary>
        Task<CobrancaMensalDto?> ObterDadosCobrancaMensalAsync(int clienteId, int mes, int ano);

        #endregion
    }
}
