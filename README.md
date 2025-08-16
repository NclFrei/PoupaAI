# PoupaAI

## Visão Geral

**PoupaAI** é uma plataforma de microserviços para gerenciamento financeiro e interação com chatbots, desenvolvida em **.NET 8**, com arquitetura **RESTful** e integração via **RabbitMQ**.

O projeto é composto por três microserviços principais:

1. **UsuariosService**: gerencia usuários do sistema.
2. **FinanciasService**: gerencia transações financeiras e categorias.
3. **ChatbotService**: permite interação via chatbot e processa perguntas dos usuários.

Cada microserviço utiliza **SQL Server** como banco de dados e se comunica de forma assíncrona via **RabbitMQ**.

---

## Arquitetura

<img width="1181" height="1062" alt="Diagrama sem nome drawio (3)" src="https://github.com/user-attachments/assets/ab54da94-601f-4502-a010-fffd5a477081" />


- Cada serviço possui seu próprio **banco SQL Server**.
- Comunicação **assíncrona** via RabbitMQ.
- APIs **REST** expostas em portas diferentes:
  - `UsuariosService`: 5001
  - `FinanciasService`: 5002
  - `ChatbotService`: 5003

---

## Pré-requisitos

Antes de rodar o projeto, instale:

- Docker

---

## Rodando com Docker

### 1. Criar a rede Docker

```bash
docker network create poupaAI
```

### 2. Rodar os bancos de dados

```bash
docker run -d --name sql_financias --network poupaAI -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1433:1433 mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name sql_usuarios --network poupaAI -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1434:1433 mcr.microsoft.com/mssql/server:2022-latest

docker run -d --name sql_chatbot --network poupaAI -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=Your_password123" -p 1435:1433 mcr.microsoft.com/mssql/server:2022-latest
```

### 3. Rodar RabbitMQ

```bash
docker run -d --name rabbitmq --network poupaAI -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

A interface web do RabbitMQ estará disponível em: `http://localhost:15672`\
**Login padrão:** `guest` / `guest`

### 4. Build dos microserviços

> **Nota:** é necessário estar dentro da pasta de cada microserviço ao executar o build.

```bash
docker build -t usuariosservice:1.1 ./UsuariosService
docker build -t financiasservice:1.1 ./FinanciasService
docker build -t chatbotservice:1.1 ./ChatbotService
```

### 5. Rodar APIs

```bash
docker run -d --name usuarios_api --network poupaAI -p 5001:8080 \
-e ConnectionStrings__MySqlConnection="Server=sql_usuarios,1433;Database=UsuariosDB;User Id=sa;Password=Your_password123;TrustServerCertificate=True;" \
-e RabbitMqHost="rabbitmq" -e RabbitMqPort="5672" usuariosservice:1.1

docker run -d --name financias_api --network poupaAI -p 5002:8080 \
-e ConnectionStrings__MySqlConnection="Server=sql_financias,1433;Database=FinanciasDB;User Id=sa;Password=Your_password123;TrustServerCertificate=True;" \
-e RabbitMqHost="rabbitmq" -e RabbitMqPort="5672" financiasservice:1.1

docker run -d --name chatbot_api --network poupaAI -p 5003:8080 \
-e ConnectionStrings__MySqlConnection="Server=sql_chatbot,1433;Database=ChatbotDB;User Id=sa;Password=Your_password123;TrustServerCertificate=True;" \
-e RabbitMqHost="rabbitmq" -e RabbitMqPort="5672" chatbotservice:1.1
```

---
## Configuração do `appsettings`

Cada microserviço possui um arquivo `appsettings.json` semelhante ao exemplo abaixo:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "RabbitMqHost": "rabbitmq",
  "RabbitMqPort": "5672",
  "ConnectionStrings": {
    "MySqlConnection": "Server=localhost,1433;Database=FinanciasDB;User Id=sa;Password=Your_password123;"
  },
  "JWTSettings": {
    "SecretKey": ""
  }
}
```

### Observações importantes:

- O **JWTSettings.SecretKey** deve ser **igual em todos os microserviços** para que a autenticação funcione corretamente.
- No **ChatbotService**, você deve adicionar a chave da API do Google AI:

```json
"GoogleAiApiKey": ""
```

- **JWT SecretKey** e **GoogleAiApiKey** **não devem ser versionadas no repositório**. Quem estiver rodando os serviços deve configurar esses valores localmente, por questões de segurança.

- Ajuste o `Server` e `Database` na seção `ConnectionStrings` conforme o microserviço:

  - `UsuariosService` → `UsuariosDB`
  - `FinanciasService` → `FinanciasDB`
  - `ChatbotService` → `ChatbotDB`

---

## Endpoints de exemplo

### UsuariosService

- **Criar usuário:** `POST /api/usuarios`

  ```json
  {
    "Nome": "Nicollas Frei",
    "Email": "nicollas@example.com",
    "Senha": "123456",
    "DataNascimento": "1995-08-16T00:00:00"
  }
  ```

- **Listar usuários:** `GET /api/usuarios/{id}`

### FinanciasService

- **Criar transação:** `POST /api/transacoes`

  ```json
  {
    "Nome": "Pagamento Conta Luz",
    "Descricao": "Agosto 2025",
    "DataTransacao": "2025-08-16T00:00:00",
    "Valor": 120.50,
    "CategoriaId": 1,
    "UsuarioId": 1,
    "Tipo": "Despesa"
  }
  ```

- **Listar transações:** `GET /api/transacoes/usuario/{idusuario}

### ChatbotService

- **Perguntar ao chatbot:** `POST /api/chatbot/perguntar`
  ```json
  {
    "usuarioId": 1,
    "pergunta": "Qual foi minha última transação?"
  }
  ```

---

## Observações Gerais

- Todos os microserviços são independentes, mas dependem do **RabbitMQ** para eventos.
- Certifique-se de que **as portas não estão em uso** antes de rodar os containers.
- Pode acessar cada banco via SQL Server Management Studio (SSMS) nas portas correspondentes:
  - `sql_financias` → 1433
  - `sql_usuarios` → 1434
  - `sql_chatbot` → 1435



