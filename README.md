📝 Task Manager - WinForms & .NET 9.0 (Versiunea 2)

Aplicație desktop dezvoltată în C# (.NET 9.0) pentru gestionarea sarcinilor într-o echipă mică.
Proiectul evidențiază aplicarea practică a:

🏗 Layered Architecture
🧩 Design Patterns
📐 Principiilor SOLID

👉 Versiunea 2 introduce o interfață grafică modernă și integrarea cu un server SMTP real pentru notificări.

✨ Funcționalități Noi (v2)
🖥 Interfață grafică completă (WinForms)
Formular interactiv pentru:
adăugare task-uri (titlu, descriere, tip, prioritate, deadline)
vizualizare într-un DataGridView curat și organizat
📧 Notificări reale prin email
La marcarea unui task ca Done, aplicația trimite automat email folosind:
MailKit
SMTP (Gmail)
🔌 Dependency Injection (IoC Container)
Gestionarea automată a dependențelor cu:
Microsoft.Extensions.DependencyInjection
🏗 Arhitectura aplicației

Aplicația folosește o arhitectură stratificată (Layered Architecture):

[TaskManager.UI]        → Prezentare (WinForms + IoC)
        │
        ▼
[TaskManager.Core]      → Logică business + interfețe (fără dependențe externe)
        ▲
        │
[TaskManager.Data]      → Acces la date (SQLite)

📌 Observație:
Core este centrul aplicației – definește regulile, iar UI și Data doar le implementează.

🏛 Principii SOLID (aplicate)
1. Single Responsibility Principle (SRP)
Validarea este separată în TaskValidator
TaskService doar orchestrează logica
2. Open/Closed Principle (OCP)
Sistemul de notificări folosește ITaskNotifier
✔ Email-ul a fost adăugat fără modificarea TaskService
3. Liskov Substitution Principle (LSP)
Ierarhie:
TaskItem
DeadlineTask
RecurringTask
Implementările pot fi înlocuite fără a afecta aplicația

✔ SqliteTaskRepository și InMemoryTaskRepository sunt interschimbabile

4. Interface Segregation Principle (ISP)
Interfețele sunt separate:
ITaskReader
ITaskWriter

✔ ReportService folosește doar citire → fără risc de modificare accidentală

5. Dependency Inversion Principle (DIP)
TaskService depinde de abstracții:
ITaskRepository
ITaskNotifier

✔ Toate dependențele sunt injectate prin constructor (IoC Container)

💾 Baza de date & accesul la date
🗄 SQLite (fișier local tasks.db)
🧱 Repository Pattern
🔧 ADO.NET (Microsoft.Data.Sqlite)
❌ Fără ORM (conform cerințelor)

✔ Mapare manuală din SqliteDataReader → obiecte C#

🧪 Testare automată (NUnit)

Proiectul include 13 teste unitare care acoperă:

✔ validarea datelor
✔ logica serviciilor
✔ principiul LSP
✔ verificări pentru ISP & DIP (prin reflecție)

⚡ Testele sunt:

rapide
izolate
bazate pe InMemoryTaskRepository

👉 Rulează testele:

dotnet test
🚀 Cum rulezi proiectul
📋 Cerințe
.NET 9.0 SDK
📧 Configurare email

În EmailNotifier.cs trebuie să adaugi:

adresa Gmail
App Password generată de Google
▶ Rulare aplicație (WinForms)

```bash
dotnet run --project src/TaskManager.UI
```