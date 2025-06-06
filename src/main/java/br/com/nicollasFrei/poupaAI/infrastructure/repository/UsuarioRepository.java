package br.com.nicollasFrei.poupaAI.infrastructure.repository;

import br.com.nicollasFrei.poupaAI.infrastructure.entity.Usuario;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UsuarioRepository extends JpaRepository<Usuario, Long> {

    boolean existsByEmail(String email);

    Optional<Usuario> findByEmail(String email);
    void deleteByEmail(String email);
}
