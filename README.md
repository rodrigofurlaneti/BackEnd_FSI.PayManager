# PayManager – Personal Finance Management System  
# PayManager – Sistema de Gerenciamento Financeiro Pessoal

PayManager is a modern and scalable personal finance management system built with .NET 8, following Clean Architecture and Domain-Driven Design principles.  
O PayManager é um sistema moderno e escalável de gerenciamento financeiro pessoal desenvolvido em .NET 8, seguindo os princípios de Clean Architecture e Domain-Driven Design.

---

## 📌 About the Project  
## 📌 Sobre o Projeto

PayManager allows users to control expenses and incomes, manage multiple wallets, categorize transactions, schedule recurring payments, and receive reminders before due dates.  
O PayManager permite que os usuários controlem despesas e receitas, gerenciem múltiplas carteiras, categorizem transações, agendem pagamentos recorrentes e recebam lembretes antes do vencimento das contas.

The solution was designed to be secure, modular, and easy to extend.  
A solução foi projetada para ser segura, modular e fácil de expandir.

---

## 🚀 Features  
## 🚀 Funcionalidades

### ✔ Multi-user architecture  
### ✔ Arquitetura multiusuário

### ✔ Full transaction management  
### ✔ Gestão completa de transações

- Income (receitas)  
- Expense (despesas)  
- Transfer (transferências)  
- Status: Pending, Paid, Overdue, Canceled  
- Status: Pendente, Pago, Atrasado, Cancelado

### ✔ Recurring transactions  
### ✔ Transações recorrentes

Supports: Daily, Weekly, Monthly, Yearly  
Suporta: Diário, Semanal, Mensal, Anual

### ✔ Reminders  
### ✔ Lembretes

Users receive reminders before due dates.  
Usuários recebem lembretes antes do vencimento.

### ✔ Wallet management  
### ✔ Gerenciamento de carteiras

Multiple wallets per user with initial balance and default wallet.  
Múltiplas carteiras por usuário com saldo inicial e carteira padrão.

### ✔ Categories  
### ✔ Categorias

User-defined and system-defined categories.  
Categorias personalizadas e categorias do sistema.

### ✔ JWT Authentication  
### ✔ Autenticação JWT

Secure login, token generation, and password hashing (BCrypt).  
Login seguro, geração de token e hash de senha (BCrypt).

---

## 🏗️ Architecture  
## 🏗️ Arquitetura

The project follows **Clean Architecture** and **DDD**:  
O projeto segue **Clean Architecture** e **DDD**:

src/
├── FSI.PayManager.Domain
├── FSI.PayManager.Application
├── FSI.PayManager.Infrastructure
└── FSI.PayManager.Api
tests/
└── FSI.PayManager.Domain.Tests


---

## 🛠️ Technologies  
## 🛠️ Tecnologias

- .NET 8  
- ASP.NET Core Web API  
- MySQL  
- Dapper  
- JWT Authentication  
- BCrypt.Net  
- xUnit + FluentAssertions  
- Coverlet  
- Clean Architecture  
- Domain-Driven Design  

---

## 🧪 Tests  
## 🧪 Testes

The Domain layer contains complete unit tests for all entities and repository behaviors.  
A camada Domain contém testes unitários completos para todas as entidades e comportamentos do repositório.

---

## ▶️ How to Run  
## ▶️ Como Executar

### **EN**
1. Clone repository  
2. Configure `appsettings.json`  
3. Run database creation script  
4. Run API  

### **PT**
1. Clone o repositório  
2. Configure o `appsettings.json`  
3. Execute o script de criação do banco  
4. Execute a API  

---

## 📜 License  
## 📜 Licença

This project is open-source under the MIT license.  
Este projeto é open-source sob a licença MIT.

