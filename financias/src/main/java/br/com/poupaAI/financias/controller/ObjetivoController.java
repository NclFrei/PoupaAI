package br.com.poupaAI.financias.controller;


import br.com.poupaAI.financias.business.dto.ObjetivoDTO;
import br.com.poupaAI.financias.business.service.ObjetivosService;
import jakarta.validation.Valid;
import lombok.RequiredArgsConstructor;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.core.annotation.AuthenticationPrincipal;
import org.springframework.security.core.userdetails.UserDetails;
import org.springframework.web.bind.annotation.*;

@RestController
@RequestMapping("/objetivos")
@RequiredArgsConstructor
public class ObjetivoController {
    private final ObjetivosService objetivosService;

    private static final Logger logger = LoggerFactory.getLogger(ObjetivoController.class);

    @PostMapping
    public ResponseEntity<ObjetivoDTO>  gravarObjetivo(@RequestBody ObjetivoDTO dto,
                                                       @RequestHeader("Authorization") String token){
        return ResponseEntity.ok(objetivosService.criarObjetivo(token, dto));
    }


}
