package br.com.poupaAI.usuarios.infrastructure.exceptions;

public class ResourceNotFoundException extends RuntimeException {

    public ResourceNotFoundException(String mensagem) {
      super(mensagem);
    }

    public ResourceNotFoundException(String mensagem, Throwable throwable){
      super(mensagem);
    }
}
