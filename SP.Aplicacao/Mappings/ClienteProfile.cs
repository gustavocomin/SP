using AutoMapper;
using SP.Aplicacao.DTOs.Clientes;
using SP.Dominio.Clientes;

namespace SP.Aplicacao.Mappings
{
    public class ClienteProfile : Profile
    {
        public ClienteProfile()
        {
            // Cliente -> ClienteDto
            CreateMap<Cliente, ClienteDto>();

            // Cliente -> ClienteResumoDto
            CreateMap<Cliente, ClienteResumoDto>();

            // CriarClienteDto -> Cliente
            CreateMap<CriarClienteDto, Cliente>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.StatusFinanceiro, opt => opt.MapFrom(src => SP.Dominio.Enums.StatusFinanceiro.EmDia))
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore()) // Será definido pelo banco
                .ForMember(dest => dest.DataUltimaAtualizacao, opt => opt.Ignore())
                .ForMember(dest => dest.Ativo, opt => opt.MapFrom(src => true));

            // AtualizarClienteDto -> Cliente
            CreateMap<AtualizarClienteDto, Cliente>()
                .ForMember(dest => dest.DataCadastro, opt => opt.Ignore())
                .ForMember(dest => dest.DataUltimaAtualizacao, opt => opt.Ignore()) // Será definido pelo repository
                .ForMember(dest => dest.Ativo, opt => opt.Ignore()); // Não pode ser alterado via DTO de atualização
        }
    }
}
