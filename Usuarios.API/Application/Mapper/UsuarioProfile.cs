using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Usuarios.API.Domain.DTOs.Request;
using Usuarios.API.Domain.DTOs.Response;
using Usuarios.API.Domain.Models;


namespace Usuarios.API.Application.Mapper;

public class UsuarioProfile : Profile
{
    public UsuarioProfile()
    {
        // Criação de usuário
        CreateMap<UsuarioCreateRequest, Usuario>();

        // Retorno de usuário
        CreateMap<Usuario, UsuarioResponse>();

        // Atualização parcial (PATCH)
        CreateMap<AtualizarUsuarioRequest, Usuario>()
            .ForAllMembers(opt =>
                opt.Condition((src, dest, srcMember) =>
                    srcMember != null &&
                    !(srcMember is string str && string.IsNullOrWhiteSpace(str))
                ));
    }
}
