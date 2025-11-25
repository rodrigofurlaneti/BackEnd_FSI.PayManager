# PayManager – Personal Finance Management System 🇺🇸  
# PayManager – Sistema de Gerenciamento Financeiro Pessoal 🇧🇷  

---

## 🇺🇸 English Version

### 🧾 Overview

PayManager is a modern and scalable personal finance management system built with **.NET 8**, following **Clean Architecture**, **DDD**, and **Clean Code** principles.  
It allows users to manage incomes, expenses, wallets, categories, recurring transactions, and reminders — all protected with JWT authentication.

---

## 🚀 Features

- Multi-user architecture  
- Full transaction management:
  - Income  
  - Expense  
  - Transfer  
  - Status: Pending, Paid, Overdue, Canceled
- Recurring transactions (Daily, Weekly, Monthly, Yearly)  
- Reminder system before due dates  
- Wallet management with initial balance and default wallet  
- Categories (custom and system-defined)  
- Secure JWT authentication  
- Password hashing (BCrypt)  
- Full domain-level unit testing with xUnit + FluentAssertions  

---

## 🏗️ Architecture

```mermaid
flowchart TD

    subgraph API Layer
        A1[Controllers\nAuth / Users / Wallets / Categories / Transactions / Recurring / Reminders]
    end

    subgraph Application Layer
        B1[DTOs]
        B2[Services\nUseCases]
        B3[Interfaces]
        B4[Mappers]
    end

    subgraph Domain Layer
        C1[Entities\nUser, Wallet, Category,\nFinancialTransaction, Reminder,\nRecurringTransaction]
        C2[Domain Interfaces\n(IRepository)]
        C3[Value Objects]
    end

    subgraph Infrastructure Layer
        D1[Repositories (Dapper)]
        D2[MySQL Context]
        D3[Jwt / Hash Providers]
    end

    subgraph External
        E1[(MySQL Database)]
        E2[(JWT Authentication)]
    end

    A1 --> B2
    B2 --> C1
    B2 --> C2
    C2 --> D1
    D1 --> E1
    A1 --> E2
```

---

## 🛠️ Technologies

- .NET 8  
- ASP.NET Core Web API  
- MySQL  
- Dapper  
- JWT Authentication  
- BCrypt.Net  
- xUnit + FluentAssertions  
- Clean Architecture  
- Domain-Driven Design  

---

## ▶️ How to Run

1. Clone the repository  
2. Configure `appsettings.json`  
3. Run the database creation script  
4. Start the API (`dotnet run`)  

---

## 📜 License

This project is open-source under the MIT License.

---

---

## 🇧🇷 Versão em Português (Brasil)

### 🧾 Visão Geral

O PayManager é um sistema moderno e escalável de controle financeiro pessoal desenvolvido em **.NET 8**, seguindo **Clean Architecture**, **DDD** e **Clean Code**.  
O sistema permite controlar receitas, despesas, carteiras, categorias, transações recorrentes e lembretes — tudo com autenticação JWT.

---

## 🚀 Funcionalidades

- Arquitetura multiusuário  
- Gestão completa de transações:
  - Receita  
  - Despesa  
  - Transferência  
  - Status: Pendente, Pago, Atrasado, Cancelado
- Transações recorrentes (Diária, Semanal, Mensal, Anual)  
- Lembretes antes do vencimento  
- Gerenciamento de carteiras com saldo inicial e carteira padrão  
- Categorias personalizadas e do sistema  
- Autenticação JWT segura  
- Hash de senha com BCrypt  
- Testes unitários completos na camada Domain  

---

## 🏗️ Arquitetura

```mermaid

flowchart TD

    subgraph API Layer
        A1[Controllers\nAuth / Users / Wallets / Categories / Transactions / Recorrentes / Lembretes]
    end

    subgraph Application Layer
        B1[DTOs]
        B2[Serviços\nUseCases]
        B3[Interfaces]
        B4[Mappers]
    end

    subgraph Domain Layer
        C1[Entidades\nUser, Wallet, Category,\nFinancialTransaction, Reminder,\nRecurringTransaction]
        C2[Interfaces de Domínio\n(IRepository)]
        C3[Value Objects]
    end

    subgraph Infrastructure Layer
        D1[Repositórios (Dapper)]
        D2[MySQL Context]
        D3[Jwt / Hash Providers]
    end

    subgraph External
        E1[(Banco MySQL)]
        E2[(Autenticação JWT)]
    end

    A1 --> B2
    B2 --> C1
    B2 --> C2
    C2 --> D1
    D1 --> E1
    A1 --> E2
```

---

## 🛠️ Tecnologias

- .NET 8  
- ASP.NET Core Web API  
- MySQL  
- Dapper  
- JWT Authentication  
- BCrypt.Net  
- xUnit + FluentAssertions  
- Clean Architecture  
- Domain-Driven Design  

---

## ▶️ Como Executar

1. Clone o repositório  
2. Configure o arquivo `appsettings.json`  
3. Execute o script SQL do banco  
4. Inicie a API (`dotnet run`)  

---

## 📜 Licença

Este projeto é open-source sob a licença MIT.

