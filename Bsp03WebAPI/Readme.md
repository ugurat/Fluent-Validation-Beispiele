

# FluentValidation

Das Projekt zeigt die Anwendung von FluentValidation in einer ASP.NET Core Web API zur Validierung von Benutzerdaten. Die Struktur umfasst `Controllers` für HTTP-Anfragen, `Models` mit der Benutzerdefinition, und `Validators` für die Validierungsregeln. Die `Program.cs` konfiguriert den Webserver und integriert FluentValidation für server- und clientseitige Prüfungen. Bei ungültigen Daten, wie leerem Namen oder negativem Alter, liefert das System einen HTTP-Statuscode 400 und detaillierte Fehlermeldungen, die den Nutzern bei der Datenkorrektur helfen. Diese Implementierung sichert eine konsistente und robuste Datenverarbeitung.

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

Die Projektstruktur von Bsp03 ist so aufgebaut, dass jede Komponente eine klare Rolle in der gesamten Logik der Anwendung einnimmt. Der `BenutzerController.cs` im `Controllers`-Verzeichnis verarbeitet HTTP-Anfragen und ermöglicht die Kommunikation mit Clients. Im `Models`-Ordner definiert `Benutzer.cs` die Datenstruktur für Benutzerobjekte, was die Grundlage für die Datenverarbeitung bildet. Der `Validators`-Ordner enthält `BenutzerValidator.cs`, welcher die Validierungsregeln festlegt und dadurch die Datenintegrität sicherstellt. Die `Program.cs` am Wurzelverzeichnis ist der Ausgangspunkt der Anwendung und koordiniert die Konfiguration und den Start des gesamten Systems, indem sie die verschiedenen Module und Dienste miteinander verbindet.


## Nuget Packete

 - FluentValidation https://www.nuget.org/packages/FluentValidation.AspNetCore/11.3.0

```` bash

Install-Package FluentValidation.AspNetCore -Version 11.3.0

````


---

## Models / Benutzer.cs

```` csharp

namespace Bsp01.Models
{
    internal class Benutzer
    {
        public int Id { get; set; }
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

Der `BenutzerValidator` wird durch Erben von `AbstractValidator<Benutzer>` konzipiert, um die Eingaben eines `Benutzer`-Objekts zu prüfen. Zunächst widmen wir uns der `Name`-Eigenschaft, die durch `RuleFor` als nicht leer und mit einer Länge zwischen 2 und 50 Zeichen definiert wird. Sollte dieser Anspruch nicht erfüllt sein, liefert die validierende Regel eine klare Fehlermeldung zurück. Ebenso betrachten wir die `Alter`-Eigenschaft. Sie muss gemäß den Vorschriften größer als 0 und maximal 120 sein. Auch hier sind spezifische Fehlermeldungen vorgesehen, um dem Benutzer im Falle eines Verstoßes präzise Rückmeldungen zu geben. Diese schrittweise Validierung stellt sicher, dass die Benutzerdaten korrekt und innerhalb der erwarteten Parameter liegen, bevor sie in weiterführenden Prozessen verwendet werden.


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

Der `BenutzerController` ist Teil einer ASP.NET Core Web API und dient der Handhabung von HTTP-Anfragen. Zur Konfiguration als API-Endpunkt wird das `[Route("api/[controller]")]`-Attribut verwendet, wodurch der Controller Anfragen an die URL "api/benutzer" entgegennehmen kann. Das `[ApiController]`-Attribut gewährleistet, dass der Controller spezifische Features für APIs nutzt, etwa die automatische Validierung. Innerhalb des Controllers definiert die `ErstelleBenutzer`-Methode, eine `POST`-Operation, wie eingehende Daten verarbeitet werden. Das `[FromBody]`-Attribute zeigt an, dass ein `Benutzer`-Objekt direkt aus dem Anforderungskörper entnommen und als Parameter übergeben wird. Nach der Übernahme der Daten erzeugt die Methode eine `Ok`-Antwort, die nicht nur eine Bestätigungsmeldung, sondern auch das übertragene Benutzerobjekt enthält. So wird ein nahtloser Ablauf vom Empfang bis zur Bestätigung ermöglicht, während die API den Zustand der Daten und den erfolgreichen Vorgang dokumentiert.

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

In der `Program.cs`, welche den Einstiegspunkt der ASP.NET Core-Anwendung darstellt, wird die Umgebung inklusive Webserver initialisiert. Mit `WebApplication.CreateBuilder(args)` wird zunächst eine grundlegende Konfiguration aufgesetzt, die alle notwendigen Dienste der Anwendung einbezieht. Damit FluentValidation zur automatisierten Überprüfung von Daten genutzt werden kann, fügen wir zwei entscheidende Konfigurationen hinzu. Die Methode `AddFluentValidationAutoValidation()` integriert FluentValidation so, dass eingehende Modelle während Anfragen serverseitig automatisch validiert werden. Parallel dazu sorgt `AddFluentValidationClientsideAdapters()` dafür, dass die Validierungsregeln nicht nur auf dem Server, sondern auch im Client-Code zur Anwendung kommen. Diese doppelte Validierungsebene gewährleistet, dass inkorrekte Daten sowohl vor dem Absenden als auch beim Empfang abgefangen werden, wodurch ein konsistentes und robustes Datenmanagement etabliert wird.


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

Der POST-Anfrage an den Endpunkt "http://localhost:5000/api/Benutzer" mit einem leeren Namen und einem negativen Alter wird mit einem HTTP-Statuscode 400 und einer Fehlermeldung "Bad Request" beantwortet. 
Der Benutzer-Validator prüft die Eingabedaten gegen festgelegte Regeln. Da der Name nicht leer sein darf und eine Länge zwischen 2 und 50 Zeichen haben muss und das Alter größer als 0 sein sollte, schlägt die Validierung fehl. 
Das Ergebnis umfasst spezifische Fehlermeldungen für jede nicht erfüllte Regel, was dem Nutzer hilft, die exakten Anpassungen der Daten vorzunehmen, um den Validierungsanforderungen zu entsprechen.

