using AutoMapper;
using Chatbot.API.Domain.DTOs.Request;
using Chatbot.API.Domain.Models;

namespace Chatbot.API.Application.Mapper;

public class TransacaoProfile : Profile
{
    public TransacaoProfile()
    {
        CreateMap<TransacaoRequest, Transacao>()
            .ForMember(dest => dest.IdExterno, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Id, opt => opt.Ignore());
    }
}