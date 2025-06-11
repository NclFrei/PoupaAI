package br.com.poupaAI.financias.business.service;


import br.com.poupaAI.financias.business.dto.ObjetivoDTO;
import br.com.poupaAI.financias.business.mapper.ObjetivoConverter;
import br.com.poupaAI.financias.infrastructure.entity.ObjetivoEntity;
import br.com.poupaAI.financias.infrastructure.repository.ObjetivosRepository;
import br.com.poupaAI.financias.infrastructure.security.JwtUtil;
import lombok.RequiredArgsConstructor;
import org.springframework.stereotype.Service;

import java.math.BigDecimal;
import java.time.LocalDateTime;

@Service
@RequiredArgsConstructor
public class ObjetivosService {

    private final JwtUtil jwtUtil;
    private final ObjetivoConverter objetivoConverter;
    private final ObjetivosRepository objetivosRepository;

    public ObjetivoDTO criarObjetivo(String token, ObjetivoDTO dto) {
        // A exceção provavelmente está acontecendo em uma das linhas abaixo
        String email = jwtUtil.extractUsername(token.substring(7));

        dto.setDataCriacao(LocalDateTime.now());
        dto.setEmailUsuario(email);
        dto.setValorAtual(BigDecimal.ZERO);

        ObjetivoEntity entity = objetivoConverter.paraObjetivoEntity(dto);
        ObjetivoEntity salvo = objetivosRepository.save(entity);
        return objetivoConverter.paraObjetivoDTO(salvo);
    }


}
