# Task Manager - MAP (Metode Avansate de Programare)

Acesta este un proiect dezvoltat în C# (.NET 9.0) pentru cursul de Metode Avansate de Programare. Aplicația este un manager de sarcini care demonstrează aplicarea practică a principiilor de design software (SOLID), șabloanelor de proiectare (Design Patterns) și arhitecturii stratificate.

---

## 🚀 Laboratorul 3: Arhitectură, Baze de Date și Interfață Grafică (WinForms)

În acest laborator, aplicația a fost extinsă dintr-un model de bază într-o soluție complet funcțională, cu persistență reală și interfață utilizator.

### 🎯 Obiective Implementate

#### 1. Principii SOLID
*   **Single Responsibility Principle (SRP):** Logica de validare a sarcinilor a fost extrasă într-o clasă dedicată (`TaskValidator`), separând-o de modelele de date și de serviciile de business.
*   **Open/Closed Principle (OCP):** Implementarea sistemului de notificări folosește o interfață (`ITaskNotifier`) și un dicționar de strategii. Sistemul poate fi extins cu noi tipuri de notificări (ex: `ConsoleNotifier`, `EmailNotifier`, `FileLogNotifier`) fără a modifica clasa principală `TaskService`.
*   **Liskov Substitution Principle (LSP):** Ierarhia de clase (`TaskItem` ca bază, derivată în `DeadlineTask` și `RecurringTask`) respectă contractele și precondițiile. De exemplu, metoda `Complete()` aruncă o excepție clară dacă sarcina este deja finalizată (Design by Contract).

#### 2. Design Patterns: Repository Pattern
Accesul la date a fost decuplat de logica de business prin interfața `ITaskRepository`. Au fost create două implementări:
*   `InMemoryTaskRepository`: Folosit pentru teste unitare rapide, stocând datele într-o colecție locală.
*   `SqliteTaskRepository`: Folosit în producție, realizează conexiunea la o bază de date reală **SQLite** (`tasks.db`). Interogările (`SELECT`, `INSERT`, `UPDATE`, `DELETE`) sunt scrise manual folosind ADO.NET (`SqliteCommand`, `SqliteDataReader`), respectând cerința de a **nu** utiliza un ORM (precum Entity Framework).

#### 3. Interfața Grafică (UI)
*   S-a implementat o interfață desktop folosind **Windows Forms (WinForms)**.
*   Interfața este construită programatic (din cod, fără designer vizual), afișând un `DataGridView` conectat direct la baza de date și butoane pentru adăugarea, finalizarea și ștergerea sarcinilor.
*   UI-ul este decuplat de logică, comunicând exclusiv prin intermediul clasei `TaskService` (Dependency Injection manual la nivelul `Program.cs`).

#### 4. Testare Automată (NUnit)
Proiectul include un pachet de teste unitare scrise folosind framework-ul **NUnit**, care validează:
*   Corectitudinea validatorului (`TaskValidator`).
*   Respectarea LSP pentru toate derivatele clasei `TaskItem`.
*   Apelarea corectă a notificărilor (mocking pentru OCP).
*   Funcționarea adăugării în `InMemoryTaskRepository`.

---

## 🛠️ Cum se rulează proiectul

### 1. Pornirea Aplicației (Interfața Grafică)
Pentru a porni aplicația cu interfața WinForms și baza de date SQLite, deschide un terminal în folderul rădăcină al proiectului și rulează:

```bash
dotnet run --project src/TaskManager.UI
