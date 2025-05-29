package br.com.nicollasFrei.poupaAI.business.converter;

import br.com.nicollasFrei.poupaAI.business.dto.UsuarioDTO;
import br.com.nicollasFrei.poupaAI.infrastructure.entity.Usuario;

public class UsuarioConverter {

    public Usuario paraUsuario(UsuarioDTO usuarioDTO){
        return Usuario.builder()
                .nome(usuarioDTO.getNome())
                .senha(usuarioDTO.getSenha())
                .email(usuarioDTO.getEmail())
                .build();

    }

    public UsuarioDTO paraUsuarioDTO(Usuario usuarioDTO){
        return UsuarioDTO.builder()
                .nome(usuarioDTO.getNome())
                .senha(usuarioDTO.getSenha())
                .email(usuarioDTO.getEmail())
                .build();

    }

}
