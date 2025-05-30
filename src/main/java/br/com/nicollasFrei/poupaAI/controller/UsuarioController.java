package br.com.nicollasFrei.poupaAI.controller;


import br.com.nicollasFrei.poupaAI.business.dto.UsuarioDTO;
import br.com.nicollasFrei.poupaAI.business.service.UsuarioService;
import br.com.nicollasFrei.poupaAI.infrastructure.security.JwtUtil;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestBody;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/usuario")
@RequiredArgsConstructor
public class UsuarioController {

    private final UsuarioService usuarioService;
    private final AuthenticationManager authenticationManager;
    private final JwtUtil jwtUtil;

    @PostMapping
    public ResponseEntity<UsuarioDTO> saveUsuario(@RequestBody @Valid UsuarioDTO usuarioDTO){
        UsuarioDTO savedUser = usuarioService.saveUsuario(usuarioDTO);
        return ResponseEntity.status(HttpStatus.CREATED).body(savedUser);
    }



}
