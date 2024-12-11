using Bsp01.Models;
using Bsp01.Validators;

namespace Bsp01
{
    internal class Program
    {
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
    }
}
