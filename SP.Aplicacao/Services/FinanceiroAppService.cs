using AutoMapper;
using SP.Aplicacao.DTOs.Common;
using SP.Aplicacao.DTOs.Financeiro;
using SP.Aplicacao.DTOs.Clientes;
using SP.Aplicacao.Services.Interfaces;
using SP.Infraestrutura.Entities.Sessoes;
using SP.Infraestrutura.Entities.Clientes;
using System.Globalization;

namespace SP.Aplicacao.Services;

/// <summary>
/// Application Service para relatórios financeiros
/// </summary>
public class FinanceiroAppService(
    ISessaoRepository sessaoRepository,
    IClienteRepository clienteRepository,
    IMapper mapper) : IFinanceiroAppService
{
    private static readonly string[] NomesMeses = 
    {
        "", "Janeiro", "Fevereiro", "Março", "Abril", "Maio", "Junho",
        "Julho", "Agosto", "Setembro", "Outubro", "Novembro", "Dezembro"
    };

    public async Task<ResultadoDto<RelatorioFinanceiroMensalDto>> ObterRelatorioMensalAsync(int ano, int mes)
    {
        try
        {
            if (mes < 1 || mes > 12)
            {
                return ResultadoDto<RelatorioFinanceiroMensalDto>.ComErro("Mês deve estar entre 1 e 12");
            }

            // Obter dados básicos do mês
            var dadosBasicos = await sessaoRepository.ObterDadosFinanceirosMensalAsync(ano, mes);
            
            // Obter detalhes por cliente
            var dadosPorCliente = await sessaoRepository.ObterSessoesPorClienteMensalAsync(ano, mes);
            
            // Obter detalhes por forma de pagamento
            var dadosPorFormaPagamento = await sessaoRepository.ObterSessoesPorFormaPagamentoMensalAsync(ano, mes);
            
            // Obter sessões não pagas
            var sessoesNaoPagas = await sessaoRepository.ObterSessoesNaoPagasMensalAsync(ano, mes);

            var valorAReceber = dadosBasicos.ValorFaturado - dadosBasicos.ValorPago;
            var percentualPagamento = dadosBasicos.ValorFaturado > 0 
                ? (dadosBasicos.ValorPago / dadosBasicos.ValorFaturado) * 100 
                : 0;
            var valorMedioSessao = dadosBasicos.TotalRealizadas > 0 
                ? dadosBasicos.ValorFaturado / dadosBasicos.TotalRealizadas 
                : 0;

            // Calcular total por forma de pagamento para percentuais
            var totalPorFormaPagamento = dadosPorFormaPagamento.Sum(x => x.ValorTotal);

            var relatorio = new RelatorioFinanceiroMensalDto
            {
                Ano = ano,
                Mes = mes,
                NomeMes = NomesMeses[mes],
                TotalSessoesRealizadas = dadosBasicos.TotalRealizadas,
                TotalSessoesCanceladas = dadosBasicos.TotalCanceladas,
                TotalFaltas = dadosBasicos.TotalFaltas,
                ValorTotalFaturado = dadosBasicos.ValorFaturado,
                ValorTotalPago = dadosBasicos.ValorPago,
                ValorTotalAReceber = valorAReceber,
                PercentualPagamento = percentualPagamento,
                ValorMedioSessao = valorMedioSessao,
                
                DetalhesPorCliente = dadosPorCliente.Select(x => new RelatorioClienteMensalDto
                {
                    Cliente = new ClienteResumoDto { Id = x.ClienteId, Nome = x.NomeCliente },
                    QuantidadeSessoes = x.QuantidadeSessoes,
                    ValorTotalFaturado = x.ValorFaturado,
                    ValorTotalPago = x.ValorPago,
                    ValorEmAberto = x.ValorFaturado - x.ValorPago,
                    PercentualPagamento = x.ValorFaturado > 0 ? (x.ValorPago / x.ValorFaturado) * 100 : 0,
                    UltimaDataPagamento = x.UltimaDataPagamento
                }).ToList(),
                
                DetalhesPorFormaPagamento = dadosPorFormaPagamento.Select(x => new RelatorioFormaPagamentoDto
                {
                    FormaPagamento = x.FormaPagamento,
                    QuantidadeSessoes = x.QuantidadeSessoes,
                    ValorTotal = x.ValorTotal,
                    PercentualDoTotal = totalPorFormaPagamento > 0 ? (x.ValorTotal / totalPorFormaPagamento) * 100 : 0
                }).ToList(),
                
                SessoesNaoPagas = sessoesNaoPagas.Select(x => new SessaoNaoPagaDto
                {
                    Id = x.Id,
                    NomeCliente = x.NomeCliente,
                    DataSessao = x.DataSessao,
                    Valor = x.Valor,
                    DiasEmAtraso = (DateTime.Today - x.DataSessao.Date).Days
                }).ToList()
            };

            return ResultadoDto<RelatorioFinanceiroMensalDto>.ComSucesso(relatorio, "Relatório mensal gerado com sucesso");
        }
        catch (Exception ex)
        {
            return ResultadoDto<RelatorioFinanceiroMensalDto>.ComErro($"Erro ao gerar relatório mensal: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<RelatorioFinanceiroAnualDto>> ObterRelatorioAnualAsync(int ano)
    {
        try
        {
            // Obter dados anuais agrupados por mês
            var dadosAnuais = await sessaoRepository.ObterDadosFinanceirosAnualAsync(ano);
            
            var totalSessoes = dadosAnuais.Sum(x => x.QuantidadeSessoes);
            var valorTotalFaturado = dadosAnuais.Sum(x => x.ValorFaturado);
            var valorTotalPago = dadosAnuais.Sum(x => x.ValorPago);
            var valorTotalAReceber = valorTotalFaturado - valorTotalPago;
            var percentualPagamento = valorTotalFaturado > 0 ? (valorTotalPago / valorTotalFaturado) * 100 : 0;
            var valorMedioSessao = totalSessoes > 0 ? valorTotalFaturado / totalSessoes : 0;

            // Encontrar melhor mês
            var melhorMes = dadosAnuais.OrderByDescending(x => x.ValorFaturado).FirstOrDefault();

            var relatorio = new RelatorioFinanceiroAnualDto
            {
                Ano = ano,
                TotalSessoesRealizadas = totalSessoes,
                ValorTotalFaturado = valorTotalFaturado,
                ValorTotalPago = valorTotalPago,
                ValorTotalAReceber = valorTotalAReceber,
                PercentualPagamento = percentualPagamento,
                ValorMedioSessao = valorMedioSessao,
                
                MelhorMes = melhorMes.QuantidadeSessoes > 0 ? new MelhorMesDto
                {
                    Mes = melhorMes.Mes,
                    NomeMes = NomesMeses[melhorMes.Mes],
                    ValorFaturado = melhorMes.ValorFaturado,
                    QuantidadeSessoes = melhorMes.QuantidadeSessoes
                } : null,
                
                RelatoriosMensais = dadosAnuais.Select(x => new RelatorioMensalResumoDto
                {
                    Mes = x.Mes,
                    NomeMes = NomesMeses[x.Mes],
                    QuantidadeSessoes = x.QuantidadeSessoes,
                    ValorFaturado = x.ValorFaturado,
                    ValorPago = x.ValorPago,
                    ValorAReceber = x.ValorFaturado - x.ValorPago,
                    PercentualPagamento = x.ValorFaturado > 0 ? (x.ValorPago / x.ValorFaturado) * 100 : 0
                }).ToList(),
                
                EvolucaoMensal = CalcularEvolucaoMensal(dadosAnuais)
            };

            return ResultadoDto<RelatorioFinanceiroAnualDto>.ComSucesso(relatorio, "Relatório anual gerado com sucesso");
        }
        catch (Exception ex)
        {
            return ResultadoDto<RelatorioFinanceiroAnualDto>.ComErro($"Erro ao gerar relatório anual: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<DashboardFinanceiroDto>> ObterDashboardFinanceiroAsync()
    {
        try
        {
            var hoje = DateTime.Today;
            var mesAtual = hoje.Month;
            var anoAtual = hoje.Year;
            var mesAnterior = mesAtual == 1 ? 12 : mesAtual - 1;
            var anoMesAnterior = mesAtual == 1 ? anoAtual - 1 : anoAtual;

            // Valor total em aberto
            var valorTotalEmAberto = await sessaoRepository.ObterValorTotalNaoPagasAsync();
            
            // Quantidade de sessões não pagas
            var sessoesNaoPagas = await sessaoRepository.ObterSessoesNaoPagasAsync();
            var quantidadeSessoesNaoPagas = sessoesNaoPagas.Count;

            // Faturamento mês atual e anterior
            var dadosMesAtual = await sessaoRepository.ObterDadosFinanceirosMensalAsync(anoAtual, mesAtual);
            var dadosMesAnterior = await sessaoRepository.ObterDadosFinanceirosMensalAsync(anoMesAnterior, mesAnterior);

            var crescimentoMensal = dadosMesAnterior.ValorFaturado > 0 
                ? ((dadosMesAtual.ValorFaturado - dadosMesAnterior.ValorFaturado) / dadosMesAnterior.ValorFaturado) * 100 
                : 0;

            // Valor médio por sessão (últimos 3 meses)
            var valorMedioSessao = await CalcularValorMedioUltimosTresMeses(anoAtual, mesAtual);

            // Clientes com maior débito
            var clientesComMaiorDebito = await sessaoRepository.ObterClientesComMaiorDebitoAsync(5);
            
            // Próximos vencimentos
            var proximosVencimentos = await sessaoRepository.ObterProximosVencimentosAsync(7);

            var dashboard = new DashboardFinanceiroDto
            {
                ValorTotalEmAberto = valorTotalEmAberto,
                QuantidadeSessoesNaoPagas = quantidadeSessoesNaoPagas,
                FaturamentoMesAtual = dadosMesAtual.ValorFaturado,
                FaturamentoMesAnterior = dadosMesAnterior.ValorFaturado,
                CrescimentoMensal = crescimentoMensal,
                ValorMedioSessao = valorMedioSessao,
                
                ClientesComMaiorDebito = clientesComMaiorDebito.Select(x => new ClienteEmAbertoDto
                {
                    ClienteId = x.ClienteId,
                    NomeCliente = x.NomeCliente,
                    ValorEmAberto = x.ValorEmAberto,
                    QuantidadeSessoesNaoPagas = x.QuantidadeSessoes,
                    SessaoMaisAntigaNaoPaga = x.SessaoMaisAntiga,
                    DiasEmAtraso = x.SessaoMaisAntiga.HasValue ? (DateTime.Today - x.SessaoMaisAntiga.Value.Date).Days : 0
                }).ToList(),
                
                ProximosVencimentos = proximosVencimentos.Select(x => new ProximoVencimentoDto
                {
                    ClienteId = x.ClienteId,
                    NomeCliente = x.NomeCliente,
                    DataVencimento = x.DataVencimento,
                    ValorAVencer = x.ValorAVencer,
                    DiasAteVencimento = (x.DataVencimento - DateTime.Today).Days
                }).ToList()
            };

            return ResultadoDto<DashboardFinanceiroDto>.ComSucesso(dashboard, "Dashboard financeiro gerado com sucesso");
        }
        catch (Exception ex)
        {
            return ResultadoDto<DashboardFinanceiroDto>.ComErro($"Erro ao gerar dashboard financeiro: {ex.Message}");
        }
    }

    public async Task<ResultadoDto<RelatorioFinanceiroMensalDto>> ObterRelatorioMesAtualAsync()
    {
        var hoje = DateTime.Today;
        return await ObterRelatorioMensalAsync(hoje.Year, hoje.Month);
    }

    public async Task<ResultadoDto<RelatorioFinanceiroAnualDto>> ObterRelatorioAnoAtualAsync()
    {
        var hoje = DateTime.Today;
        return await ObterRelatorioAnualAsync(hoje.Year);
    }

    public async Task<ResultadoDto<ComparativoMensalDto>> ObterComparativoMensalAsync(int ano1, int mes1, int ano2, int mes2)
    {
        try
        {
            var dados1 = await sessaoRepository.ObterDadosFinanceirosMensalAsync(ano1, mes1);
            var dados2 = await sessaoRepository.ObterDadosFinanceirosMensalAsync(ano2, mes2);

            var comparativo = new ComparativoMensalDto
            {
                PrimeiroPeriodo = new PeriodoComparativoDto
                {
                    Ano = ano1,
                    Mes = mes1,
                    NomePeriodo = $"{NomesMeses[mes1]}/{ano1}",
                    TotalSessoes = dados1.TotalRealizadas,
                    ValorFaturado = dados1.ValorFaturado,
                    ValorPago = dados1.ValorPago,
                    ValorMedioSessao = dados1.TotalRealizadas > 0 ? dados1.ValorFaturado / dados1.TotalRealizadas : 0
                },
                
                SegundoPeriodo = new PeriodoComparativoDto
                {
                    Ano = ano2,
                    Mes = mes2,
                    NomePeriodo = $"{NomesMeses[mes2]}/{ano2}",
                    TotalSessoes = dados2.TotalRealizadas,
                    ValorFaturado = dados2.ValorFaturado,
                    ValorPago = dados2.ValorPago,
                    ValorMedioSessao = dados2.TotalRealizadas > 0 ? dados2.ValorFaturado / dados2.TotalRealizadas : 0
                },
                
                DiferencaSessoes = dados2.TotalRealizadas - dados1.TotalRealizadas,
                DiferencaValorFaturado = dados2.ValorFaturado - dados1.ValorFaturado,
                DiferencaValorPago = dados2.ValorPago - dados1.ValorPago,
                
                CrescimentoPercentualFaturamento = dados1.ValorFaturado > 0 
                    ? ((dados2.ValorFaturado - dados1.ValorFaturado) / dados1.ValorFaturado) * 100 
                    : 0,
                    
                CrescimentoPercentualPagamento = dados1.ValorPago > 0 
                    ? ((dados2.ValorPago - dados1.ValorPago) / dados1.ValorPago) * 100 
                    : 0
            };

            return ResultadoDto<ComparativoMensalDto>.ComSucesso(comparativo, "Comparativo mensal gerado com sucesso");
        }
        catch (Exception ex)
        {
            return ResultadoDto<ComparativoMensalDto>.ComErro($"Erro ao gerar comparativo mensal: {ex.Message}");
        }
    }

    private List<EvolucaoMensalDto> CalcularEvolucaoMensal(List<(int Mes, int QuantidadeSessoes, decimal ValorFaturado, decimal ValorPago)> dadosAnuais)
    {
        var evolucao = new List<EvolucaoMensalDto>();
        
        for (int i = 0; i < dadosAnuais.Count; i++)
        {
            var dadosMes = dadosAnuais[i];
            var evolucaoMes = new EvolucaoMensalDto
            {
                Mes = dadosMes.Mes,
                NomeMes = NomesMeses[dadosMes.Mes],
                ValorFaturado = dadosMes.ValorFaturado
            };

            if (i > 0)
            {
                var dadosMesAnterior = dadosAnuais[i - 1];
                if (dadosMesAnterior.ValorFaturado > 0)
                {
                    evolucaoMes.CrescimentoPercentual = ((dadosMes.ValorFaturado - dadosMesAnterior.ValorFaturado) / dadosMesAnterior.ValorFaturado) * 100;
                    evolucaoMes.DiferencaValor = dadosMes.ValorFaturado - dadosMesAnterior.ValorFaturado;
                }
            }

            evolucao.Add(evolucaoMes);
        }

        return evolucao;
    }

    private async Task<decimal> CalcularValorMedioUltimosTresMeses(int ano, int mes)
    {
        var totalSessoes = 0;
        var totalValor = 0m;

        for (int i = 0; i < 3; i++)
        {
            var mesCalculo = mes - i;
            var anoCalculo = ano;
            
            if (mesCalculo <= 0)
            {
                mesCalculo += 12;
                anoCalculo--;
            }

            var dados = await sessaoRepository.ObterDadosFinanceirosMensalAsync(anoCalculo, mesCalculo);
            totalSessoes += dados.TotalRealizadas;
            totalValor += dados.ValorFaturado;
        }

        return totalSessoes > 0 ? totalValor / totalSessoes : 0;
    }
}
