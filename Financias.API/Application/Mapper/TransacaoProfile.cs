using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;

namespace Financias.API.Application.Mapper;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<TransacaoRequest, Transacao>();
        CreateMap<Transacao, TransacaoResponse>();
        
        CreateMap<AtualizaTransacaoRequest, Transacao>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    !(srcMember is string str && string.IsNullOrWhiteSpace(str))
                ));
        
        CreateMap<Transacao, TransacaoResponseRabbitMq>()
            .ForMember(dest => dest.Categoria, opt => opt.MapFrom(src => src.Categoria.Nome))
            .ForMember(dest => dest.Tipo, opt => opt.MapFrom(src => src.Tipo.ToString()));
    }
}
