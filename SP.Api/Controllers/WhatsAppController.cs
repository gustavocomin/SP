using Microsoft.AspNetCore.Mvc;
using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.WhatsApp;
using SP.Aplicacao.DTOs.WhatsApp.Enums;
using SP.Aplicacao.Services.Interfaces;

namespace SP.Api.Controllers
{
    /// <summary>
    /// Controller para integração com WhatsApp
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class WhatsAppController : ControllerBase
    {
        private readonly IWhatsAppService _whatsAppService;
        private readonly ILogger<WhatsAppController> _logger;

        public WhatsAppController(
            IWhatsAppService whatsAppService,
            ILogger<WhatsAppController> logger)
        {
            _whatsAppService = whatsAppService;
            _logger = logger;
        }

        #region Envio de Mensagens

        /// <summary>
        /// Envia mensagem de texto via WhatsApp
        /// </summary>
        /// <param name="mensagem">Dados da mensagem</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("enviar")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarMensagem([FromBody] EnviarWhatsAppDto mensagem)
        {
            var resultado = await _whatsAppService.EnviarMensagemAsync(mensagem);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia mensagem usando template
        /// </summary>
        /// <param name="telefone">Número do destinatário</param>
        /// <param name="nomeTemplate">Nome do template</param>
        /// <param name="parametros">Parâmetros do template</param>
        /// <param name="clienteId">ID do cliente (opcional)</param>
        /// <param name="sessaoId">ID da sessão (opcional)</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("enviar-template")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarTemplate(
            [FromQuery] string telefone,
            [FromQuery] string nomeTemplate,
            [FromBody] List<string> parametros,
            [FromQuery] int? clienteId = null,
            [FromQuery] int? sessaoId = null)
        {
            var resultado = await _whatsAppService.EnviarTemplateAsync(telefone, nomeTemplate, parametros, clienteId, sessaoId);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia mídia (imagem, documento, vídeo, áudio)
        /// </summary>
        /// <param name="telefone">Número do destinatário</param>
        /// <param name="urlMidia">URL da mídia</param>
        /// <param name="tipoMidia">Tipo da mídia (image, document, video, audio)</param>
        /// <param name="legenda">Legenda da mídia (opcional)</param>
        /// <param name="clienteId">ID do cliente (opcional)</param>
        /// <param name="sessaoId">ID da sessão (opcional)</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("enviar-midia")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarMidia(
            [FromQuery] string telefone,
            [FromQuery] string urlMidia,
            [FromQuery] string tipoMidia,
            [FromQuery] string? legenda = null,
            [FromQuery] int? clienteId = null,
            [FromQuery] int? sessaoId = null)
        {
            var resultado = await _whatsAppService.EnviarMidiaAsync(telefone, urlMidia, tipoMidia, legenda, clienteId, sessaoId);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Agenda mensagem para envio futuro
        /// </summary>
        /// <param name="mensagem">Dados da mensagem com data de agendamento</param>
        /// <returns>ID do agendamento</returns>
        [HttpPost("agendar")]
        public async Task<ActionResult<int>> AgendarMensagem([FromBody] EnviarWhatsAppDto mensagem)
        {
            var resultado = await _whatsAppService.AgendarMensagemAsync(mensagem);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Mensagens Pré-definidas

        /// <summary>
        /// Envia lembrete de sessão
        /// </summary>
        /// <param name="sessaoId">ID da sessão</param>
        /// <param name="horasAntecedencia">Horas de antecedência (padrão: 24h)</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("lembrete-sessao/{sessaoId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarLembreteSessao(
            int sessaoId,
            [FromQuery] int horasAntecedencia = 24)
        {
            var resultado = await _whatsAppService.EnviarLembreteSessaoAsync(sessaoId, horasAntecedencia);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia confirmação de agendamento
        /// </summary>
        /// <param name="sessaoId">ID da sessão</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("confirmacao-agendamento/{sessaoId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarConfirmacaoAgendamento(int sessaoId)
        {
            var resultado = await _whatsAppService.EnviarConfirmacaoAgendamentoAsync(sessaoId);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia notificação de cancelamento
        /// </summary>
        /// <param name="sessaoId">ID da sessão</param>
        /// <param name="motivo">Motivo do cancelamento</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("notificacao-cancelamento/{sessaoId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarNotificacaoCancelamento(
            int sessaoId,
            [FromQuery] string motivo)
        {
            var resultado = await _whatsAppService.EnviarNotificacaoCancelamentoAsync(sessaoId, motivo);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia cobrança de pagamento
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <param name="valor">Valor a ser cobrado</param>
        /// <param name="dataVencimento">Data de vencimento</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("cobranca-pagamento/{clienteId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarCobrancaPagamento(
            int clienteId,
            [FromQuery] decimal valor,
            [FromQuery] DateTime dataVencimento)
        {
            var resultado = await _whatsAppService.EnviarCobrancaPagamentoAsync(clienteId, valor, dataVencimento);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia mensagem de boas-vindas
        /// </summary>
        /// <param name="clienteId">ID do cliente</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("boas-vindas/{clienteId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarBoasVindas(int clienteId)
        {
            var resultado = await _whatsAppService.EnviarBoasVindasAsync(clienteId);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Envia pesquisa de satisfação
        /// </summary>
        /// <param name="sessaoId">ID da sessão</param>
        /// <returns>Resultado do envio</returns>
        [HttpPost("pesquisa-satisfacao/{sessaoId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarPesquisaSatisfacao(int sessaoId)
        {
            var resultado = await _whatsAppService.EnviarPesquisaSatisfacaoAsync(sessaoId);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Configuração

        /// <summary>
        /// Obtém configuração atual do WhatsApp
        /// </summary>
        /// <returns>Configuração do WhatsApp</returns>
        [HttpGet("configuracao")]
        public async Task<ActionResult<ConfiguracaoWhatsAppDto>> ObterConfiguracao()
        {
            var resultado = await _whatsAppService.ObterConfiguracaoAsync();
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Atualiza configuração do WhatsApp
        /// </summary>
        /// <param name="config">Nova configuração</param>
        /// <returns>Configuração atualizada</returns>
        [HttpPut("configuracao")]
        public async Task<ActionResult<ConfiguracaoWhatsAppDto>> AtualizarConfiguracao([FromBody] ConfiguracaoWhatsAppDto config)
        {
            var resultado = await _whatsAppService.AtualizarConfiguracaoAsync(config);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Testa conexão com o provedor ativo
        /// </summary>
        /// <returns>Status da conexão</returns>
        [HttpPost("testar-conexao")]
        public async Task<ActionResult<bool>> TestarConexao()
        {
            var resultado = await _whatsAppService.TestarConexaoAsync();
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Valida número de telefone
        /// </summary>
        /// <param name="telefone">Número a ser validado</param>
        /// <returns>Status da validação</returns>
        [HttpGet("validar-telefone")]
        public async Task<ActionResult<bool>> ValidarTelefone([FromQuery] string telefone)
        {
            var resultado = await _whatsAppService.ValidarTelefoneAsync(telefone);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Templates

        /// <summary>
        /// Lista templates disponíveis
        /// </summary>
        /// <returns>Lista de templates</returns>
        [HttpGet("templates")]
        public async Task<ActionResult<List<TemplateWhatsAppDto>>> ListarTemplates()
        {
            var resultado = await _whatsAppService.ListarTemplatesAsync();
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Cria novo template
        /// </summary>
        /// <param name="template">Dados do template</param>
        /// <returns>Template criado</returns>
        [HttpPost("templates")]
        public async Task<ActionResult<TemplateWhatsAppDto>> CriarTemplate([FromBody] TemplateWhatsAppDto template)
        {
            var resultado = await _whatsAppService.CriarTemplateAsync(template);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return CreatedAtAction(nameof(ListarTemplates), resultado.Dados);
        }

        /// <summary>
        /// Atualiza template existente
        /// </summary>
        /// <param name="id">ID do template</param>
        /// <param name="template">Novos dados do template</param>
        /// <returns>Template atualizado</returns>
        [HttpPut("templates/{id}")]
        public async Task<ActionResult<TemplateWhatsAppDto>> AtualizarTemplate(int id, [FromBody] TemplateWhatsAppDto template)
        {
            var resultado = await _whatsAppService.AtualizarTemplateAsync(id, template);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Remove template
        /// </summary>
        /// <param name="id">ID do template</param>
        /// <returns>Status da remoção</returns>
        [HttpDelete("templates/{id}")]
        public async Task<ActionResult<bool>> RemoverTemplate(int id)
        {
            var resultado = await _whatsAppService.RemoverTemplateAsync(id);
            
            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Histórico e Estatísticas

        /// <summary>
        /// Obtém histórico de mensagens
        /// </summary>
        /// <param name="dataInicio">Data de início (opcional)</param>
        /// <param name="dataFim">Data de fim (opcional)</param>
        /// <param name="clienteId">ID do cliente (opcional)</param>
        /// <param name="status">Status das mensagens (opcional)</param>
        /// <param name="pagina">Página (padrão: 1)</param>
        /// <param name="tamanhoPagina">Tamanho da página (padrão: 50)</param>
        /// <returns>Histórico de mensagens</returns>
        [HttpGet("historico")]
        public async Task<ActionResult<List<HistoricoWhatsAppDto>>> ObterHistorico(
            [FromQuery] DateTime? dataInicio = null,
            [FromQuery] DateTime? dataFim = null,
            [FromQuery] int? clienteId = null,
            [FromQuery] StatusEnvioWhatsApp? status = null,
            [FromQuery] int pagina = 1,
            [FromQuery] int tamanhoPagina = 50)
        {
            var resultado = await _whatsAppService.ObterHistoricoAsync(dataInicio, dataFim, clienteId, status, pagina, tamanhoPagina);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Obtém estatísticas de envio
        /// </summary>
        /// <param name="dataInicio">Data de início</param>
        /// <param name="dataFim">Data de fim</param>
        /// <returns>Estatísticas de WhatsApp</returns>
        [HttpGet("estatisticas")]
        public async Task<ActionResult<EstatisticasWhatsAppDto>> ObterEstatisticas(
            [FromQuery] DateTime dataInicio,
            [FromQuery] DateTime dataFim)
        {
            var resultado = await _whatsAppService.ObterEstatisticasAsync(dataInicio, dataFim);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Obtém status de mensagem específica
        /// </summary>
        /// <param name="messageId">ID da mensagem</param>
        /// <returns>Status da mensagem</returns>
        [HttpGet("status/{messageId}")]
        public async Task<ActionResult<StatusEnvioWhatsApp>> ObterStatusMensagem(string messageId)
        {
            var resultado = await _whatsAppService.ObterStatusMensagemAsync(messageId);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Webhook

        /// <summary>
        /// Webhook para receber status de mensagens
        /// </summary>
        /// <param name="payload">Payload do webhook</param>
        /// <returns>Status do processamento</returns>
        [HttpPost("webhook/status")]
        public async Task<ActionResult<bool>> WebhookStatus([FromBody] string payload)
        {
            var resultado = await _whatsAppService.ProcessarWebhookStatusAsync(payload);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        /// <summary>
        /// Webhook para receber mensagens
        /// </summary>
        /// <param name="payload">Payload do webhook</param>
        /// <returns>Status do processamento</returns>
        [HttpPost("webhook/mensagem")]
        public async Task<ActionResult<bool>> WebhookMensagem([FromBody] string payload)
        {
            var resultado = await _whatsAppService.ProcessarMensagemRecebidaAsync(payload);

            if (!resultado.Sucesso)
            {
                return BadRequest(resultado);
            }

            return Ok(resultado.Dados);
        }

        #endregion

        #region Endpoints de Conveniência

        /// <summary>
        /// Envia lembrete para todas as sessões do dia seguinte
        /// </summary>
        /// <returns>Resultado dos envios</returns>
        [HttpPost("lembrete-dia-seguinte")]
        public async Task<ActionResult<List<ResultadoWhatsAppDto>>> EnviarLembretesDiaSeguinte()
        {
            // TODO: Implementar busca de sessões do dia seguinte e envio em lote
            var resultados = new List<ResultadoWhatsAppDto>();
            return Ok(resultados);
        }

        /// <summary>
        /// Envia cobrança para todos os clientes com pagamentos em atraso
        /// </summary>
        /// <returns>Resultado dos envios</returns>
        [HttpPost("cobranca-em-lote")]
        public async Task<ActionResult<List<ResultadoWhatsAppDto>>> EnviarCobrancaEmLote()
        {
            // TODO: Implementar busca de pagamentos em atraso e envio em lote
            var resultados = new List<ResultadoWhatsAppDto>();
            return Ok(resultados);
        }

        /// <summary>
        /// Envia mensagem personalizada para múltiplos clientes
        /// </summary>
        /// <param name="clienteIds">IDs dos clientes</param>
        /// <param name="mensagem">Mensagem a ser enviada</param>
        /// <returns>Resultado dos envios</returns>
        [HttpPost("envio-em-lote")]
        public async Task<ActionResult<List<ResultadoWhatsAppDto>>> EnvioEmLote(
            [FromBody] List<int> clienteIds,
            [FromQuery] string mensagem)
        {
            // TODO: Implementar envio em lote para múltiplos clientes
            var resultados = new List<ResultadoWhatsAppDto>();
            return Ok(resultados);
        }

        #endregion

        #region Cobrança Mensal

        /// <summary>
        /// Envia cobrança mensal para um cliente específico
        /// </summary>
        [HttpPost("cobranca-mensal/{clienteId}")]
        public async Task<ActionResult<ResultadoWhatsAppDto>> EnviarCobrancaMensal(
            int clienteId,
            [FromQuery] int mes,
            [FromQuery] int ano,
            [FromQuery] string? chavePix = null)
        {
            var resultado = await _whatsAppService.EnviarCobrancaMensalAsync(clienteId, mes, ano, chavePix);
            return Ok(resultado);
        }

        /// <summary>
        /// Envia cobrança mensal para todos os clientes do mês
        /// </summary>
        [HttpPost("cobranca-mensal-lote")]
        public async Task<ActionResult<ResultadoCobrancaMensalDto>> EnviarCobrancaMensalLote(
            [FromQuery] int mes,
            [FromQuery] int ano,
            [FromQuery] string? chavePix = null)
        {
            var resultado = await _whatsAppService.EnviarCobrancaMensalLoteAsync(mes, ano, chavePix);
            return Ok(resultado);
        }

        /// <summary>
        /// Obtém dados da cobrança mensal de um cliente
        /// </summary>
        [HttpGet("cobranca-mensal/{clienteId}")]
        public async Task<ActionResult<CobrancaMensalDto?>> ObterDadosCobrancaMensal(
            int clienteId,
            [FromQuery] int mes,
            [FromQuery] int ano)
        {
            var dados = await _whatsAppService.ObterDadosCobrancaMensalAsync(clienteId, mes, ano);
            return Ok(dados);
        }

        /// <summary>
        /// Envia cobrança mensal do mês atual para todos os clientes
        /// </summary>
        [HttpPost("cobranca-mensal-mes-atual")]
        public async Task<ActionResult<ResultadoCobrancaMensalDto>> EnviarCobrancaMensalMesAtual(
            [FromQuery] string? chavePix = null)
        {
            var agora = DateTime.Now.AddMonths(-1); // Mês anterior
            var resultado = await _whatsAppService.EnviarCobrancaMensalLoteAsync(agora.Month, agora.Year, chavePix);
            return Ok(resultado);
        }

        #endregion
    }
}
