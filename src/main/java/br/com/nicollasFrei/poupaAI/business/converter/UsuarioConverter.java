package br.com.nicollasFrei.poupaAI.business.converter;

import br.com.nicollasFrei.poupaAI.business.dto.UsuarioDTO;
import br.com.nicollasFrei.poupaAI.business.dto.UsuarioPatchDTO;
import br.com.nicollasFrei.poupaAI.infrastructure.entity.Usuario;
import org.springframework.stereotype.Component;

@Component
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

    public Usuario updateUsuario(UsuarioPatchDTO usuarioDTO, Usuario entity) {
        return Usuario.builder()
                .nome(usuarioDTO.getNome() != null ? usuarioDTO.getNome() : entity.getNome())
                .id(entity.getId())
                .senha(usuarioDTO.getSenha() != null ? usuarioDTO.getSenha() : entity.getSenha())
                .email(usuarioDTO.getEmail() != null ? usuarioDTO.getEmail() : entity.getEmail())
                .build();
    }

}
