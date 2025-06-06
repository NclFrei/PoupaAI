package br.com.poupaAI.financias.infrastructure.entity;

import br.com.poupaAI.financias.infrastructure.enums.CategoriaTransacao;
import jakarta.persistence.*;
import lombok.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;

@Entity
@Table(name = "transacoes")
@Getter
@Setter
@NoArgsConstructor
@AllArgsConstructor
@Builder
public class Transacao {

    @Id
    @GeneratedValue(strategy = GenerationType.IDENTITY)
    private Long id;

    @Column(name = "valor", precision = 15, scale = 2)
    private BigDecimal valor;

    @Enumerated(EnumType.STRING)
    @Column(name = "tipo", length = 10)
    private CategoriaTransacao tipo;

    @Column(name = "data_transacao")
    private LocalDateTime dataTransacao;

    @Column(name = "descricao", length = 200)
    private String descricao;

    @ManyToOne(fetch = FetchType.LAZY)
    @JoinColumn(name = "objetivo_id", nullable = false)
    private Objetivo objetivo;
}