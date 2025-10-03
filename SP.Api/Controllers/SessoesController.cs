using Microsoft.AspNetCore.Mvc;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Aplicacao.Services.Interfaces;
using SP.Dominio.Enums;

namespace SP.Api.Controllers;

/// <summary>
/// Controller para gerenciamento de sessões
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class SessoesController(ISessaoAppService sessaoAppService) : ControllerBase
{
    /// <summary>
    /// Obtém todas as sessões
    /// </summary>
    [HttpGet]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterTodos()
    {
        var resultado = await sessaoAppService.ObterTodosAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém uma sessão por ID
    /// </summary>
    [HttpGet("{id}")]
    public async Task<ActionResult<SessaoDto>> ObterPorId(int id)
    {
        var resultado = await sessaoAppService.ObterPorIdAsync(id);
        
        if (!resultado.Sucesso)
            return NotFound(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Cria uma nova sessão
    /// </summary>
    [HttpPost]
    public async Task<ActionResult<SessaoDto>> Criar([FromBody] CriarSessaoDto criarSessaoDto)
    {
        var resultado = await sessaoAppService.CriarAsync(criarSessaoDto);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return CreatedAtAction(nameof(ObterPorId), 
            new { id = resultado.Dados!.Id }, resultado.Dados);
    }

    /// <summary>
    /// Atualiza uma sessão existente
    /// </summary>
    [HttpPut("{id}")]
    public async Task<ActionResult<SessaoDto>> Atualizar(int id, [FromBody] AtualizarSessaoDto atualizarSessaoDto)
    {
        var resultado = await sessaoAppService.AtualizarAsync(id, atualizarSessaoDto);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Remove uma sessão
    /// </summary>
    [HttpDelete("{id}")]
    public async Task<ActionResult> Remover(int id)
    {
        var resultado = await sessaoAppService.RemoverAsync(id);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return NoContent();
    }

    /// <summary>
    /// Obtém sessões de um cliente
    /// </summary>
    [HttpGet("cliente/{clienteId}")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterPorCliente(int clienteId)
    {
        var resultado = await sessaoAppService.ObterPorClienteAsync(clienteId);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões por período
    /// </summary>
    [HttpGet("periodo")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterPorPeriodo(
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim)
    {
        var resultado = await sessaoAppService.ObterPorPeriodoAsync(dataInicio, dataFim);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões de um cliente por período
    /// </summary>
    [HttpGet("cliente/{clienteId}/periodo")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterPorClientePeriodo(
        int clienteId,
        [FromQuery] DateTime dataInicio, 
        [FromQuery] DateTime dataFim)
    {
        var resultado = await sessaoAppService.ObterPorClientePeriodoAsync(clienteId, dataInicio, dataFim);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões por status
    /// </summary>
    [HttpGet("status/{status}")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterPorStatus(StatusSessao status)
    {
        var resultado = await sessaoAppService.ObterPorStatusAsync(status);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões de hoje
    /// </summary>
    [HttpGet("hoje")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesHoje()
    {
        var resultado = await sessaoAppService.ObterSessoesHojeAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões de uma data específica
    /// </summary>
    [HttpGet("data")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterPorData([FromQuery] DateTime data)
    {
        var resultado = await sessaoAppService.ObterPorDataAsync(data);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões não pagas
    /// </summary>
    [HttpGet("nao-pagas")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesNaoPagas()
    {
        var resultado = await sessaoAppService.ObterSessoesNaoPagasAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões não pagas de um cliente
    /// </summary>
    [HttpGet("cliente/{clienteId}/nao-pagas")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesNaoPagasPorCliente(int clienteId)
    {
        var resultado = await sessaoAppService.ObterSessoesNaoPagasPorClienteAsync(clienteId);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões para faturamento do mês
    /// </summary>
    [HttpGet("faturamento")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesParaFaturamento(
        [FromQuery] int ano, 
        [FromQuery] int mes)
    {
        var resultado = await sessaoAppService.ObterSessoesParaFaturamentoAsync(ano, mes);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém sessões de um cliente no mês
    /// </summary>
    [HttpGet("cliente/{clienteId}/mes")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesClienteMes(
        int clienteId,
        [FromQuery] int ano, 
        [FromQuery] int mes)
    {
        var resultado = await sessaoAppService.ObterSessoesClienteMesAsync(clienteId, ano, mes);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém próximas sessões
    /// </summary>
    [HttpGet("proximas")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterProximasSessoes()
    {
        var resultado = await sessaoAppService.ObterProximasSessoesAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Obtém estatísticas de sessões
    /// </summary>
    [HttpGet("estatisticas")]
    public async Task<ActionResult<EstatisticasSessoesDto>> ObterEstatisticas()
    {
        var resultado = await sessaoAppService.ObterEstatisticasAsync();
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Marca sessão como realizada
    /// </summary>
    [HttpPatch("{id}/realizar")]
    public async Task<ActionResult> MarcarComoRealizada(
        int id, 
        [FromBody] MarcarRealizadaRequest request)
    {
        var resultado = await sessaoAppService.MarcarComoRealizadaAsync(
            id, 
            request.DataHoraRealizada, 
            request.DuracaoRealMinutos, 
            request.AnotacoesClinicas);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(new { message = resultado.Mensagem });
    }

    /// <summary>
    /// Marca sessão como paga
    /// </summary>
    [HttpPatch("{id}/pagar")]
    public async Task<ActionResult> MarcarComoPaga(int id, [FromBody] MarcarPagaRequest request)
    {
        var resultado = await sessaoAppService.MarcarComoPagaAsync(id, request.FormaPagamento);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(new { message = resultado.Mensagem });
    }

    /// <summary>
    /// Marca sessões como pagas em lote
    /// </summary>
    [HttpPatch("pagar-lote")]
    public async Task<ActionResult> MarcarSessoesComoPagas([FromBody] MarcarPagasLoteRequest request)
    {
        var resultado = await sessaoAppService.MarcarSessoesComoPagasAsync(request.SessaoIds, request.FormaPagamento);
        
        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);
            
        return Ok(new { message = resultado.Mensagem });
    }

    /// <summary>
    /// Cancela sessão
    /// </summary>
    [HttpPatch("{id}/cancelar")]
    public async Task<ActionResult> CancelarSessao(int id, [FromBody] CancelarSessaoRequest request)
    {
        var resultado = await sessaoAppService.CancelarSessaoAsync(id, request.Motivo, request.NovoStatus);

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(new { message = resultado.Mensagem });
    }

    /// <summary>
    /// Cancela sessões em lote
    /// </summary>
    [HttpPatch("cancelar-lote")]
    public async Task<ActionResult> CancelarSessoes([FromBody] CancelarSessoesLoteRequest request)
    {
        var resultado = await sessaoAppService.CancelarSessoesAsync(request.SessaoIds, request.Motivo, request.NovoStatus);

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(new { message = resultado.Mensagem });
    }

    /// <summary>
    /// Reagenda sessão
    /// </summary>
    [HttpPost("{id}/reagendar")]
    public async Task<ActionResult<SessaoDto>> ReagendarSessao(int id, [FromBody] ReagendarSessaoRequest request)
    {
        var resultado = await sessaoAppService.ReagendarSessaoAsync(id, request.NovaDataHora, request.Motivo);

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Gera sessões recorrentes
    /// </summary>
    [HttpPost("gerar-recorrentes")]
    public async Task<ActionResult<List<SessaoResumoDto>>> GerarSessoesRecorrentes([FromBody] GerarSessoesRecorrentesDto gerarSessoesDto)
    {
        var resultado = await sessaoAppService.GerarSessoesRecorrentesAsync(gerarSessoesDto);

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(resultado.Dados);
    }

    /// <summary>
    /// Verifica conflito de horário
    /// </summary>
    [HttpGet("verificar-conflito")]
    public async Task<ActionResult<bool>> VerificarConflitoHorario(
        [FromQuery] DateTime dataHora,
        [FromQuery] int duracaoMinutos,
        [FromQuery] int? sessaoIdExcluir = null)
    {
        var resultado = await sessaoAppService.VerificarConflitoHorarioAsync(dataHora, duracaoMinutos, sessaoIdExcluir);

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(new { temConflito = resultado.Dados });
    }

    /// <summary>
    /// Obtém sessões que precisam de confirmação
    /// </summary>
    [HttpGet("para-confirmacao")]
    public async Task<ActionResult<List<SessaoResumoDto>>> ObterSessoesParaConfirmacao()
    {
        var resultado = await sessaoAppService.ObterSessoesParaConfirmacaoAsync();

        if (!resultado.Sucesso)
            return BadRequest(resultado.Erros);

        return Ok(resultado.Dados);
    }
}

/// <summary>
/// Request para marcar sessão como realizada
/// </summary>
public class MarcarRealizadaRequest
{
    public DateTime? DataHoraRealizada { get; set; }
    public int? DuracaoRealMinutos { get; set; }
    public string? AnotacoesClinicas { get; set; }
}

/// <summary>
/// Request para marcar sessão como paga
/// </summary>
public class MarcarPagaRequest
{
    public string FormaPagamento { get; set; } = string.Empty;
}

/// <summary>
/// Request para marcar sessões como pagas em lote
/// </summary>
public class MarcarPagasLoteRequest
{
    public List<int> SessaoIds { get; set; } = new();
    public string FormaPagamento { get; set; } = string.Empty;
}

/// <summary>
/// Request para cancelar sessão
/// </summary>
public class CancelarSessaoRequest
{
    public string Motivo { get; set; } = string.Empty;
    public StatusSessao NovoStatus { get; set; }
}

/// <summary>
/// Request para cancelar sessões em lote
/// </summary>
public class CancelarSessoesLoteRequest
{
    public List<int> SessaoIds { get; set; } = new();
    public string Motivo { get; set; } = string.Empty;
    public StatusSessao NovoStatus { get; set; }
}

/// <summary>
/// Request para reagendar sessão
/// </summary>
public class ReagendarSessaoRequest
{
    public DateTime NovaDataHora { get; set; }
    public string? Motivo { get; set; }
}
