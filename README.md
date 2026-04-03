# 📝 Task Manager - WinForms & .NET 9.0 (Version 2)

This is a desktop application developed in C# (.NET 9.0) for managing tasks within a small team. The project highlights the practical application of:

🏗️ Layered Architecture
🧩 Design Patterns
📐 SOLID Principles

👉 Version 2 introduces a modern graphical user interface and integration with a real SMTP server for notifications.

## ✨ New Features (v2)

### 🖥️ Complete Graphical User Interface (WinForms)
An interactive form for:
*   Adding tasks (title, description, type, priority, deadline)
*   Viewing tasks in a clean and organized `DataGridView`

### 📧 Real Email Notifications
When a task is marked as "Done", the application automatically sends an email using:
*   MailKit
*   SMTP (Gmail)

### 🔌 Dependency Injection (IoC Container)
Automatic dependency management with:
*   `Microsoft.Extensions.DependencyInjection`

## 🏗️ Application Architecture

The application uses a layered architecture:

**[TaskManager.UI]** &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;→ Presentation (WinForms + IoC)  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;▼  
**[TaskManager.Core]** &nbsp;&nbsp;&nbsp;&nbsp;→ Business Logic + Interfaces (no external dependencies)  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;▲  
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;│  
**[TaskManager.Data]** &nbsp;&nbsp;&nbsp;&nbsp;→ Data Access (SQLite)

📌 **Note:** `Core` is the application's center – it defines the rules, while `UI` and `Data` merely implement them.

## 🏛️ SOLID Principles (Applied)

1.  **Single Responsibility Principle (SRP)**
    *   Validation is separated into `TaskValidator`.
    *   `TaskService` only orchestrates the logic.

2.  **Open/Closed Principle (OCP)**
    *   The notification system uses `ITaskNotifier`.
    *   ✔ Email notifications were added without modifying `TaskService`.

3.  **Liskov Substitution Principle (LSP)**
    *   Hierarchy: `TaskItem` → `DeadlineTask` / `RecurringTask`. Implementations can be swapped without affecting the application.
    *   ✔ `SqliteTaskRepository` and `InMemoryTaskRepository` are interchangeable.

4.  **Interface Segregation Principle (ISP)**
    *   Interfaces are segregated: `ITaskReader` and `ITaskWriter`.
    *   ✔ `ReportService` only uses read operations, preventing accidental modifications.

5.  **Dependency Inversion Principle (DIP)**
    *   `TaskService` depends on abstractions: `ITaskRepository` and `ITaskNotifier`.
    *   ✔ All dependencies are injected via the constructor using the IoC Container.

## 💾 Database & Data Access

*   **🗄️ Storage:** SQLite (local `tasks.db` file)
*   **🧱 Pattern:** Repository Pattern
*   **🔧 Technology:** ADO.NET (`Microsoft.Data.Sqlite`)
*   ❌ No ORM is used (as per requirements).

✔ Manual mapping from `SqliteDataReader` to C# objects is performed.

## 🧪 Automated Testing (NUnit)

The project includes unit tests that cover:

*   ✔ Data validation
*   ✔ Service logic
*   ✔ Liskov Substitution Principle (LSP)
*   ✔ Checks for ISP & DIP through reflection

⚡ The tests are:

*   Fast
*   Isolated
*   Based on `InMemoryTaskRepository`

👉 **Run the tests:**
```bash
dotnet test
```

## 🚀 How to Run the Project

### 📋 Requirements
*   .NET 9.0 SDK

### 📧 Email Configuration

In `src/TaskManager.Core/Notifications/EmailNotifier.cs`, you must add:

*   Your Gmail address
*   A Google-generated App Password

### ▶️ Running the Application (WinForms)

```bash
dotnet run --project src/TaskManager.UI