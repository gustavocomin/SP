using Microsoft.AspNetCore.Mvc;
using SP.Aplicacao.Services.Interfaces;

namespace SP.Api.Controllers;

/// <summary>
/// Controller para relatórios financeiros
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class FinanceiroController(IFinanceiroAppService financeiroAppService) : ControllerBase
{
    /// <summary>
    /// Obtém dashboard financeiro
    /// </summary>
    [HttpGet("dashboard")]
    public async Task<IActionResult> ObterDashboard()
    {
        var resultado = await financeiroAppService.ObterDashboardFinanceiroAsync();
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém relatório financeiro mensal
    /// </summary>
    [HttpGet("mensal/{ano:int}/{mes:int}")]
    public async Task<IActionResult> ObterRelatorioMensal(int ano, int mes)
    {
        var resultado = await financeiroAppService.ObterRelatorioMensalAsync(ano, mes);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém relatório financeiro do mês atual
    /// </summary>
    [HttpGet("mensal/atual")]
    public async Task<IActionResult> ObterRelatorioMesAtual()
    {
        var resultado = await financeiroAppService.ObterRelatorioMesAtualAsync();
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém relatório financeiro anual
    /// </summary>
    [HttpGet("anual/{ano:int}")]
    public async Task<IActionResult> ObterRelatorioAnual(int ano)
    {
        var resultado = await financeiroAppService.ObterRelatorioAnualAsync(ano);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém relatório financeiro do ano atual
    /// </summary>
    [HttpGet("anual/atual")]
    public async Task<IActionResult> ObterRelatorioAnoAtual()
    {
        var resultado = await financeiroAppService.ObterRelatorioAnoAtualAsync();
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém comparativo entre dois meses
    /// </summary>
    [HttpGet("comparativo")]
    public async Task<IActionResult> ObterComparativoMensal(
        [FromQuery] int ano1, 
        [FromQuery] int mes1, 
        [FromQuery] int ano2, 
        [FromQuery] int mes2)
    {
        var resultado = await financeiroAppService.ObterComparativoMensalAsync(ano1, mes1, ano2, mes2);
        
        if (resultado.Sucesso)
            return Ok(resultado.Dados);
            
        return BadRequest(resultado);
    }

    /// <summary>
    /// Obtém relatórios dos últimos 12 meses
    /// </summary>
    [HttpGet("ultimos-12-meses")]
    public async Task<IActionResult> ObterUltimos12Meses()
    {
        var hoje = DateTime.Today;
        var relatorios = new List<object>();

        for (int i = 11; i >= 0; i--)
        {
            var data = hoje.AddMonths(-i);
            var resultado = await financeiroAppService.ObterRelatorioMensalAsync(data.Year, data.Month);
            
            if (resultado.Sucesso)
            {
                relatorios.Add(new
                {
                    Ano = data.Year,
                    Mes = data.Month,
                    NomeMes = resultado.Dados!.NomeMes,
                    TotalSessoes = resultado.Dados.TotalSessoesRealizadas,
                    ValorFaturado = resultado.Dados.ValorTotalFaturado,
                    ValorPago = resultado.Dados.ValorTotalPago,
                    ValorAReceber = resultado.Dados.ValorTotalAReceber,
                    PercentualPagamento = resultado.Dados.PercentualPagamento
                });
            }
        }

        return Ok(relatorios);
    }

    /// <summary>
    /// Obtém evolução anual dos últimos anos
    /// </summary>
    [HttpGet("evolucao-anual")]
    public async Task<IActionResult> ObterEvolucaoAnual([FromQuery] int anos = 3)
    {
        var anoAtual = DateTime.Today.Year;
        var evolucao = new List<object>();

        for (int i = anos - 1; i >= 0; i--)
        {
            var ano = anoAtual - i;
            var resultado = await financeiroAppService.ObterRelatorioAnualAsync(ano);
            
            if (resultado.Sucesso)
            {
                evolucao.Add(new
                {
                    Ano = ano,
                    TotalSessoes = resultado.Dados!.TotalSessoesRealizadas,
                    ValorFaturado = resultado.Dados.ValorTotalFaturado,
                    ValorPago = resultado.Dados.ValorTotalPago,
                    ValorAReceber = resultado.Dados.ValorTotalAReceber,
                    PercentualPagamento = resultado.Dados.PercentualPagamento,
                    ValorMedioSessao = resultado.Dados.ValorMedioSessao
                });
            }
        }

        return Ok(evolucao);
    }

    /// <summary>
    /// Obtém resumo financeiro rápido
    /// </summary>
    [HttpGet("resumo")]
    public async Task<IActionResult> ObterResumoFinanceiro()
    {
        var dashboard = await financeiroAppService.ObterDashboardFinanceiroAsync();
        var relatorioMesAtual = await financeiroAppService.ObterRelatorioMesAtualAsync();
        var relatorioAnoAtual = await financeiroAppService.ObterRelatorioAnoAtualAsync();

        if (!dashboard.Sucesso || !relatorioMesAtual.Sucesso || !relatorioAnoAtual.Sucesso)
        {
            return BadRequest("Erro ao gerar resumo financeiro");
        }

        var resumo = new
        {
            // Dashboard
            ValorTotalEmAberto = dashboard.Dados!.ValorTotalEmAberto,
            QuantidadeSessoesNaoPagas = dashboard.Dados.QuantidadeSessoesNaoPagas,
            CrescimentoMensal = dashboard.Dados.CrescimentoMensal,
            
            // Mês atual
            MesAtual = new
            {
                Mes = relatorioMesAtual.Dados!.NomeMes,
                TotalSessoes = relatorioMesAtual.Dados.TotalSessoesRealizadas,
                ValorFaturado = relatorioMesAtual.Dados.ValorTotalFaturado,
                ValorPago = relatorioMesAtual.Dados.ValorTotalPago,
                PercentualPagamento = relatorioMesAtual.Dados.PercentualPagamento
            },
            
            // Ano atual
            AnoAtual = new
            {
                Ano = relatorioAnoAtual.Dados!.Ano,
                TotalSessoes = relatorioAnoAtual.Dados.TotalSessoesRealizadas,
                ValorFaturado = relatorioAnoAtual.Dados.ValorTotalFaturado,
                ValorPago = relatorioAnoAtual.Dados.ValorTotalPago,
                PercentualPagamento = relatorioAnoAtual.Dados.PercentualPagamento
            },
            
            // Top 3 clientes com maior débito
            TopClientesDebito = dashboard.Dados.ClientesComMaiorDebito.Take(3),
            
            // Próximos vencimentos
            ProximosVencimentos = dashboard.Dados.ProximosVencimentos.Take(5)
        };

        return Ok(resumo);
    }

    /// <summary>
    /// Obtém métricas para gráficos
    /// </summary>
    [HttpGet("metricas")]
    public async Task<IActionResult> ObterMetricas([FromQuery] string tipo = "mensal", [FromQuery] int periodo = 12)
    {
        try
        {
            if (tipo.ToLower() == "anual")
            {
                var anoAtual = DateTime.Today.Year;
                var metricas = new List<object>();

                for (int i = periodo - 1; i >= 0; i--)
                {
                    var ano = anoAtual - i;
                    var resultado = await financeiroAppService.ObterRelatorioAnualAsync(ano);
                    
                    if (resultado.Sucesso)
                    {
                        metricas.Add(new
                        {
                            Periodo = ano.ToString(),
                            Sessoes = resultado.Dados!.TotalSessoesRealizadas,
                            Faturado = resultado.Dados.ValorTotalFaturado,
                            Pago = resultado.Dados.ValorTotalPago,
                            AReceber = resultado.Dados.ValorTotalAReceber
                        });
                    }
                }

                return Ok(metricas);
            }
            else
            {
                var hoje = DateTime.Today;
                var metricas = new List<object>();

                for (int i = periodo - 1; i >= 0; i--)
                {
                    var data = hoje.AddMonths(-i);
                    var resultado = await financeiroAppService.ObterRelatorioMensalAsync(data.Year, data.Month);
                    
                    if (resultado.Sucesso)
                    {
                        metricas.Add(new
                        {
                            Periodo = $"{resultado.Dados!.NomeMes}/{data.Year}",
                            Sessoes = resultado.Dados.TotalSessoesRealizadas,
                            Faturado = resultado.Dados.ValorTotalFaturado,
                            Pago = resultado.Dados.ValorTotalPago,
                            AReceber = resultado.Dados.ValorTotalAReceber
                        });
                    }
                }

                return Ok(metricas);
            }
        }
        catch (Exception ex)
        {
            return BadRequest($"Erro ao obter métricas: {ex.Message}");
        }
    }
}
