
# Fluent-Validation-Beispiele

Das Projekt beinhaltet drei Hauptbereiche: Bsp01Console, Bsp02AspNetMvc und Bsp03WebAPI. Alle Projektteile nutzen die FluentValidation-Bibliothek zur Sicherstellung der Datenintegrität und Verbesserung der Wartbarkeit sowie Lesbarkeit der Validierungslogik. Die Trennung in verschiedene Projekte bietet klare Übersicht und Anpassung für spezifische Anforderungen.


## Bsp01Console

Dieser Teil des Projekts stellt eine Konsolenanwendung dar, die aufzeigt, wie die FluentValidation-Bibliothek in einer einfachen Umgebung implementiert wird. Die Hauptfunktion besteht darin, ein Benutzerobjekt zu erstellen, es mithilfe eines Validators zu überprüfen und die Validierungsergebnisse im Konsolenfenster auszugeben. Diese Einheit ist ideal für einfache Validierungslogiken ohne Webumgebung.

[Weitere Informationen unter Bsp01Console/Readme.md](Bsp01Console/Readme.md)

----

## Bsp02AspNetMvc

Innerhalb dieses Verzeichnisses wird gezeigt, wie FluentValidation in einer ASP.NET MVC-Anwendung verwendet wird. Hierbei verarbeitet der Benutzercontroller HTTP-Anfragen und wendet Validierungsregeln auf Benutzerobjekte an, bevor sie in Views zurückgegeben werden. Diese Komponente demonstriert, wie serverseitige Validierung und Model Binding in einer MVC-Architektur zusammenspielen.

[Weitere Informationen unter Bsp02AspNetMvc/Readme.md](Bsp02AspNetMvc/Readme.md)

----

## Bsp03WebAPI

Der WebAPI-Teil des Projekts illustriert die Nutzung von FluentValidation zur Validierung von Eingabedaten in einer RESTful API. Es zeigt die Unterstützung von automatischer Validierung sowohl auf Server- als auch auf Clientseite, insbesondere durch die Verwendung von FluentValidation Extensions in Kombination mit ASP.NET Core. Dieser Bereich ist dafür ausgelegt, robuste und flexible Webservices zu ermöglichen, die stringent überprüfte Daten annehmen und verarbeiten.

[Weitere Informationen unter Bsp03WebAPI/Readme.md](Bsp03WebAPI/Readme.md)

----





## Git Konto wechseln und commiten

```` bash
git init

touch .gitignore

git add .
git commit -m "Initial commit"

git branch -M main

git remote add origin https://github.com/ugurat/Fluent-Validation-Beispiele.git

git push -u origin main
````

Falls Fehler:

```` bash
PASSWORT LÖSCHEN und ERNEUT ANMELDEN

Gehe zu "Windows-Anmeldeinformationen":
Unter Windows-Anmeldeinformationen "gespeicherte Zugangsdaten für Webseiten und Anwendungen" finden.

Suche nach gespeicherten GitHub-Einträgen:
git:https://github.com oder Ähnliches.

Eintrag löschen und erneut versuchen:

git push -u origin main
````

Aktualisiert

```` bash
git add .
git commit -m "aktualisiert"
git push -u origin main
````

Überschreiben

```` bash

git init

git add .
git commit -m "Initial commit"

git branch -M main

git remote add origin https://github.com/ugurat/Fluent-Validation-Beispiele.git


git push -u origin main --force

````

Mit dem Parameter `--force` wird Git-Repo überschrieben.

----


## Entwickler
- **Name**: Ugur CIGDEM
- **E-Mail**: [ugurat@gmail.com](mailto:ugurat@gmail.com)

---

## Markdown-Datei (.md)

---

