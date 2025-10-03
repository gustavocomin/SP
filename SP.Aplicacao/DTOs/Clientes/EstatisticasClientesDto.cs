namespace SP.Aplicacao.DTOs.Clientes
{
    public class EstatisticasClientesDto
    {
        public int TotalClientesAtivos { get; set; }
        public int TotalClientesInativos { get; set; }
        public int TotalClientesInadimplentes { get; set; }
        public int TotalClientesEmDia { get; set; }
        public decimal ValorTotalSessoes { get; set; }
        public decimal ValorMedioSessao { get; set; }
        public int ClientesComVencimentoHoje { get; set; }
        public int ClientesComVencimentoProximosDias { get; set; }
    }
}
