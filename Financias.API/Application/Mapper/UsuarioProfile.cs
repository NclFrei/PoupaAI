using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;

namespace Financias.API.Application.Mapper;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        CreateMap<UsuarioRequest, Usuario>()
             .ForMember(dest => dest.IdExterno, opt => opt.MapFrom(src => src.Id))
             .ForMember(dest => dest.Id, opt => opt.Ignore());
        
        CreateMap<Usuario, UsuarioResponse>();
    }
}
