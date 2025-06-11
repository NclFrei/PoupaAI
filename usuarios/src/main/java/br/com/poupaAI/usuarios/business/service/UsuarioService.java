package br.com.poupaAI.usuarios.business.service;

import br.com.poupaAI.usuarios.business.converter.UsuarioConverter;
import br.com.poupaAI.usuarios.business.dto.UsuarioDTO;
import br.com.poupaAI.usuarios.business.dto.UsuarioPatchDTO;
import br.com.poupaAI.usuarios.infrastructure.entity.Usuario;
import br.com.poupaAI.usuarios.infrastructure.exceptions.ConflictException;
import br.com.poupaAI.usuarios.infrastructure.exceptions.ResourceNotFoundException;
import br.com.poupaAI.usuarios.infrastructure.repository.UsuarioRepository;
import br.com.poupaAI.usuarios.infrastructure.security.JwtUtil;
import lombok.RequiredArgsConstructor;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.security.crypto.password.PasswordEncoder;
import org.springframework.stereotype.Service;

@Service
@RequiredArgsConstructor
public class UsuarioService {

    private final UsuarioRepository usuarioRepository;
    private final UsuarioConverter usuarioConverter;
    private final PasswordEncoder passwordEncoder;
    private final JwtUtil jwtUtil;

    public UsuarioDTO saveUsuario(UsuarioDTO usuarioDTO){
        emailExiste(usuarioDTO.getEmail());
        usuarioDTO.setSenha(passwordEncoder.encode(usuarioDTO.getSenha()));
        Usuario usuario = usuarioConverter.paraUsuario(usuarioDTO);
        return usuarioConverter.paraUsuarioDTO(
                usuarioRepository.save(usuario));
    }

    public void emailExiste(String email) {
        try {
            boolean existe = verificaEmailExistente(email);
            if (existe) {
                throw new ConflictException("Email já cadastrado" + email);
            }
        } catch (ConflictException e) {
            throw new ConflictException("Email já cadastrado", e.getCause());
        }
    }

    public boolean verificaEmailExistente(String email) {
        return usuarioRepository.existsByEmail(email);
    }

    public UsuarioDTO buscarUsuarioPorEmail(String email) {
        try{
            return usuarioConverter.paraUsuarioDTO(
                    usuarioRepository.findByEmail(email)
                            .orElseThrow(
                                    () -> new ResourceNotFoundException("Email não encontrado " + email)
                            )
            );
        } catch (ResourceNotFoundException e) {
            throw new ResourceNotFoundException("Email não encotrado " + email);
        }
    }

    public void deletarUsuarioPorEmail(String email) {
        Usuario usuario = usuarioRepository.findByEmail(email)
                .orElseThrow(() ->
                        new ResourceNotFoundException("Email não encontrado: " + email)
                );
        try {
            usuarioRepository.delete(usuario);
        } catch (DataIntegrityViolationException ex) {
            throw ex;
        }
    }

    public UsuarioDTO atualizaDadosUsuario(String token, UsuarioPatchDTO dto) {
        String email = jwtUtil.extractUsername(token.substring(7));

        Usuario usuarioOriginal  = usuarioRepository.findByEmail(email)
                .orElseThrow(() ->
                        new ResourceNotFoundException("Email não encontrado: " + email)
                );

        Usuario usuarioAtualizado  = usuarioConverter.updateUsuario(dto, usuarioOriginal );

        Usuario salvo = usuarioRepository.save(usuarioAtualizado);
        return usuarioConverter.paraUsuarioDTO(salvo);
    }

}
