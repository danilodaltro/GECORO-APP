using AutoMapper;
using GECORO.Application.Dto;
using GECORO.Domain;

namespace GECORO.Application.Helper
{
    public class GecoroProfile: Profile
    {
        public GecoroProfile()
        {
            CreateMap<VendedorDto, Vendedor>().ReverseMap();
            CreateMap<ClienteDto, Cliente>().ReverseMap();
            CreateMap<ContratoDto, Contrato>().ReverseMap();
            CreateMap<ParcelaDto, Parcela>().ReverseMap();
            CreateMap<RegraVendedorDto, RegraVendedor>().ReverseMap();
        }
    }
}