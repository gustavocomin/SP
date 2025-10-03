using AutoMapper;
using SP.Aplicacao.DTOs.Sessoes;
using SP.Dominio.Enums;
using SP.Dominio.Sessoes;

namespace SP.Aplicacao.Mappings;

/// <summary>
/// Profile do AutoMapper para mapeamento da entidade Sessao
/// </summary>
public class SessaoProfile : Profile
{
    public SessaoProfile()
    {
        // Mapeamento de Sessao para SessaoDto
        CreateMap<Sessao, SessaoDto>()
            .ForMember(dest => dest.Cliente, opt => opt.MapFrom(src => src.Cliente));

        // Mapeamento de Sessao para SessaoResumoDto
        CreateMap<Sessao, SessaoResumoDto>()
            .ForMember(dest => dest.NomeCliente, opt => opt.MapFrom(src => src.Cliente != null ? src.Cliente.Nome : string.Empty));

        // Mapeamento de CriarSessaoDto para Sessao
        CreateMap<CriarSessaoDto, Sessao>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => StatusSessao.Agendada))
            .ForMember(dest => dest.Pago, opt => opt.MapFrom(src => false))
            .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true))
            .ForMember(dest => dest.DataCriacao, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.DataHoraRealizada, opt => opt.Ignore())
            .ForMember(dest => dest.DuracaoRealMinutos, opt => opt.Ignore())
            .ForMember(dest => dest.AnotacoesClinicas, opt => opt.Ignore())
            .ForMember(dest => dest.MotivoCancelamento, opt => opt.Ignore())
            .ForMember(dest => dest.DataPagamento, opt => opt.Ignore())
            .ForMember(dest => dest.FormaPagamento, opt => opt.Ignore())
            .ForMember(dest => dest.DataUltimaAtualizacao, opt => opt.Ignore())
            .ForMember(dest => dest.SessaoOriginalId, opt => opt.Ignore())
            .ForMember(dest => dest.SessaoOriginal, opt => opt.Ignore())
            .ForMember(dest => dest.SessoesReagendadas, opt => opt.Ignore());

        // Mapeamento de AtualizarSessaoDto para Sessao
        CreateMap<AtualizarSessaoDto, Sessao>()
            .ForMember(dest => dest.DataUltimaAtualizacao, opt => opt.MapFrom(src => DateTime.Now))
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.ClienteId, opt => opt.Ignore())
            .ForMember(dest => dest.Cliente, opt => opt.Ignore())
            .ForMember(dest => dest.DataHoraRealizada, opt => opt.Ignore())
            .ForMember(dest => dest.DuracaoRealMinutos, opt => opt.Ignore())
            .ForMember(dest => dest.Pago, opt => opt.Ignore())
            .ForMember(dest => dest.DataPagamento, opt => opt.Ignore())
            .ForMember(dest => dest.FormaPagamento, opt => opt.Ignore())
            .ForMember(dest => dest.DataCriacao, opt => opt.Ignore())
            .ForMember(dest => dest.Ativo, opt => opt.Ignore())
            .ForMember(dest => dest.SessaoOriginalId, opt => opt.Ignore())
            .ForMember(dest => dest.SessaoOriginal, opt => opt.Ignore())
            .ForMember(dest => dest.SessoesReagendadas, opt => opt.Ignore());
    }
}
