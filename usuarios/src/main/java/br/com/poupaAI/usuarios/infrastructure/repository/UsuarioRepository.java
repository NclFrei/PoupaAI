package br.com.poupaAI.usuarios.infrastructure.repository;


import br.com.poupaAI.usuarios.infrastructure.entity.Usuario;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

import java.util.Optional;

@Repository
public interface UsuarioRepository extends JpaRepository<Usuario, Long> {

    boolean existsByEmail(String email);

    Optional<Usuario> findByEmail(String email);
    Optional<Usuario> deleteByEmail(String email);
}
