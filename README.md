# EUCTest

EUCDemo
Použití

Nastavte si ConnectionString v appsettings.json na prázdnou databázi. Bude potřeba právo na zápis a i práva na DDL.
Aplikace si při startu ověří, že databázové tabulky existují a pokud ne, vytvoří je. Je použit standardní EF přístup Code First. Create skript není potřeba, ale je přiložen v /scripts.
Po ověření existence DB proběhne inicializace tabulky států. Pokud není tabulka naplněna, inicializace použije přiložený JSON se státy a naplní tabulku států podle tohoto seznamu.
Aplikace má jednu stránku - index. Přepínáním v navigačním menu lze přepínat mezi cs-CZ a en verzemi.
Na všech požadovaných polích jsou validátory.
Stát je výběr ze záznamů tabulky států.
Pohlaví je přes enum a validaci rozsahu. Enum jsem nelokalizoval.
Souhlas se zpracováním údajů musí být "true".
Po stisku tlačítka "Založit osobu" se provede validace a v případě úspěchu se záznam založí do databáze.
Jako technologii jsem zvolil EF + Server Blazor, které jsou mi nejbližší.
