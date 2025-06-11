package br.com.poupaAI.financias;

import org.springframework.boot.SpringApplication;
import org.springframework.boot.autoconfigure.SpringBootApplication;
import org.springframework.cloud.openfeign.EnableFeignClients;

@SpringBootApplication
@EnableFeignClients
public class FinanciasApplication {

	public static void main(String[] args) {
		SpringApplication.run(FinanciasApplication.class, args);
	}

}
