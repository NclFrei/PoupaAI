package br.com.poupaAI.financias.infrastructure.repository;


import br.com.poupaAI.financias.infrastructure.entity.ObjetivoEntity;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;


@Repository
public interface ObjetivosRepository extends JpaRepository<ObjetivoEntity, Long> {
}
