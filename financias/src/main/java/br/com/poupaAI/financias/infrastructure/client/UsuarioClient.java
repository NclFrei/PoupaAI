package br.com.poupaAI.financias.infrastructure.client;

import br.com.poupaAI.financias.business.dto.UsuarioDTO;
import org.springframework.cloud.openfeign.FeignClient;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.GetMapping;
import org.springframework.web.bind.annotation.RequestHeader;
import org.springframework.web.bind.annotation.RequestParam;

@FeignClient(name = "usuario", url = "${usuario.url}")
public interface UsuarioClient {

    @GetMapping("/usuario")
    UsuarioDTO buscarUsuarioPorEmai(@RequestParam("email") String email,
                                    @RequestHeader("Authorization") String token);

}