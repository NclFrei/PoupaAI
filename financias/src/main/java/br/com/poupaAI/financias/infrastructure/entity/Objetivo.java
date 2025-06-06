package br.com.poupaAI.financias.infrastructure.entity;

import jakarta.persistence.*;
import jakarta.validation.constraints.Email;
import lombok.*;
import org.springframework.security.core.GrantedAuthority;
import org.springframework.security.core.userdetails.UserDetails;
import jakarta.validation.constraints.NotBlank;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.Collection;
import java.util.List;

@Entity
@Table(name = "objetivos")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Objetivo {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "descricao", length = 150)
    private String nome;

    @Column(name = "descricao", length = 150)
    private String descricao;

    @Column(name = "valor_meta", precision = 15, scale = 2)
    private BigDecimal valorMeta;

    @Column(name = "valor_atual", precision = 15, scale = 2)
    private BigDecimal valorAtual;


    @Column(name = "data_criacao")
    private LocalDateTime dataCriacao;


    @Column(name = "data_alvo")
    private LocalDateTime dataAlvo;

    @OneToMany(mappedBy = "objetivo", cascade = CascadeType.ALL, orphanRemoval = true)
    private List<Transacao> transacoes;


}
