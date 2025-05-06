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
## Admin Account 
```
Email = "ziadbahaa41@gmail.Com";
Password = "246819@$Ziadbahaa!";
```
---
## 3. Apply EF Migrations And Update Database
```
- Add-Migration "" -o Data/Migrations
- Update-Database
```
---

## 📸 Project Screenshots

Here’s a quick look at how the **Online Exam System** looks and works. These screenshots showcase the main features and the clean UI of the platform.

---

<p align="center">
  <img src="https://i.imgur.com/b7Wdrru.png" alt="Dashboard Screenshot" width="300"/>
  <img src="https://i.imgur.com/46qq1d8.png" alt="Exam Page" width="300"/>
  <img src="https://i.imgur.com/m0ZvASs.png" alt="Question Page" width="300"/>
</p>
<br/>

<p align="center">
  <img src="https://i.imgur.com/IDt4Z6w.png" alt="Student Result Page" width="300"/>
  <img src="https://i.imgur.com/iL5i0zd.png" alt="User Profile Page" width="300"/>
</p>

---

### Descriptions:
- **🖥️ Admin Dashboard**: Admin overview of all exam-related data.
- **📝 Create New Exam**: Easily create and manage new exams.
- **❓ Add Questions to Exam**: Add questions and configure your exams with ease.
- **📊 Student Exam Results**: View student results and performance metrics.
- **🙍‍♂️ User Profile & Info**: Users can view and update their profiles.



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


