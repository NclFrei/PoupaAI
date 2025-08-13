using AutoMapper;
using Financias.API.Domain.DTOs.Request;
using Financias.API.Domain.DTOs.Response;
using Financias.API.Domain.Models;

namespace Financias.API.Application.Mapper;

public class CategoriaProfile : Profile
{
    public CategoriaProfile()
    {
        CreateMap<CategoriaRequest, Categoria>();
        CreateMap<Categoria, CategoriaResponse>();
    }
}