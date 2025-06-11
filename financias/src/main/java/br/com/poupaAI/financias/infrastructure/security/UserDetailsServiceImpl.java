package br.com.poupaAI.financias.infrastructure.security;

import br.com.poupaAI.financias.business.dto.UsuarioDTO;
import br.com.poupaAI.financias.infrastructure.client.UsuarioClient;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.userdetails.User;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.security.core.userdetails.UserDetailsService;
import org.springframework.security.core.userdetails.UsernameNotFoundException;
import org.springframework.stereotype.Service;

@Service
public class UserDetailsServiceImpl  {

    // Repositório para acessar dados de usuário no banco de dados
    @Autowired
    private UsuarioClient client;


    public UserDetails carregaDadosUsuario(String email, String token){

        UsuarioDTO usuarioDTO = client.buscarUsuarioPorEmai(email, token);
        return User
                .withUsername(usuarioDTO.getEmail()) // Define o nome de usuário como o e-mail
                .password(usuarioDTO.getSenha()) // Define a senha do usuário
                .build();

    }
}
