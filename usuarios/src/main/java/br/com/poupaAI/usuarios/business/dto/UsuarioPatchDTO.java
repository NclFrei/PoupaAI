package br.com.poupaAI.usuarios.business.dto;


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
