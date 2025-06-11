package br.com.poupaAI.financias.business.mapper;

import br.com.poupaAI.financias.business.dto.ObjetivoDTO;
import br.com.poupaAI.financias.infrastructure.entity.ObjetivoEntity;
import org.mapstruct.Mapper;
import org.mapstruct.Mapping;

@Mapper(componentModel = "spring")
public interface ObjetivoConverter {

    @Mapping(source = "id", target = "id")
    @Mapping(source = "dataAlvo", target = "dataAlvo")
    @Mapping(source = "dataCriacao", target = "dataCriacao")
    ObjetivoEntity paraObjetivoEntity(ObjetivoDTO dto);

    ObjetivoDTO paraObjetivoDTO(ObjetivoEntity entity);
}
