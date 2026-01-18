# Branchly

![.NET 8](https://img.shields.io/badge/.NET-8-blue)
![License](https://img.shields.io/github/license/alexandrefgs/Branchly)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen)
![GitHub last commit](https://img.shields.io/github/last-commit/alexandrefgs/Branchly)
![GitHub issues](https://img.shields.io/github/issues/alexandrefgs/Branchly)

**Branchly** é uma plataforma open-source de **Link-in-bio**, desenvolvida com **ASP.NET Core MVC**, focada em **boas práticas de arquitetura**, **Domain-Driven Design (DDD)** e **Clean Architecture**.

O projeto está sendo construído como um **monólito modular**, simulando um sistema real de produção, com foco em organização, manutenibilidade e evolução contínua.

---

## ✨ Funcionalidades (Atuais e Planejadas)

* 🌐 **Páginas de perfil personalizáveis** — bio, avatar e identificador público (`branchly.me/@usuario`)
* 🔗 **Gerenciamento de links** — adicionar, remover e reordenar links
* 📊 **Analytics básicos** — cliques por link (versão inicial)
* 🔐 **Autenticação e autorização** — login, cadastro e recuperação de senha
* 🏗 **Clean Architecture** com separação clara de responsabilidades
* 🧠 **Domain-Driven Design (DDD)** aplicado desde o início
* 🖥 **ASP.NET Core MVC com Razor Views**
* 🔄 **Preparado para futura migração para SPA (Angular)**

---

## 📂 Estrutura do Projeto

A solution segue os princípios da **Clean Architecture**:

```
Branchly
│
├── Branchly.Domain          → Entidades, Value Objects e regras de negócio
├── Branchly.Application     → Casos de uso, DTOs e contratos
├── Branchly.Infrastructure  → Persistência, EF Core e integrações externas
└── Branchly.Web             → ASP.NET Core MVC (Controllers, Views e UI)
```

### Fluxo de Dependências

```
Web
 ↓
Application
 ↓
Domain
 ↑
Infrastructure
```

---

## 🛠 Stack Tecnológica

* **.NET 8**
* **ASP.NET Core MVC**
* **Entity Framework Core** (planejado)
* **SQL Server** (planejado)
* **Razor Views**
* **Clean Architecture**
* **Domain-Driven Design (DDD)**
* **Princípios SOLID**

---

## 🚀 Como Executar o Projeto

### Pré-requisitos

* [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
* Visual Studio 2022 ou superior / VS Code

### Executando a aplicação

```bash
dotnet restore
dotnet build
dotnet run --project Branchly.Web
```

A aplicação estará disponível em:

```
https://localhost:5001
```

---

## 🚧 Status do Projeto

Este projeto está **em desenvolvimento ativo** e está sendo construído de forma incremental, com foco em:

* Qualidade de código
* Arquitetura limpa
* Boas práticas do mercado
* Evolução gradual e consciente

---

## 🛣 Roadmap

* [x] Criação da solution
* [x] Estrutura base da Clean Architecture
* [ ] Modelagem do domínio
* [ ] Autenticação e autorização
* [ ] Perfis de usuário
* [ ] Gerenciamento de links
* [ ] Analytics básicos
* [ ] Evolução da UI
* [ ] Migração para Angular (futuro)

---

## 🤝 Contribuindo

Contribuições são bem-vindas!
Sinta-se à vontade para abrir **issues** ou **pull requests** com sugestões de melhorias.

---

## 📜 Licença

Este projeto está licenciado sob a **MIT License**.

---

### 🌟 Agradecimentos

* **ASP.NET Core**
* **Entity Framework Core**
* **Clean Architecture**
* **Domain-Driven Design (DDD)**
