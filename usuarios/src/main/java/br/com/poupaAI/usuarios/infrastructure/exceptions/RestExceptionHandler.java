package br.com.poupaAI.usuarios.infrastructure.exceptions;

import org.springframework.http.HttpStatus;
import org.springframework.http.ResponseEntity;
import org.springframework.security.authentication.BadCredentialsException;
import org.springframework.web.bind.MethodArgumentNotValidException;
import org.springframework.web.bind.annotation.ControllerAdvice;
import org.springframework.web.bind.annotation.ExceptionHandler;
import org.springframework.web.servlet.mvc.method.annotation.ResponseEntityExceptionHandler;

import java.nio.file.AccessDeniedException;
import java.util.stream.Collectors;

@ControllerAdvice
public class RestExceptionHandler {

    // 1) Quando for lançada ConflictException (e-mail duplicado, por exemplo)
    @ExceptionHandler(ConflictException.class)
    public ResponseEntity<RestErrorMessage> handleConflict(ConflictException ex) {
        RestErrorMessage body = new RestErrorMessage(HttpStatus.CONFLICT, ex.getMessage());
        return ResponseEntity
                .status(HttpStatus.CONFLICT)
                .body(body);
    }

    // 2) Quando for lançada ResourceNotFoundException (recurso não encontrado)
    @ExceptionHandler(ResourceNotFoundException.class)
    public ResponseEntity<RestErrorMessage> handleNotFound(ResourceNotFoundException ex) {
        RestErrorMessage body = new RestErrorMessage(HttpStatus.NOT_FOUND, ex.getMessage());
        return ResponseEntity
                .status(HttpStatus.NOT_FOUND)
                .body(body);
    }

    // 3) Quando falhar a validação de campos (@Valid / @NotBlank / @Email etc.)
    @ExceptionHandler(MethodArgumentNotValidException.class)
    public ResponseEntity<RestErrorMessage> handleValidation(MethodArgumentNotValidException ex) {
        // Extrai todas as mensagens de campo que falharam na validação
        String mensagens = ex.getBindingResult()
                .getFieldErrors()
                .stream()
                .map(error -> error.getField() + ": " + error.getDefaultMessage())
                .collect(Collectors.joining("; "));

        RestErrorMessage body = new RestErrorMessage(HttpStatus.BAD_REQUEST, mensagens);
        return ResponseEntity
                .status(HttpStatus.BAD_REQUEST)
                .body(body);
    }

    // 4) (Opcional) Qualquer outra exceção não prevista cai aqui
    @ExceptionHandler(Exception.class)
    public ResponseEntity<RestErrorMessage> handleGeneric(Exception ex) {
        // Evite expor stacktrace em produção; retorne mensagem genérica ou ex.getMessage()
        RestErrorMessage body = new RestErrorMessage(HttpStatus.INTERNAL_SERVER_ERROR, "Erro interno no servidor");
        return ResponseEntity
                .status(HttpStatus.INTERNAL_SERVER_ERROR)
                .body(body);
    }

    // 5) Quando o Login estiver incorreto
    @ExceptionHandler(BadCredentialsException.class)
    public ResponseEntity<RestErrorMessage> handleBadCredentials(BadCredentialsException ex) {
        RestErrorMessage body = new RestErrorMessage(
                HttpStatus.UNAUTHORIZED,
                "E-mail ou senha inválidos."
        );
        return ResponseEntity.status(HttpStatus.UNAUTHORIZED).body(body);
    }

}