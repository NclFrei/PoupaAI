using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.Models;

namespace Financias.API.Application.Mapper;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<Transacoes, TransacaoRequest>();
    }
}
