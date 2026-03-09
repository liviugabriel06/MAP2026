Proiect Laborator 2 - Modelare UML
Diagramele UML pentru sistemul de Gestionare Comenzi

Decizii de modelare: 

    Refactorizare: Am separat GodClass-ul in clase mai mici in asa fel incat o clasa sa faca un singur lucru.

    Decuplare: Am introdus un OrderController pentru a separa logica de business de interfata de comunicare.

    Fluxuri: Diagrama de secventa pentru anulare trateaza cazul in care comanda a fost deja expediata.