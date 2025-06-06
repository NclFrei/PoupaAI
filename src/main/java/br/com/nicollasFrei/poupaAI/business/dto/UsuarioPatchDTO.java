package br.com.nicollasFrei.poupaAI.business.dto;

import lombok.*;

@Getter
@Setter
@AllArgsConstructor
@NoArgsConstructor
@Builder
public class UsuarioPatchDTO {
    private String nome;
    private String email;
    private String senha;
}
