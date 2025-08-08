using AutoMapper;
using Financias.API.Dtos.Request;

namespace Financias.API.Application.Mapper;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<TransacaoRequest, TransacaoRequest>();
    }
}
