# Laborator 2 - Modelare UML pentru un Sistem de Gestiune Comenzi

Acest repository conține artefactele UML necesare modelării unui sistem de gestiune a comenzilor. Scopul principal al acestui laborator a fost identificarea problemelor de design arhitectural dintr-un sistem existent și propunerea unei refactorizări orientate pe obiecte, având la bază reducerea cuplării și creșterea coeziunii.

## Decizii de Modelare

Pe parcursul realizării diagramelor, am luat următoarele decizii arhitecturale și de design:

1. **Diagrama de Cazuri de Utilizare (Use Case)**
   - Am identificat cei 3 actori principali: `Client`, `Admin` și sistemul extern `Sistem de Plată`.
   - Am evidențiat relații de tip `«include»` (ex: plasarea unei comenzi necesită obligatoriu procesarea plății) și `«extend»` (ex: rambursarea plății se face opțional în cadrul fluxului de anulare).

2. **Diagrama de Clasă - Starea Inițială (Before)**
   - Am modelat clasa `OrderManager` ca o veritabilă **"God Class"**. Aceasta concentrează în mod greșit responsabilități multiple: validare stoc, procesare plăți, salvare directă în baza de date și trimitere de email-uri.
   - Am evidențiat **cuplarea directă** cu infrastructura (clase specifice pentru SQL și SMTP), demonstrând încălcarea principiului *Single Responsibility* și lipsa abstractizării.

3. **Diagrama de Clasă - Refactorizare (After)**
   - Am "spart" God Class-ul în mai multe componente mici, fiecare având o singură responsabilitate clară (`ManagerComenzi`, `RepozitoriuComenzi`, `ProcesorPlata`, `ServiciuEmail`, etc.).
   - Am redus cuplarea făcând ca `ManagerComenzi` să acționeze doar ca un coordonator care depinde de celelalte servicii, în loc să implementeze el însuși logica tehnică.

4. **Diagramele de Secvență**
   - Au fost realizate folosind **Mermaid** pentru a putea fi versionate ușor ca text.
   - **Fluxul de plasare a comenzii:** Demonstrează ordinea apelurilor și condițiile de oprire timpurie (ex: stoc epuizat, plată respinsă) folosind blocuri `alt`.
   - **Fluxul de anulare a comenzii:** Tratează cazurile de eroare (comandă inexistentă - 404, sau comandă deja expediată) și integrează un flux opțional (`opt`) pentru rambursarea banilor și notificarea prin email.

## Structura Repository-ului

Toate diagramele se găsesc în directorul `docs/`:

* `docs/use-case.png` - Diagrama cazurilor de utilizare
* `docs/class-diagram-before.png` - Structura claselor înainte de refactorizare (God Class)
* `docs/class-diagram-after.png` - Structura claselor propusă după refactorizare
* `docs/sequence-place-order.md` - Diagrama de secvență pentru plasarea comenzii (Mermaid)
* `docs/sequence-cancel-order.md` - Diagrama de secvență pentru anularea comenzii (Mermaid)