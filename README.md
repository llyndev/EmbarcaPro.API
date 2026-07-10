# EmbarcaPro.API

API em desenvolvimento para o sistema de transporte, com foco no gerenciamento de usuarios, motoristas, caminhoes, carretas, locais e fretes.

## Tecnologias

- .NET 8
- ASP.NET Core Web API
- Entity Framework Core
- SQL Server / LocalDB
- JWT para autenticacao
- Swagger para testar a API em desenvolvimento

## Recursos principais

- Cadastro e login de usuarios
- Autenticacao com token JWT
- Cadastro de motoristas
- Cadastro e listagem de caminhoes
- Cadastro de carretas
- Cadastro de locais
- Cadastro de fretes

## Endpoints iniciais

Alguns endpoints disponiveis no momento:

- `POST /api/auth/register`
- `POST /api/auth/login`
- `GET /api/trucks`
- `POST /api/trucks`
- `POST /api/drivers`
- `POST /api/freights`

Os endpoints protegidos precisam receber o token JWT no header:

```text
Authorization: Bearer seu-token
```

## Estrutura do projeto

```text
Controller/      Controllers da API
Data/            Contexto do banco e mapeamentos
Dtos/            Objetos de entrada e saida
Models/          Entidades do sistema
Services/        Regras de negocio
Migrations/      Migrations do Entity Framework
```

## Status

Projeto em desenvolvimento.
