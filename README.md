# SafeScribe API - CP5

Este projeto é uma implementação de uma API RESTful para a plataforma SafeScribe, desenvolvida como parte do Checkpoint 5. 

O objetivo é fornecer um backend seguro para gestão de notas e documentos sensíveis, com um sistema robusto de autenticação e autorização utilizando JSON Web Tokens (JWT). 

## Tecnologias Utilizadas

* .NET 8:** Framework principal para a construção da API.
* ASP.NET Core Web API:** Tipo de projeto utilizado. 
* JWT (JSON Web Tokens):** Para autenticação e autorização segura. 
* Entity Framework Core In-Memory:** Para persistência de dados de forma simplificada.

* ## Pré-requisitos e Instalação

Antes de começar, você precisa ter o SDK do .NET 8 (ou superior) instalado em sua máquina.

1.  **Clone o repositório:**
    ```bash
    git clone https://github.com/Dametto98/CP5_ASP.NET
    cd SafeScribe.API
    ```

2.  **Instale os pacotes necessários:**
    Execute os seguintes comandos no terminal, dentro da pasta do projeto, para instalar as dependências.
    ```bash
    dotnet add package Microsoft.AspNetCore.Authentication.JwtBearer
    dotnet add package Microsoft.EntityFrameworkCore.InMemory
    dotnet add package BCrypt.Net-Next
    ```

## Executando o Projeto

Após a instalação dos pacotes, você pode iniciar a API com um único comando.

1.  **Inicie a aplicação:**
    ```bash
    dotnet run
    ```

2.  **Acesse a API:**
    Após a execução do comando, a API estará rodando e pronta para receber requisições. Por padrão, você poderá acessá-la através das seguintes URLs (verifique seu terminal para as portas exatas):
    * `https://localhost:7xxx`
    * `http://localhost:5xxx`

    Você pode usar uma ferramenta como o Postman ou o Swagger (se configurado) para interagir com os endpoints de autenticação (`/api/v1/auth`) e de notas (`/api/v1/notas`).

## Membros do Grupo

* **Nome:** Caike Dametto - **RM:** 558614
* **Nome:** Guilherme janunzzi - **RM:** 558461
