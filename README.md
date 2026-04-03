# 📝 Task Manager - WinForms & .NET 9.0 (Versiunea 2)

Acesta este un proiect dezvoltat în C# (.NET 9.0), reprezentând o aplicație robustă de gestiune a sarcinilor pentru o echipă mică. Aplicația demonstrează aplicarea practică a arhitecturii stratificate (Layered Architecture), a șabloanelor de proiectare (Design Patterns) și respectarea strictă a **Principiilor SOLID**.

Versiunea 2 a proiectului aduce o interfață grafică interactivă și integrarea cu un server SMTP real pentru notificări.

---

## ✨ Funcționalități Noi (Versiunea 2)
* **Interfață Manuală Completă:** Formular interactiv pentru adăugarea sarcinilor (titlu, descriere, tip, prioritate, dată limită) și vizualizarea lor într-un `DataGridView` curat.
* **Notificări Reale pe Email:** La marcarea unei sarcini ca *Done*, aplicația trimite un email real utilizatorului folosind pachetul `MailKit` și protocolul SMTP de la Google.
* **IoC Container:** Instanțierea claselor este automatizată complet folosind `Microsoft.Extensions.DependencyInjection`.

---

## 🏗 Arhitectura Aplicației

Proiectul folosește o **Arhitectură Stratificată** pentru a decupla logica de business de interfață și de baza de date:

```text
[TaskManager.UI] (Strat Prezentare + IoC Container / WinForms)
       │
       ▼
[TaskManager.Core] (Logică Business + Interfețe / Fără dependențe externe)
       ▲
       │
[TaskManager.Data] (Strat Acces Date / SQLite)
```

Notă: Proiectul Core este inima aplicației. El dictează regulile (interfețele), iar Data și UI doar le implementează.

🏛 Decizii de Design: Principiile SOLID
Aplicarea principiilor SOLID poate fi observată clar în codul sursă, după cum urmează:

Single Responsibility Principle (SRP)

Logica de validare a sarcinilor (titlu nenul, lungime maximă etc.) a fost extrasă într-o clasă dedicată (TaskValidator).

Așadar, TaskService se ocupă strict de orchestrarea datelor, iar TaskValidator de validarea lor.

Open/Closed Principle (OCP)

Sistemul de notificări folosește o interfață (ITaskNotifier) și un dicționar de strategii.

Dovada: Pentru a adăuga trimiterea de emailuri reale în Versiunea 2, am creat/modificat doar clasa EmailNotifier, fără a modifica absolut nicio linie de cod în TaskService. Aplicația a fost deschisă pentru extindere, dar închisă pentru modificare.

Liskov Substitution Principle (LSP)

Avem ierarhia de sarcini (TaskItem bază, DeadlineTask, RecurringTask). Suprascrierea metodei Complete() funcționează fără să strice așteptările programului.

TaskService lucrează perfect cu orice implementare a interfeței ITaskRepository, demonstrând că SqliteTaskRepository (baza de date reală) și InMemoryTaskRepository (pentru teste) pot fi interschimbate fără ca programul să crape.

Interface Segregation Principle (ISP)

Interfața principală a fost divizată în două roluri specifice: ITaskReader și ITaskWriter.

Clasa ReportService necesită doar ITaskReader. Astfel, protejăm datele: serviciul de rapoarte nu are acces tehnic la funcțiile de modificare (Add, Delete), eliminând riscul ștergerilor accidentale.

Dependency Inversion Principle (DIP)

Modulele de nivel înalt (TaskService) depind doar de abstracții (ITaskRepository, ITaskNotifier).

Toate dependențele sunt injectate automat prin constructor folosind un IoC Container (ServiceCollection în Program.cs), decuplând complet aplicația de implementările concrete.

💾 Baza de Date & Accesul la Date
Accesul la date respectă Repository Pattern. S-a folosit baza de date locală SQLite (fișierul tasks.db se generează automat).
Implementarea din clasa SqliteTaskRepository realizează maparea manuală din SqliteDataReader în obiecte C#, folosind pachetul ADO.NET (Microsoft.Data.Sqlite), fără a folosi un sistem ORM (conform cerințelor).

🧪 Testare Automată (NUnit)
Proiectul include o suită de 13 teste unitare complet funcționale, care acoperă:

Comportamentul validatorului.

Logica din serviciul principal.

Demonstrarea principiului LSP pe ierarhia de Task-uri.

Demonstrarea ISP și DIP, validând prin reflexie faptul că TaskService cere explicit interfețe în constructor.

Testele sunt izolate și ultra-rapide datorită folosirii InMemoryTaskRepository. Toate pot fi rulate prin comanda:
dotnet test

🚀 Cum se rulează proiectul
1. Cerințe prealabile:

.NET 9.0 SDK instalat.

Pentru notificările prin Email: În fișierul EmailNotifier.cs (din TaskManager.Core), trebuie introdusă adresa de Gmail și Parola de Aplicație generată de Google.

2. Rularea interfeței grafice (WinForms):
Deschide un terminal în rădăcina proiectului și rulează:

Bash
dotnet run --project src/TaskManager.UI