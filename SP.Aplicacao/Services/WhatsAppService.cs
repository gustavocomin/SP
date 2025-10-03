using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.WhatsApp;
using SP.Aplicacao.DTOs.WhatsApp.Enums;
using SP.Aplicacao.Services.Interfaces;
using SP.Infraestrutura.Entities.Clientes;
using SP.Infraestrutura.Entities.Sessoes;
using SP.Infraestrutura.Common.Base;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;
using System.Text;
using System.Net.Http;
using SP.Dominio.Sessoes;
using SP.Dominio.Enums;

namespace SP.Aplicacao.Services
{
    /// <summary>
    /// Serviço para integração com WhatsApp
    /// </summary>
    public class WhatsAppService : IWhatsAppService
    {
        private readonly IClienteRepository _clienteRepository;
        private readonly ISessaoRepository _sessaoRepository;

        private readonly IConfiguration _configuration;
        private readonly ILogger<WhatsAppService> _logger;


        public WhatsAppService(
            IClienteRepository clienteRepository,
            ISessaoRepository sessaoRepository,
            IConfiguration configuration,
            ILogger<WhatsAppService> logger)
        {
            _clienteRepository = clienteRepository;
            _sessaoRepository = sessaoRepository;
            _configuration = configuration;
            _logger = logger;
        }

        #region Envio de Mensagens

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarMensagemAsync(EnviarWhatsAppDto mensagem)
        {
            try
            {
                // Validar telefone
                var telefoneValido = await ValidarTelefoneAsync(mensagem.Telefone);
                if (!telefoneValido.Dados)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Número de telefone inválido");
                }

                // Obter configuração ativa
                var config = await ObterConfiguracaoAsync();
                if (!config.Sucesso || !config.Dados!.Ativo)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("WhatsApp não configurado ou inativo");
                }

                // Enviar baseado no provedor ativo
                var resultado = config.Dados.ProvedorAtivo switch
                {
                    ProvedorWhatsApp.WhatsAppBusinessApi => await EnviarViaBusinessApiAsync(mensagem, config.Dados),
                    ProvedorWhatsApp.Twilio => await EnviarViaTwilioAsync(mensagem, config.Dados),
                    ProvedorWhatsApp.Evolution => await EnviarViaEvolutionAsync(mensagem, config.Dados),
                    _ => throw new NotImplementedException($"Provedor {config.Dados.ProvedorAtivo} não implementado")
                };

                // Salvar no histórico
                if (resultado.Sucesso)
                {
                    await SalvarHistoricoAsync(mensagem, resultado.Dados!);
                }

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar mensagem WhatsApp para {Telefone}", mensagem.Telefone);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro interno: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarTemplateAsync(
            string telefone,
            string nomeTemplate,
            List<string> parametros,
            int? clienteId = null,
            int? sessaoId = null)
        {
            var mensagem = new EnviarWhatsAppDto
            {
                Telefone = telefone,
                Tipo = TipoMensagemWhatsApp.Template,
                Template = nomeTemplate,
                ParametrosTemplate = parametros,
                ClienteId = clienteId,
                SessaoId = sessaoId
            };

            return await EnviarMensagemAsync(mensagem);
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarMidiaAsync(
            string telefone,
            string urlMidia,
            string tipoMidia,
            string? legenda = null,
            int? clienteId = null,
            int? sessaoId = null)
        {
            var tipo = tipoMidia.ToLower() switch
            {
                "image" => TipoMensagemWhatsApp.Imagem,
                "document" => TipoMensagemWhatsApp.Documento,
                "video" => TipoMensagemWhatsApp.Video,
                "audio" => TipoMensagemWhatsApp.Audio,
                _ => TipoMensagemWhatsApp.Documento
            };

            var mensagem = new EnviarWhatsAppDto
            {
                Telefone = telefone,
                Tipo = tipo,
                UrlMidia = urlMidia,
                TipoMidia = tipoMidia,
                Mensagem = legenda ?? "",
                ClienteId = clienteId,
                SessaoId = sessaoId
            };

            return await EnviarMensagemAsync(mensagem);
        }

        public async Task<ResultadoDto<int>> AgendarMensagemAsync(EnviarWhatsAppDto mensagem)
        {
            try
            {
                // TODO: Implementar sistema de agendamento
                // Por enquanto, retorna ID simulado
                var agendamentoId = new Random().Next(1000, 9999);

                _logger.LogInformation("Mensagem agendada para {Data} - ID: {Id}",
                    mensagem.DataAgendamento, agendamentoId);

                return ResultadoDto<int>.ComSucesso(agendamentoId);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao agendar mensagem WhatsApp");
                return ResultadoDto<int>.ComErro($"Erro ao agendar: {ex.Message}");
            }
        }

        #endregion

        #region Mensagens Pré-definidas

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarLembreteSessaoAsync(
            int sessaoId,
            int horasAntecedencia = 24)
        {
            try
            {
                var sessao = await _sessaoRepository.ObterPorIdAsync(sessaoId);
                if (sessao == null)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Sessão não encontrada");
                }

                var cliente = await _clienteRepository.ObterPorIdAsync(sessao.ClienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var mensagem = $"🗓️ *Lembrete de Sessão*\n\n" +
                              $"Olá {cliente.Nome}!\n\n" +
                              $"Você tem uma sessão agendada para:\n" +
                              $"📅 {sessao.DataHoraAgendada:dd/MM/yyyy}\n" +
                              $"🕐 {sessao.DataHoraAgendada:HH:mm}\n" +
                              $"⏱️ Duração: {sessao.DuracaoMinutos} minutos\n\n" +
                              $"Em caso de necessidade de reagendamento, entre em contato com antecedência.\n\n" +
                              $"Até breve! 😊";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = cliente.Id,
                    SessaoId = sessaoId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar lembrete de sessão {SessaoId}", sessaoId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar lembrete: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarConfirmacaoAgendamentoAsync(int sessaoId)
        {
            try
            {
                var sessao = await _sessaoRepository.ObterPorIdAsync(sessaoId);
                if (sessao == null)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Sessão não encontrada");
                }

                var cliente = await _clienteRepository.ObterPorIdAsync(sessao.ClienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var mensagem = $"✅ *Agendamento Confirmado*\n\n" +
                              $"Olá {cliente.Nome}!\n\n" +
                              $"Sua sessão foi agendada com sucesso:\n\n" +
                              $"📅 Data: {sessao.DataHoraAgendada:dd/MM/yyyy}\n" +
                              $"🕐 Horário: {sessao.DataHoraAgendada:HH:mm}\n" +
                              $"⏱️ Duração: {sessao.DuracaoMinutos} minutos\n" +
                              $"💰 Valor: R$ {sessao.Valor:F2}\n\n" +
                              $"Aguardo você! 😊\n\n" +
                              $"_Em caso de imprevistos, entre em contato com antecedência._";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = cliente.Id,
                    SessaoId = sessaoId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar confirmação de agendamento {SessaoId}", sessaoId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar confirmação: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarNotificacaoCancelamentoAsync(
            int sessaoId,
            string motivo)
        {
            try
            {
                var sessao = await _sessaoRepository.ObterPorIdAsync(sessaoId);
                if (sessao == null)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Sessão não encontrada");
                }

                var cliente = await _clienteRepository.ObterPorIdAsync(sessao.ClienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var mensagem = $"❌ *Sessão Cancelada*\n\n" +
                              $"Olá {cliente.Nome},\n\n" +
                              $"Infelizmente precisei cancelar nossa sessão:\n\n" +
                              $"📅 Data: {sessao.DataHoraAgendada:dd/MM/yyyy}\n" +
                              $"🕐 Horário: {sessao.DataHoraAgendada:HH:mm}\n\n" +
                              $"*Motivo:* {motivo}\n\n" +
                              $"Vamos reagendar assim que possível. Entre em contato para marcarmos um novo horário.\n\n" +
                              $"Obrigado pela compreensão! 🙏";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = cliente.Id,
                    SessaoId = sessaoId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar notificação de cancelamento {SessaoId}", sessaoId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar notificação: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarCobrancaPagamentoAsync(
            int clienteId,
            decimal valor,
            DateTime dataVencimento)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var diasVencimento = (dataVencimento - DateTime.Today).Days;
                var statusVencimento = diasVencimento switch
                {
                    > 0 => $"Vence em {diasVencimento} dias",
                    0 => "Vence hoje",
                    < 0 => $"Venceu há {Math.Abs(diasVencimento)} dias"
                };

                var mensagem = $"💰 *Cobrança de Pagamento*\n\n" +
                              $"Olá {cliente.Nome},\n\n" +
                              $"Você possui um pagamento pendente:\n\n" +
                              $"💵 Valor: R$ {valor:F2}\n" +
                              $"📅 Vencimento: {dataVencimento:dd/MM/yyyy}\n" +
                              $"⏰ Status: {statusVencimento}\n\n" +
                              $"Por favor, efetue o pagamento o quanto antes.\n\n" +
                              $"Formas de pagamento:\n" +
                              $"• PIX\n" +
                              $"• Cartão\n" +
                              $"• Dinheiro\n\n" +
                              $"Qualquer dúvida, entre em contato! 😊";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = clienteId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar cobrança para cliente {ClienteId}", clienteId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar cobrança: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarBoasVindasAsync(int clienteId)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var mensagem = $"🎉 *Seja bem-vindo(a)!*\n\n" +
                              $"Olá {cliente.Nome}!\n\n" +
                              $"É um prazer tê-lo(a) como meu paciente. Estou aqui para ajudá-lo(a) em sua jornada de autoconhecimento e bem-estar.\n\n" +
                              $"📋 *Informações importantes:*\n" +
                              $"• Chegue 5 minutos antes do horário\n" +
                              $"• Em caso de cancelamento, avise com 24h de antecedência\n" +
                              $"• Mantenha seu telefone atualizado para receber lembretes\n\n" +
                              $"Estou à disposição para qualquer dúvida!\n\n" +
                              $"Até nossa primeira sessão! 😊🌟";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = clienteId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar boas-vindas para cliente {ClienteId}", clienteId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar boas-vindas: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarPesquisaSatisfacaoAsync(int sessaoId)
        {
            try
            {
                var sessao = await _sessaoRepository.ObterPorIdAsync(sessaoId);
                if (sessao == null)
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Sessão não encontrada");
                }

                var cliente = await _clienteRepository.ObterPorIdAsync(sessao.ClienteId);
                if (cliente == null || string.IsNullOrEmpty(cliente.Telefone))
                {
                    return ResultadoDto<ResultadoWhatsAppDto>.ComErro("Cliente não encontrado ou sem telefone");
                }

                var mensagem = $"⭐ *Pesquisa de Satisfação*\n\n" +
                              $"Olá {cliente.Nome}!\n\n" +
                              $"Como foi nossa sessão de hoje? Sua opinião é muito importante para mim!\n\n" +
                              $"📊 *Avalie de 1 a 5:*\n" +
                              $"1️⃣ Muito insatisfeito\n" +
                              $"2️⃣ Insatisfeito\n" +
                              $"3️⃣ Neutro\n" +
                              $"4️⃣ Satisfeito\n" +
                              $"5️⃣ Muito satisfeito\n\n" +
                              $"Responda com o número correspondente à sua avaliação.\n\n" +
                              $"Sugestões e comentários também são bem-vindos! 💬\n\n" +
                              $"Obrigado! 😊";

                return await EnviarMensagemAsync(new EnviarWhatsAppDto
                {
                    Telefone = cliente.Telefone,
                    Mensagem = mensagem,
                    ClienteId = cliente.Id,
                    SessaoId = sessaoId
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar pesquisa de satisfação {SessaoId}", sessaoId);
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro ao enviar pesquisa: {ex.Message}");
            }
        }

        #endregion

        #region Métodos Privados de Envio

        private async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarViaBusinessApiAsync(
            EnviarWhatsAppDto mensagem,
            ConfiguracaoWhatsAppDto config)
        {
            try
            {
                // TODO: Implementar integração real com WhatsApp Business API
                _logger.LogInformation("Enviando via WhatsApp Business API para {Telefone}", mensagem.Telefone);

                // Simulação de envio bem-sucedido
                var resultado = new ResultadoWhatsAppDto
                {
                    MessageId = Guid.NewGuid().ToString(),
                    Status = StatusEnvioWhatsApp.Enviado,
                    DataEnvio = DateTime.Now,
                    Provedor = ProvedorWhatsApp.WhatsAppBusinessApi,
                    Custo = 0.05m // Custo simulado
                };

                return ResultadoDto<ResultadoWhatsAppDto>.ComSucesso(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no envio via Business API");
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro Business API: {ex.Message}");
            }
        }

        private async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarViaTwilioAsync(
            EnviarWhatsAppDto mensagem,
            ConfiguracaoWhatsAppDto config)
        {
            try
            {
                // TODO: Implementar integração real com Twilio
                _logger.LogInformation("Enviando via Twilio para {Telefone}", mensagem.Telefone);

                var resultado = new ResultadoWhatsAppDto
                {
                    MessageId = $"twilio_{Guid.NewGuid()}",
                    Status = StatusEnvioWhatsApp.Enviado,
                    DataEnvio = DateTime.Now,
                    Provedor = ProvedorWhatsApp.Twilio,
                    Custo = 0.08m
                };

                return ResultadoDto<ResultadoWhatsAppDto>.ComSucesso(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no envio via Twilio");
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro Twilio: {ex.Message}");
            }
        }

        private async Task<ResultadoDto<ResultadoWhatsAppDto>> EnviarViaEvolutionAsync(
            EnviarWhatsAppDto mensagem,
            ConfiguracaoWhatsAppDto config)
        {
            try
            {
                // TODO: Implementar integração real com Evolution API
                _logger.LogInformation("Enviando via Evolution API para {Telefone}", mensagem.Telefone);

                var resultado = new ResultadoWhatsAppDto
                {
                    MessageId = $"evolution_{Guid.NewGuid()}",
                    Status = StatusEnvioWhatsApp.Enviado,
                    DataEnvio = DateTime.Now,
                    Provedor = ProvedorWhatsApp.Evolution,
                    Custo = 0.00m // Evolution é gratuito
                };

                return ResultadoDto<ResultadoWhatsAppDto>.ComSucesso(resultado);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro no envio via Evolution");
                return ResultadoDto<ResultadoWhatsAppDto>.ComErro($"Erro Evolution: {ex.Message}");
            }
        }

        private async Task SalvarHistoricoAsync(EnviarWhatsAppDto mensagem, ResultadoWhatsAppDto resultado)
        {
            try
            {
                // TODO: Implementar salvamento no banco de dados
                // Por enquanto, apenas log
                _logger.LogInformation("Histórico salvo - MessageId: {MessageId}, Status: {Status}",
                    resultado.MessageId, resultado.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao salvar histórico de WhatsApp");
            }
        }

        #endregion

        #region Configuração

        public async Task<ResultadoDto<ConfiguracaoWhatsAppDto>> ObterConfiguracaoAsync()
        {
            try
            {
                // TODO: Buscar configuração do banco de dados
                // Por enquanto, retorna configuração padrão
                var config = new ConfiguracaoWhatsAppDto
                {
                    ProvedorAtivo = ProvedorWhatsApp.Evolution,
                    Ativo = true,
                    BusinessApi = new ConfiguracaoBusinessApiDto
                    {
                        ApiUrl = "https://graph.facebook.com/v18.0"
                    },
                    Terceiros = new ConfiguracaoTerceirosDto
                    {
                        ServicoAtivo = "Evolution"
                    }
                };

                return ResultadoDto<ConfiguracaoWhatsAppDto>.ComSucesso(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter configuração WhatsApp");
                return ResultadoDto<ConfiguracaoWhatsAppDto>.ComErro($"Erro ao obter configuração: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<ConfiguracaoWhatsAppDto>> AtualizarConfiguracaoAsync(ConfiguracaoWhatsAppDto config)
        {
            try
            {
                // TODO: Salvar configuração no banco de dados
                _logger.LogInformation("Configuração WhatsApp atualizada - Provedor: {Provedor}", config.ProvedorAtivo);

                return ResultadoDto<ConfiguracaoWhatsAppDto>.ComSucesso(config);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar configuração WhatsApp");
                return ResultadoDto<ConfiguracaoWhatsAppDto>.ComErro($"Erro ao atualizar: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<bool>> TestarConexaoAsync()
        {
            try
            {
                var config = await ObterConfiguracaoAsync();
                if (!config.Sucesso)
                {
                    return ResultadoDto<bool>.ComErro("Configuração não encontrada");
                }

                // TODO: Implementar teste real baseado no provedor
                _logger.LogInformation("Testando conexão com {Provedor}", config.Dados!.ProvedorAtivo);

                return ResultadoDto<bool>.ComSucesso(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao testar conexão WhatsApp");
                return ResultadoDto<bool>.ComErro($"Erro no teste: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<bool>> ValidarTelefoneAsync(string telefone)
        {
            try
            {
                // Validação básica de telefone brasileiro
                var telefoneNumeros = new string(telefone.Where(char.IsDigit).ToArray());

                // Deve ter 10 ou 11 dígitos (com DDD)
                if (telefoneNumeros.Length < 10 || telefoneNumeros.Length > 11)
                {
                    return ResultadoDto<bool>.ComSucesso(false);
                }

                // Deve começar com código do país (55) se tiver 13 dígitos
                if (telefoneNumeros.Length == 13 && !telefoneNumeros.StartsWith("55"))
                {
                    return ResultadoDto<bool>.ComSucesso(false);
                }

                return ResultadoDto<bool>.ComSucesso(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao validar telefone {Telefone}", telefone);
                return ResultadoDto<bool>.ComErro($"Erro na validação: {ex.Message}");
            }
        }

        #endregion

        #region Templates (Implementação Básica)

        public async Task<ResultadoDto<List<TemplateWhatsAppDto>>> ListarTemplatesAsync()
        {
            try
            {
                // TODO: Buscar templates do banco de dados
                var templates = new List<TemplateWhatsAppDto>
                {
                    new()
                    {
                        Nome = "lembrete_sessao",
                        Categoria = CategoriaTemplate.Utility,
                        Conteudo = "Lembrete: Você tem uma sessão agendada para {{data}} às {{hora}}",
                        Status = StatusTemplate.Aprovado,
                        Ativo = true
                    },
                    new()
                    {
                        Nome = "confirmacao_agendamento",
                        Categoria = CategoriaTemplate.Utility,
                        Conteudo = "Agendamento confirmado para {{data}} às {{hora}}. Valor: R$ {{valor}}",
                        Status = StatusTemplate.Aprovado,
                        Ativo = true
                    }
                };

                return ResultadoDto<List<TemplateWhatsAppDto>>.ComSucesso(templates);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao listar templates");
                return ResultadoDto<List<TemplateWhatsAppDto>>.ComErro($"Erro ao listar: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<TemplateWhatsAppDto>> CriarTemplateAsync(TemplateWhatsAppDto template)
        {
            try
            {
                // TODO: Salvar template no banco de dados
                _logger.LogInformation("Template criado: {Nome}", template.Nome);
                return ResultadoDto<TemplateWhatsAppDto>.ComSucesso(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao criar template");
                return ResultadoDto<TemplateWhatsAppDto>.ComErro($"Erro ao criar: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<TemplateWhatsAppDto>> AtualizarTemplateAsync(int id, TemplateWhatsAppDto template)
        {
            try
            {
                // TODO: Atualizar template no banco de dados
                _logger.LogInformation("Template atualizado: {Id}", id);
                return ResultadoDto<TemplateWhatsAppDto>.ComSucesso(template);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao atualizar template {Id}", id);
                return ResultadoDto<TemplateWhatsAppDto>.ComErro($"Erro ao atualizar: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<bool>> RemoverTemplateAsync(int id)
        {
            try
            {
                // TODO: Remover template do banco de dados
                _logger.LogInformation("Template removido: {Id}", id);
                return ResultadoDto<bool>.ComSucesso(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao remover template {Id}", id);
                return ResultadoDto<bool>.ComErro($"Erro ao remover: {ex.Message}");
            }
        }

        #endregion

        #region Histórico e Estatísticas (Implementação Básica)

        public async Task<ResultadoDto<List<HistoricoWhatsAppDto>>> ObterHistoricoAsync(
            DateTime? dataInicio = null,
            DateTime? dataFim = null,
            int? clienteId = null,
            StatusEnvioWhatsApp? status = null,
            int pagina = 1,
            int tamanhoPagina = 50)
        {
            try
            {
                // TODO: Implementar busca real no banco de dados
                var historico = new List<HistoricoWhatsAppDto>();
                return ResultadoDto<List<HistoricoWhatsAppDto>>.ComSucesso(historico);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter histórico");
                return ResultadoDto<List<HistoricoWhatsAppDto>>.ComErro($"Erro ao obter histórico: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<EstatisticasWhatsAppDto>> ObterEstatisticasAsync(
            DateTime dataInicio,
            DateTime dataFim)
        {
            try
            {
                // TODO: Implementar cálculo real das estatísticas
                var estatisticas = new EstatisticasWhatsAppDto
                {
                    Periodo = $"{dataInicio:dd/MM/yyyy} - {dataFim:dd/MM/yyyy}",
                    TotalEnviadas = 0,
                    TotalEntregues = 0,
                    TotalLidas = 0,
                    TotalErros = 0,
                    TaxaEntrega = 0,
                    TaxaLeitura = 0,
                    CustoTotal = 0,
                    CustoMedio = 0
                };

                return ResultadoDto<EstatisticasWhatsAppDto>.ComSucesso(estatisticas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter estatísticas");
                return ResultadoDto<EstatisticasWhatsAppDto>.ComErro($"Erro ao obter estatísticas: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<StatusEnvioWhatsApp>> ObterStatusMensagemAsync(string messageId)
        {
            try
            {
                // TODO: Consultar status real no provedor
                return ResultadoDto<StatusEnvioWhatsApp>.ComSucesso(StatusEnvioWhatsApp.Entregue);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter status da mensagem {MessageId}", messageId);
                return ResultadoDto<StatusEnvioWhatsApp>.ComErro($"Erro ao obter status: {ex.Message}");
            }
        }

        #endregion

        #region Webhook (Implementação Básica)

        public async Task<ResultadoDto<bool>> ProcessarWebhookStatusAsync(string payload)
        {
            try
            {
                // TODO: Processar webhook real do provedor
                _logger.LogInformation("Webhook de status processado");
                return ResultadoDto<bool>.ComSucesso(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar webhook de status");
                return ResultadoDto<bool>.ComErro($"Erro no webhook: {ex.Message}");
            }
        }

        public async Task<ResultadoDto<bool>> ProcessarMensagemRecebidaAsync(string payload)
        {
            try
            {
                // TODO: Processar mensagem recebida
                _logger.LogInformation("Mensagem recebida processada");
                return ResultadoDto<bool>.ComSucesso(true);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao processar mensagem recebida");
                return ResultadoDto<bool>.ComErro($"Erro ao processar mensagem: {ex.Message}");
            }
        }

        #endregion

        #region Cobrança Mensal

        public async Task<ResultadoWhatsAppDto> EnviarCobrancaMensalAsync(int clienteId, int mes, int ano, string? chavePix = null)
        {
            try
            {
                var dadosCobranca = await ObterDadosCobrancaMensalAsync(clienteId, mes, ano);
                if (dadosCobranca == null)
                {
                    return new ResultadoWhatsAppDto
                    {
                        Status = StatusEnvioWhatsApp.Erro,
                        MensagemErro = "Cliente não encontrado ou não possui sessões no período informado",
                        DataEnvio = DateTime.Now
                    };
                }

                // Usar chave PIX fornecida ou padrão
                var chave = chavePix ?? dadosCobranca.ChavePix;

                // Criar mensagem personalizada baseada no Google Sheets
                var mensagem = CriarMensagemCobrancaMensal(dadosCobranca, chave);

                var envioDto = new EnviarWhatsAppDto
                {
                    Telefone = dadosCobranca.Telefone,
                    Mensagem = mensagem,
                    Tipo = TipoMensagemWhatsApp.Texto,
                    ClienteId = clienteId
                };

                var resultado = await EnviarMensagemAsync(envioDto);
                return resultado.Dados ?? new ResultadoWhatsAppDto
                {
                    Status = StatusEnvioWhatsApp.Erro,
                    MensagemErro = "Erro ao enviar mensagem",
                    DataEnvio = DateTime.Now
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar cobrança mensal para cliente {ClienteId}", clienteId);
                return new ResultadoWhatsAppDto
                {
                    Status = StatusEnvioWhatsApp.Erro,
                    MensagemErro = ex.Message,
                    DataEnvio = DateTime.Now
                };
            }
        }

        public async Task<ResultadoCobrancaMensalDto> EnviarCobrancaMensalLoteAsync(int mes, int ano, string? chavePix = null)
        {
            var resultado = new ResultadoCobrancaMensalDto();

            try
            {
                // Buscar todos os clientes que tiveram sessões no mês
                var dataInicio = new DateTime(ano, mes, 1);
                var dataFim = dataInicio.AddMonths(1).AddDays(-1);

                var sessoesDoMes = await _sessaoRepository.ObterSessoesPorPeriodoAsync(dataInicio, dataFim);
                var clientesComSessoes = sessoesDoMes
                    .Where(s => s.Status == StatusSessao.Realizada)
                    .GroupBy(s => s.ClienteId)
                    .Select(g => g.Key)
                    .ToList();

                resultado.TotalClientes = clientesComSessoes.Count;

                foreach (var clienteId in clientesComSessoes)
                {
                    try
                    {
                        var dadosCobranca = await ObterDadosCobrancaMensalAsync(clienteId, mes, ano);
                        if (dadosCobranca != null)
                        {
                            var resultadoEnvio = await EnviarCobrancaMensalAsync(clienteId, mes, ano, chavePix);

                            if (resultadoEnvio.Status == StatusEnvioWhatsApp.Enviado)
                            {
                                resultado.EnviosRealizados++;
                                resultado.ClientesProcessados.Add(dadosCobranca);
                            }
                            else
                            {
                                resultado.EnviosFalharam++;
                                resultado.Erros.Add($"Cliente {dadosCobranca.NomeCliente}: {resultadoEnvio.MensagemErro}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        resultado.EnviosFalharam++;
                        resultado.Erros.Add($"Cliente ID {clienteId}: {ex.Message}");
                        _logger.LogError(ex, "Erro ao processar cliente {ClienteId}", clienteId);
                    }
                }

                _logger.LogInformation("Cobrança mensal enviada para {Enviados}/{Total} clientes",
                    resultado.EnviosRealizados, resultado.TotalClientes);

                return resultado;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao enviar cobrança mensal em lote");
                resultado.Erros.Add($"Erro geral: {ex.Message}");
                return resultado;
            }
        }

        public async Task<CobrancaMensalDto?> ObterDadosCobrancaMensalAsync(int clienteId, int mes, int ano)
        {
            try
            {
                var cliente = await _clienteRepository.ObterPorIdAsync(clienteId);
                if (cliente == null || !cliente.Ativo)
                    return null;

                var dataInicio = new DateTime(ano, mes, 1);
                var dataFim = dataInicio.AddMonths(1).AddDays(-1);

                var sessoesDoMes = await _sessaoRepository.ObterSessoesPorClientePeriodoAsync(clienteId, dataInicio, dataFim);
                var sessoesRealizadas = sessoesDoMes.Where(s => s.Status == StatusSessao.Realizada).ToList();

                if (!sessoesRealizadas.Any())
                    return null;

                var quantidadeSessoes = sessoesRealizadas.Count;
                var valorTotal = sessoesRealizadas.Sum(s => s.Valor);

                // Calcular data de vencimento (5º dia útil do mês seguinte)
                var proximoMes = dataInicio.AddMonths(1);
                var dataVencimento = CalcularQuintoDiaUtil(proximoMes);

                return new CobrancaMensalDto
                {
                    ClienteId = clienteId,
                    NomeCliente = cliente.Nome,
                    Telefone = cliente.Telefone,
                    MesReferencia = dataInicio.ToString("MMMM/yyyy", new System.Globalization.CultureInfo("pt-BR")),
                    QuantidadeSessoes = quantidadeSessoes,
                    ValorTotal = valorTotal,
                    DataVencimento = dataVencimento
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao obter dados de cobrança mensal para cliente {ClienteId}", clienteId);
                return null;
            }
        }

        #endregion

        #region Métodos Privados para Cobrança Mensal

        private string CriarMensagemCobrancaMensal(CobrancaMensalDto dados, string chavePix)
        {
            return $"Oi, bom dia {dados.NomeCliente}!\n\n" +
                   $"Estou entrando em contato para informar o valor total das sessões realizadas em {dados.MesReferencia}. " +
                   $"Foram {dados.QuantidadeSessoes} sessões, totalizando {dados.ValorTotal:C}.\n\n" +
                   $"O pagamento pode ser feito via Pix (chave: {chavePix}) até o 5º dia útil ou em dinheiro na próxima sessão.\n\n" +
                   $"Por gentileza, encaminhe o comprovante pelo WhatsApp para que eu possa registrar o pagamento.\n\n" +
                   $"Qualquer dúvida, estou à disposição.\n" +
                   $"Obrigada!";
        }

        private DateTime CalcularQuintoDiaUtil(DateTime mes)
        {
            var data = new DateTime(mes.Year, mes.Month, 1);
            var diasUteis = 0;

            while (diasUteis < 5)
            {
                if (data.DayOfWeek != DayOfWeek.Saturday && data.DayOfWeek != DayOfWeek.Sunday)
                {
                    diasUteis++;
                }

                if (diasUteis < 5)
                {
                    data = data.AddDays(1);
                }
            }

            return data;
        }

        #endregion
    }
}
