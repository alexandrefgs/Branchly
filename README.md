# Branchly

![.NET 9](https://img.shields.io/badge/.NET-9-blue)
![License](https://img.shields.io/github/license/alexandrefgs/Branchly)
![Docker Compose](https://img.shields.io/badge/docker--compose-ready-blue)
![Contributions welcome](https://img.shields.io/badge/contributions-welcome-brightgreen)
![GitHub last commit](https://img.shields.io/github/last-commit/alexandrefgs/Branchly)
![GitHub issues](https://img.shields.io/github/issues/alexandrefgs/Branchly)
![Swagger UI Screenshot](docs/images/swagger.png)

**Branchly** is a modern open-source **Link-in-bio platform** built with **.NET 9**.  
It allows users to create a customizable profile page with multiple links, shortlinks, and analytics.  
The system is built as a set of independent APIs (Auth, Users, Links) and a WebApp (dashboard + public pages), following **Clean Architecture** and best practices for production-ready systems.

---

## ✨ Features

- 🌐 **Customizable profile pages** — themes, avatars, bios, and personalized handles (`branchly.me/@username`)
- 🔗 **Smart link management** — add, remove, reorder, and generate shortlinks (`branchly.me/r/{code}`)
- 📊 **Analytics dashboard** — clicks, referrers, devices, countries, time ranges (7/30 days)
- 🔐 **Authentication API** — secure login, JWT + Refresh Tokens, email verification, password reset
- 🏗 **Clean Architecture & Dependency Injection** from day one
- 📖 **Swagger/OpenAPI** documentation for all APIs
- 🧪 **Unit & Integration Tests** with EF Core in-memory and testcontainers
- 🐳 **Docker Compose** ready — runs all services + SQL Server + Redis

## 📂 Project Structure

- **Auth API** → handles authentication, registration, login, refresh, logout  
- **Users API** → profile management (handle, bio, avatar, themes, settings)  
- **Links API** → link CRUD, shortlinks, redirect service, analytics pipeline  
- **WebApp** → user dashboard + public bio page (`branchly.me/@username`)  

## 🛠 Tech Stack

- **.NET 9** (C#, ASP.NET Core)
- **Entity Framework Core** + **SQL Server**
- **Identity Core** for user management
- **JWT** (with refresh tokens)
- **Redis** (cache & rate limiting)
- **Swagger / OpenAPI**
- **Docker Compose** (multi-service setup)
- **xUnit** for testing

---

## 🚀 Getting Started

### Prerequisites
- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)  
- [Docker](https://www.docker.com/) (for SQL Server + Redis)  

### Running with Docker
```bash
docker compose up --build
```

### Services will be available at:

- Auth API → http://localhost:5001/swagger
- Users API → http://localhost:5002/swagger
- Links API → http://localhost:5003/swagger
- WebApp → http://localhost:5000

### 🔐 Authentication Flow

- User registers via Auth API → /v1/auth/register
- Logs in with credentials → receives Access Token + Refresh Token
- Access protected endpoints with Authorization: Bearer <access_token>
- Refresh token flow rotates tokens securely
- Logout revokes refresh tokens

### 📊 Analytics Flow

- Every redirect /r/{code} generates a click event
- Events are stored and aggregated daily
- Dashboard shows totals by link, referrer, device, country, date range

## 🚀 Roadmap

- [x] Authentication API (JWT, RefreshTokens, Swagger)
- [ ] Email verification
- [ ] Forgot password
- [ ] User Profiles (handle, themes, avatar)
- [ ] Links CRUD + shortlinks
- [ ] Analytics dashboard
- [ ] WebApp Dashboard (profile editor + analytics)

### 🤝 Contributing

Contributions are welcome!
Please open an issue or pull request if you’d like to help improve Branchly.

### 📜 License

This project is licensed under the MIT License.
Feel free to use it for personal or commercial projects.

### 🌟 Acknowledgements
**- .NET**
**- Entity Framework Core**
**- Swagger / Swashbuckle**
**- Redis**
**- Docker**
