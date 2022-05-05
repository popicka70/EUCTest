# EUCDemo

Použití

1) Nastavte si ConnectionString v appsettings.json na prázdnou databázi. Bude potřeba právo na zápis a i práva na DDL.
2) Aplikace si při startu ověří, že databázové tabulky existují a pokud ne, vytvoří je. Je použit standardní EF přístup Code First. Create skript není potřeba. 
3) Po ověření existence DB proběhne inicializace tabulky států. Pokud není tabulka naplněna, inicializace použije přiložený JSON se státy a naplní tabulku států podle tohoto seznamu.
4) Aplikace má jednu stránku, kam lze navigovat z menu. Prostě index. Na této stránce je formulář pro zadání osoby.
5) Na všech polích jsou validátory a attribut [Required]
6) Validace RČ je triviální.
7) Stát je výběr ze záznamů tabulky států.
8) Pohlaví je přes enum a validaci rozsahu.
9) Souhlas se zpracováním údajů musí být "true".
10) Po stisku tlačítka "Založit osobu" se provede validace a v případě úspěchu se záznam založí do databáze.
11) Celé je to česky (až na drobné vyjímky), nebyla požadována angličtina v programech, tak jsem to pojmul místně.
12) Jako technologii jsem zvolil EF + Server Blazor, které jsou mi nejbližší.
