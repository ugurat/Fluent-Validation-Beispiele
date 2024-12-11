

# FluentValidation

Das Projekt demonstriert, wie FluentValidation in einer ASP.NET Core Web API zur Validierung von Benutzerdaten eingesetzt wird. Die Ordnerstruktur umfasst `Controllers`, `Models`, `Validators`, und `Program.cs`. Im `Models`-Ordner definiert `Benutzer.cs` das Modell mit `Name` und `Alter`. Der `BenutzerValidator` stellt sicher, dass der Name nicht leer ist und zwischen 2 und 50 Zeichen liegt, und das Alter zwischen 1 und 120 Jahren ist. Der `BenutzerController` verwaltet `POST`-Anfragen zur Benutzererstellung und gibt bei Erfolg eine Bestätigungsnachricht zurück. In `Program.cs` wird die Anwendung konfiguriert. FluentValidation wird für automatische Server- und Client-validierung hinzugefügt. Dies ermöglicht konsistente Datensicherheit, während Swagger die API-Dokumentation und -Testung unterstützt.

----

## Orderstruktur


````

Bsp03
├── Controllers
│   └── BenutzerController.cs
├── Models
│   └── Benutzer.cs
├── Validators
│   └── BenutzerValidator.cs
├── Program.cs

````

Die Projektstruktur von Bsp03 gliedert sich in mehrere zentrale Komponenten, die jeweils spezifische Aufgaben erfüllen. Im `Controllers`-Verzeichnis befindet sich der `BenutzerController.cs`, der die HTTP-Anfragen der API verwaltet. Im `Models`-Ordner wird das `Benutzer.cs`-Modell definiert, das die Benutzerdaten strukturiert. Der `Validators`-Ordner enthält `BenutzerValidator.cs`, das die Validierungsregeln für die Daten festlegt. Die `Program.cs` dient als Startpunkt der Anwendung, in dem die Serverkonfiguration und die Einbindung der verschiedenen Komponenten erfolgt. Diese Struktur fördert eine saubere Trennung der Verantwortlichkeiten und unterstützt die Wartbarkeit des Projekts.


## Nuget Packete

```` bash

Install-Package FluentValidation.AspNetCore -Version 11.3.0

````
FluentValidation: https://www.nuget.org/packages/FluentValidation.AspNetCore/11.3.0

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

Der `BenutzerValidator` ist eine Klasse, die von `AbstractValidator<Benutzer>` erbt, um detaillierte Validierungsregeln für ein `Benutzer`-Objekt festzulegen. Zunächst wird die `Name`-Eigenschaft überprüft, um sicherzustellen, dass sie nicht leer ist und ihre Länge zwischen 2 und 50 Zeichen liegt. Für die `Alter`-Eigenschaft wird verifiziert, dass der Wert größer als 0 und maximal 120 ist. Jede dieser Regeln ist mit spezifischen Fehlermeldungen verknüpft, die bei einem Regelverstoß generiert werden. Diese Einrichtung ermöglicht es, Benutzerobjekte systematisch und effektiv auf vordefinierte Kriterien zu prüfen, was die Zuverlässigkeit der Daten gewährleistet, bevor sie weiter in der Anwendung verwendet werden.

## Controllers / BenutzerController.cs

``` csharp

using Bsp03.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bsp03.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BenutzerController : ControllerBase
    {

        [HttpPost]
        public IActionResult ErstelleBenutzer([FromBody] Benutzer benutzer)
        {
            return Ok(new { Nachricht = "Benutzer wurde erfolgreich erstellt!", Benutzer = benutzer });
        }

    }
}

```

Der `BenutzerController` in einer ASP.NET Core Web API-Anwendung ist für die Handhabung von HTTP-Anfragen an die API verantwortlich, die auf Benutzerinformationen abzielen. Mit den Attributen `[Route("api/[controller]")]` und `[ApiController]` wird der Controller als RESTful-API-Endpunkt konfiguriert, der Anfragen an die URL "api/benutzer" verarbeitet. Die Methode `ErstelleBenutzer` ist eine `POST`-Operation, die erwartet, dass ein `Benutzer`-Objekt in der Anforderung enthalten ist, welches durch das `[FromBody]`-Attribut angibt, dass die Daten im Anfragekörper erwartet werden. Sobald der Benutzer empfangen wird, erstellt die Methode eine erfolgreiche Antwort (`Ok`) mit einer Nachricht und dem erstellten Benutzerobjekt, um zu bestätigen, dass die Erstellung erfolgreich war. Diese Implementierung ermöglicht einen einfachen und klaren Datenempfang und eine Rückmeldung in einem API-Kontrollfluss.


## Program.cs


``` csharp

...

            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            // EINTRAGEN - FluentValidation konfigurieren
            builder.Services.AddFluentValidationAutoValidation();
            builder.Services.AddFluentValidationClientsideAdapters();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

...

```

In der `Program.cs`-Datei einer ASP.NET Core-Anwendung beginnt die Konfiguration des Webservers mit `WebApplication.CreateBuilder(args)`, das eine Umgebung für die Anwendung aufbaut. Innerhalb dieser Konfiguration wird die FluentValidation-Bibliothek integriert, um die Validierung von Modellen zu automatisieren. Dies geschieht durch die Hinzufügung von `AddFluentValidationAutoValidation()`, was die automatische Validierung mittels FluentValidation für jedes eingehende Modell in der Anwendung sicherstellt. Parallel dazu sorgt `AddFluentValidationClientsideAdapters()` dafür, dass diese Validierungsregeln auch auf der Clientseite angewendet werden, wodurch eine kohärente und effiziente Datenüberprüfung sowohl auf dem Server als auch im Browser erzielt wird. Diese Konfiguration ermöglicht eine robuste Überprüfung und gibt Entwicklern die Werkzeuge an die Hand, um fehlerhafte Daten frühzeitig abzufangen.


# Fehler Abfangen

POST http://localhost:5000/api/Benutzer


Body:

```` bash

{
  "name": "",
  "alter": -5
}

````

Ergebnis: Code 400    Error: Bad Request

```` bash

{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "traceId": "00-eff6bd447a5b0120d09252975cc5a53e-82e2198a5310bb6a-00",
  "errors": {
    "Name": [
      "Der Name darf nicht leer sein.",
      "Der Name muss zwischen 2 und 50 Zeichen lang sein."
    ],
    "Alter": [
      "Das Alter muss größer als 0 sein."
    ]
  }
}

```` 

Das Projekt illustriert den Einsatz einer Web-API zur Validierung von Benutzerinformationen und das Abfangen von Eingabefehlern. Mit einer einfachen API-Endpunkt zu einer ASP.NET Core-Anwendung wird dargelegt, wie externe Anfragen verarbeitet und geprüft werden. Der gezeigte POST-Request an die URL `http://localhost:5236/api/Benutzer` demonstriert, wie das Backend auf ungültige Eingaben reagiert. Im Anfragekörper werden Benutzerdaten mit einem leeren Namen und einem negativen Alter übermittelt. Die Anwendung nutzt FluentValidation, um diese Daten sofort zu validieren und Verstöße gegen die festgelegten Regeln zu erkennen.

Das Ergebnis eines solchen Anfragevorgangs ist ein HTTP-Statuscode 400, was für eine "Bad Request" steht. Daraufhin sendet die API eine ausführliche Fehlerrückmeldung, die die Art der Fehlerpräzisiert. Der JSON-Body enthält Informationen darüber, welche Validierungsfehler aufgetreten sind: Der `Name` darf nicht leer sein und muss eine bestimmte Länge haben, während `Alter` größer als Null sein muss. Diese detaillierten Fehlermeldungen helfen bei der Fehlerbehebung und verbessern die Benutzererfahrung, indem sie klare Anweisungen zur Behebung der Probleme bieten. Das gesamte Projekt verdeutlicht, wie strukturierte Eingangsprüfungen und klare Fehlerkommunikation effektiv implementiert werden können, um die Datenintegrität in Anwendungen zu gewährleisten.

