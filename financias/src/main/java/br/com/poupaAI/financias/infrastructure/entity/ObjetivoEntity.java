package br.com.poupaAI.financias.infrastructure.entity;

import jakarta.persistence.*;
import lombok.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

@Entity
@Table(name = "objetivos")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class ObjetivoEntity {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "nome", length = 100)
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
    private List<TransacaoEntity> transacoes;

    private String emailUsuario;


}
