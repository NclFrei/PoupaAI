package br.com.nicollasFrei.poupaAI.controller;


import br.com.nicollasFrei.poupaAI.business.dto.UsuarioDTO;
import br.com.nicollasFrei.poupaAI.business.service.UsuarioService;
import br.com.nicollasFrei.poupaAI.infrastructure.security.JwtUtil;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.AuthenticationManager;
import org.springframework.security.authentication.UsernamePasswordAuthenticationToken;
import org.springframework.security.core.Authentication;
import org.springframework.web.bind.annotation.*;

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
        return ResponseEntity.status(201).body(savedUser);
    }

    @PostMapping("/login")
    public String login(@RequestBody UsuarioDTO usuarioDTO) {
        Authentication authentication = authenticationManager.authenticate(
                new UsernamePasswordAuthenticationToken(
                        usuarioDTO.getEmail(),
                        usuarioDTO.getSenha()
                )
        );
        return "Bearer " + jwtUtil.generateToken(authentication.getName());
    }

    @GetMapping
    public ResponseEntity<UsuarioDTO> buscarUsuarioPorEmail(@RequestParam("email") String email) {
        UsuarioDTO dto = usuarioService.buscarUsuarioPorEmail(email);
        return ResponseEntity.ok(dto);
    }



}
