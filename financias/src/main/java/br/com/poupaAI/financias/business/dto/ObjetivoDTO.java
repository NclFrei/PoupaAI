package br.com.poupaAI.financias.business.dto;

import br.com.poupaAI.financias.infrastructure.entity.TransacaoEntity;
import com.fasterxml.jackson.annotation.JsonFormat;
import jakarta.validation.constraints.*;
import lombok.*;

import java.math.BigDecimal;
import java.time.LocalDateTime;
import java.util.List;

@Setter
@Getter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class ObjetivoDTO {

    private Long id;
    @NotBlank(message = "O nome do objetivo é obrigatório")
    @Size(max = 100, message = "O nome pode ter no máximo 100 caracteres")
    private String nome;

    @NotBlank(message = "A descrição é obrigatória")
    @Size(max = 255, message = "A descrição pode ter no máximo 255 caracteres")
    private String descricao;

    @NotNull(message = "O valor da meta é obrigatório")
    @Positive(message = "O valor da meta deve ser maior que zero")
    private BigDecimal valorMeta;

    @NotNull(message = "O valor atual é obrigatório")
    private BigDecimal valorAtual;

    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "dd-MM-yyyy HH:mm:ss")
    private LocalDateTime dataCriacao;

    @JsonFormat(shape = JsonFormat.Shape.STRING, pattern = "dd-MM-yyyy HH:mm:ss")
    private LocalDateTime dataAlvo;

    private List<TransacaoEntity> transacoes;

    @NotBlank(message = "O e-mail do usuário é obrigatório")
    @Email(message = "Formato de e-mail inválido")
    private String emailUsuario;

}
