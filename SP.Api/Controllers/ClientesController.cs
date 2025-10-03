using Microsoft.AspNetCore.Mvc;
using SP.Aplicacao.DTOs.Clientes;
using SP.Aplicacao.Services.Interfaces;
using SP.Dominio.Enums;

namespace SP.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientesController(IClienteAppService clienteAppService) : ControllerBase
    {
        private readonly IClienteAppService _clienteAppService = clienteAppService;

        [HttpGet]
        public async Task<ActionResult<List<ClienteResumoDto>>> ObterTodos()
        {
            var resultado = await _clienteAppService.ObterTodosAsync();

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ClienteDto>> ObterPorId(int id)
        {
            var resultado = await _clienteAppService.ObterPorIdAsync(id);

            if (!resultado.Sucesso)
                return NotFound(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpPost]
        public async Task<ActionResult<ClienteDto>> Criar(CriarClienteDto criarClienteDto)
        {
            var resultado = await _clienteAppService.CriarAsync(criarClienteDto);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return CreatedAtAction(nameof(ObterPorId),
                new { id = resultado.Dados!.Id }, resultado.Dados);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ClienteDto>> Atualizar(int id, [FromBody] AtualizarClienteDto atualizarClienteDto)
        {
            if (id != atualizarClienteDto.Id)
                return BadRequest("ID do cliente não confere");

            var resultado = await _clienteAppService.AtualizarAsync(atualizarClienteDto);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Remover(int id)
        {
            var resultado = await _clienteAppService.RemoverAsync(id);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return NoContent();
        }

        [HttpGet("cpf/{cpf}")]
        public async Task<ActionResult<ClienteDto>> ObterPorCPF(string cpf)
        {
            var resultado = await _clienteAppService.ObterPorCPFAsync(cpf);

            if (!resultado.Sucesso)
                return NotFound(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("buscar/{nome}")]
        public async Task<ActionResult<List<ClienteResumoDto>>> BuscarPorNome(string nome)
        {
            var resultado = await _clienteAppService.ObterPorNomeAsync(nome);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("inadimplentes")]
        public async Task<ActionResult<List<ClienteResumoDto>>> ObterInadimplentes()
        {
            var resultado = await _clienteAppService.ObterInadimplentesAsync();

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("em-dia")]
        public async Task<ActionResult<List<ClienteResumoDto>>> ObterEmDia()
        {
            var resultado = await _clienteAppService.ObterEmDiaAsync();

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("vencimento-hoje")]
        public async Task<ActionResult<List<ClienteResumoDto>>> ObterComVencimentoHoje()
        {
            var resultado = await _clienteAppService.ObterComVencimentoHojeAsync();

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("vencimento-proximos/{dias}")]
        public async Task<ActionResult<List<ClienteResumoDto>>> ObterComVencimentoProximos(int dias = 7)
        {
            var resultado = await _clienteAppService.ObterComVencimentoProximosDiasAsync(dias);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpGet("estatisticas")]
        public async Task<ActionResult<EstatisticasClientesDto>> ObterEstatisticas()
        {
            var resultado = await _clienteAppService.ObterEstatisticasAsync();

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Dados);
        }

        [HttpPatch("ativar")]
        public async Task<ActionResult> AtivarClientes([FromBody] List<int> ids)
        {
            var resultado = await _clienteAppService.AtivarClientesAsync(ids);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Mensagem);
        }

        [HttpPatch("desativar")]
        public async Task<ActionResult> DesativarClientes([FromBody] List<int> ids)
        {
            var resultado = await _clienteAppService.DesativarClientesAsync(ids);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Mensagem);
        }

        [HttpPatch("{id}/status-financeiro")]
        public async Task<ActionResult> AtualizarStatusFinanceiro(int id, [FromBody] StatusFinanceiro novoStatus)
        {
            var resultado = await _clienteAppService.AtualizarStatusFinanceiroAsync(id, novoStatus);

            if (!resultado.Sucesso)
                return BadRequest(resultado.Erros);

            return Ok(resultado.Mensagem);
        }
    }
}
