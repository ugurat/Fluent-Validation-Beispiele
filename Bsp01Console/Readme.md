
# FluentValidation

Dieses Projekt demonstriert die Verwendung der FluentValidation-Bibliothek zur Validierung eines Benutzerobjekts in einer Konsolenanwendung. Zunächst wird ein Modell namens `Benutzer` definiert, das die Eigenschaften `Name` und `Alter` umfasst. Ein zugehöriger Validator setzt Validierungsregeln mithilfe der Fluent-Syntax um. Diese Regeln stellen sicher, dass der Name weder leer ist noch außerhalb einer bestimmten Länge liegt und dass das Alter innerhalb eines definierten Bereichs liegt. Die Hauptlogik der Anwendung umfasst die Erstellung eines Benutzerobjekts, die Validierung desselben durch den Validator und die Ausgabe der Validierungsergebnisse, einschließlich etwaiger Fehler, im Konsolenfenster. Die klare Strukturierung des Projekts mit getrennten Ordnern für Modelle und Validatoren trägt zur besseren Wartbarkeit bei.

----

## Orderstruktur

Übersichtliche Struktur des Projekts mit getrennten Ordnern für Modelle und Validatoren.

```` bash

Bsp01
├── Models
│   └── Benutzer.cs
├── Validators
│   └── BenutzerValidator.cs
├── Program.cs

````
Die Projektstruktur besteht aus drei Hauptkomponenten: `Models`, `Validators` und der ausführenden Datei `Program.cs`. Im `Models`-Ordner befindet sich `Benutzer.cs`, das die Datenstruktur des Benutzerobjekts definiert, einschließlich Eigenschaften wie Name und Alter, die überprüft werden sollen. Der `Validators`-Ordner enthält `BenutzerValidator.cs`, in dem die Validierungslogik implementiert ist, um sicherzustellen, dass Benutzerobjekte den angegebenen Regeln entsprechen – speziell, dass Name und Alter gültige Werte aufweisen. Die `Program.cs` orchestriert den gesamten Validierungsprozess, indem sie ein Benutzerobjekt erstellt, den Validator darauf anwendet und die Validierungsergebnisse in der Konsole ausgibt. Diese Struktur ermöglicht eine klare Trennung von Datenmodell, Validierungslogik und Programmausführung, wodurch der Code sauber und wartbar bleibt.

---

## Nuget Packete

Notwendiges NuGet-Paket zum Implementieren der Validierungslogik.

```` bash

Install-Package FluentValidation -Version 11.11.0

````
FluentValidation: https://www.nuget.org/packages/FluentValidation/11.11.0


---

## Models / Benutzer.cs

```` csharp

namespace Bsp01.Models
{
    internal class Benutzer
    {
        public string Name { get; set; }
        public int Alter { get; set; }

    }
}

```` 

---

## Validators / BenutzerValidator.cs

```` csharp

using Bsp01.Models;
using FluentValidation;

namespace Bsp01.Validators
{
    internal class BenutzerValidator : AbstractValidator<Benutzer>
    {

        public BenutzerValidator()
        {

            // Festlegung der Regeln

            RuleFor(b => b.Name) // Zugriff auf die Eigenschaften aus der Klasse "Benutzer"
            .NotEmpty().WithMessage("Der Name darf nicht leer sein.")
            .Length(2, 50).WithMessage("Der Name muss zwischen 2 und 50 Zeichen lang sein.");

            RuleFor(b => b.Alter) // Zugriff auf die Eigenschaften aus der Klasse "Benutzer"
                .GreaterThan(0).WithMessage("Das Alter muss größer als 0 sein.")
                .LessThanOrEqualTo(120).WithMessage("Das Alter darf höchstens 120 Jahre betragen.");

        }

    }
}

````

Die Klasse `BenutzerValidator` implementiert die Validierungslogik, indem sie von `AbstractValidator<Benutzer>` erbt und im Konstruktor spezifische Regeln für das Benutzer-Modell definiert. Die `RuleFor`-Methoden wenden Validierungsbedingungen auf die Name- und Alter-Eigenschaften des Modells an. Die Regel für Name erfordert, dass der Wert nicht leer ist und zwischen 2 und 50 Zeichen lang ist, während die Regel für Alter sicherstellt, dass es größer als 0 und maximal 120 ist. Jede Bedingung ist mit einer aussagekräftigen Fehlermeldung verknüpft, die im Falle eines Verstoßes den Benutzer informiert, wodurch eine klare und strukturierte Prüfung der Instanzen des Benutzer-Modells ermöglicht wird.


## Program.cs


```` csharp

        static void Main(string[] args)
        {

            Console.WriteLine("==== Willkommen zu den Fluent-Validators! ===");

            // Benutzerobjekt erstellen
            var benutzer = new Benutzer { Name = "", Alter = -5 };

            // Den Validator für das Benutzerobjekt instanziieren
            var benutzerValidator = new BenutzerValidator();

            // Das Objekt mit dem Validator überprüfen
            var ergebnis = benutzerValidator.Validate(benutzer);

            // Validierungsergebnisse prüfen und ausgeben
            if (!ergebnis.IsValid)
            {
                Console.WriteLine("Das Benutzerobjekt ist ungültig. Details:");
                foreach (var fehler in ergebnis.Errors)
                {
                    Console.WriteLine($"- Fehler: {fehler.ErrorMessage}");
                }
            }
            else
            {
                Console.WriteLine("Das Benutzerobjekt ist gültig.");
            }

        }

````

In der `Program.cs` wird innerhalb der Main-Methode die Hauptfunktionalität der Anwendung ausgeführt. Zunächst wird ein Benutzer-Objekt mit ungültigen Beispielwerten, nämlich einem leeren Namen und einem negativen Alter, erstellt. Anschließend wird ein 
BenutzerValidator instanziiert, um das erstellte Benutzer-Objekt zu validieren. Die Methode Validate überprüft, ob das Objekt den festgelegten Regeln entspricht, und speichert das Ergebnis. Ergebnisfehler werden ausgewertet und spezifische Fehlermeldungen in der Konsole ausgegeben, falls die Validierung nicht erfolgreich ist. Andernfalls wird eine Bestätigung über die Gültigkeit des Objekts angezeigt, was dem Benutzer eine klare Rückmeldung über den Status der Daten gibt.

