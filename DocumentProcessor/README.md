# Sistem de Procesare a Documentelor (Laboratorul 6)

Acest proiect reprezintă o soluție completă pentru procesarea documentelor, dezvoltată de la zero. Scopul principal al arhitecturii este demonstrarea practică a **șabloanelor de proiectare structurale** (Structural Design Patterns) în C#. 

Proiectul este testabil, scalabil și demonstrează cum putem extinde sau adapta funcționalități fără a modifica codul de bază existent.

---

## Șabloanele Structurale Implementate

### 1. Adapter (Adaptorul)
* **Ce problemă concretă rezolvă:** Sistemul nostru modern folosește interfața `IDocumentParser` și returnează un model de date `Document`. Totuși, am fost nevoiți să integrăm o componentă veche, `LegacyXmlParser`, pe care nu aveam voie să o modificăm. Aceasta folosea tipuri de date incompatibile (`XmlDocument` și `LegacyDocument`). Adaptorul rezolvă problema acționând ca un "traducător" între sistemul nostru și clasa veche, permițând colaborarea lor.
* **Unde se regăsește în cod:** * Interfața țintă: `src/DocumentProcessor.Core/Interfaces/IDocumentParser.cs`
  * Componenta incompatibilă: `src/DocumentProcessor.Core/Adapter/LegacyXmlParser.cs`
  * Adaptorul: `src/DocumentProcessor.Core/Adapter/XmlParserAdapter.cs`

### 2. Decorator (Decoratorul)
* **Ce problemă concretă rezolvă:** Am dorit să adăugăm funcționalități noi procesului de parsare (precum măsurarea timpului de execuție, validarea datelor și salvarea în memorie/caching) fără a modifica logica internă a parserelor existente și fără a crea o ierarhie de moștenire gigantică și rigidă. Decoratorul ne permite să "îmbrăcăm" obiectul de bază în noi straturi de comportament la runtime.
* **Unde se regăsește în cod:** Toate se află în `src/DocumentProcessor.Core/Decorators/`:
  * Clasa de bază: `DocumentParserDecorator.cs`
  * Straturile adăugate: `LoggingDocumentParser.cs`, `ValidationDocumentParser.cs` și `CachingDocumentParser.cs`.

### 3. Facade (Fațada)
* **Ce problemă concretă rezolvă:** Din cauza utilizării Adaptorului și a lanțului multiplu de Decoratori, procesul de asamblare și rulare a unui parser a devenit foarte complex pentru client. Mai mult, clientul ar fi trebuit să gestioneze manual excepțiile (ex: `ValidationException`). Facade-ul ascunde toată această complexitate ("spaghetele" de instanțieri) în spatele unei singure metode simple, care returnează un obiect prietenos de tip `ProcessingResult`, evitând astfel crăparea aplicației.
* **Unde se regăsește în cod:** * `src/DocumentProcessor.Core/Facade/DocumentProcessingFacade.cs` (Metoda `Process(fileName, fileContent)`)

---

## Cum se rulează proiectul

Proiectul este validat complet prin teste automate. Pentru a verifica funcționalitatea, rulați comenzile:

```bash
# Pentru a compila soluția:
dotnet build

# Pentru a rula toate testele NUnit:
dotnet test