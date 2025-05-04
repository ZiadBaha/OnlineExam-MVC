# 🧠 Online Exam System - MVC

A powerful and scalable Online Examination System built with **.NET 8 Web API**, following clean architecture and layered separation.  
Supports full exam management for admins and participation for students.

---

## 📂 Project Structure

OnlineExam/
- │
- ├── OnlineExam.Application # Application layer: Interfaces, Contracts
- ├── OnlineExam.Core # Core Entities, Enums, Shared Models , ....
- ├── OnlineExam.Infrastructure # Repositories & EF Core Logic ...
- ├── OnlineExam.Web # API Layer (Controllers, Services, ...)

---

## 🚀 Features

- ✅ Generic CRUD Services using AutoMapper
- 👨‍🏫 Admin Panel: Exams, Questions, Choices, Results
- 👨‍🎓 Student Exam Participation
- 🔐 Secure Authentication with Identity
- 🔁 DTO Mapping with AutoMapper
- 📦 EF Core with SQL Server
- 🏗️ Clean Architecture (SOLID Principles)

---

## 🛠️ Tech Stack

- ASP.NET Core 8 Web MVC
- Entity Framework Core
- AutoMapper
- SQL Server
- Identity

---
## 2. Update Database Connection
```
"ConnectionStrings": {
  "DefaultConnection": "server = . ; Database = OnlineExamDb ; Trusted_Connection = true ; MultipleActiveResultSets = True ;TrustServerCertificate=True"
}
```
---
## 3. Apply EF Migrations And Update Database
```
- Add-Migration "" -o Data/Migrations
- Update-Database
```
---
## 📸 Screenshots

### 🖼️ Login Page
![Login Page] (https://imgur.com/46qq1d8.png)

### 🖼️ Dashboard
![Dashboard] (https://imgur.com/b7Wdrru.png)

### 🖼️ Exam Creation
![quize Resultn] (https://i.imgur.com/zzzzz3.png](https://imgur.com/m0ZvASsp.g)

---
## ⚙️ Getting Started
### 1. Clone the Repository
```bash
git clone https://github.com/your-username/OnlineExam.git
cd OnlineExam
```
---
## 🧑‍💻 Author
- Ziad Bahaa
- Backend Developer – ASP.NET | Clean Architecture
- 🔗 [LinkedIn](https://www.linkedin.com/in/ziad-bahaa-b04561265/)  
- 🐙 [GitHub](https://github.com/ZiadBaha)
- 📧 [Email](ziadbahaa41@gmail.com)
- 📞 [Phone](01022673000)


