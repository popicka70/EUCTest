# EUCTest

EUCDemo
Použití

1) Nastavte si ConnectionString v appsettings.json na prázdnou databázi. Bude potřeba právo na zápis a i práva na DDL.
2) Aplikace si při startu ověří, že databázové tabulky existují a pokud ne, vytvoří je. Je použit standardní EF přístup Code First. Create skript není potřeba, ale je přiložen v /scripts.
3) Po ověření existence DB proběhne inicializace tabulky států. Pokud není tabulka naplněna, inicializace použije přiložený JSON se státy a naplní tabulku států podle tohoto seznamu.
4) Aplikace má jednu stránku - index. Přepínáním v navigačním menu lze přepínat mezi cs-CZ a en verzemi.
5) Na všech požadovaných polích jsou validátory.
6) Stát je výběr ze záznamů tabulky států.
7) Pohlaví je přes enum a validaci rozsahu. Enum jsem nelokalizoval.
8) Souhlas se zpracováním údajů musí být "true".
9) Po stisku tlačítka "Založit osobu" se provede validace a v případě úspěchu se záznam založí do databáze.
10) Jako technologii jsem zvolil EF + Server Blazor, které jsou mi nejbližší.
