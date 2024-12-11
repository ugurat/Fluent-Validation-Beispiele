using Bsp02.Models;
using Bsp02.Validators;
using Microsoft.AspNetCore.Mvc;

namespace Bsp02.Controllers
{
    public class BenutzerController : Controller
    {

        static List<Benutzer> benutzerListe = null;

        public BenutzerController()
        {
            if(benutzerListe == null)
            {
                // Beispiel-Daten (In der echten Anwendung mit Datenbank verbunden)
                benutzerListe = new List<Benutzer>
                {
                    new Benutzer { Id = 1, Name = "Max Muster", Alter = 25 },
                    new Benutzer { Id = 2, Name = "Anna Beispiel", Alter = 30 }
                };

            }
        }

        // GET: Benutzer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Benutzer/Create
        [HttpPost]
        [ValidateAntiForgeryToken] // <--- EINTRAGEN
        public IActionResult Create(Benutzer benutzer)
        {
            // Validierungslogik
            var validator = new BenutzerValidator();
            var result = validator.Validate(benutzer);

            if (!result.IsValid)
            {
                // Fehler in die ModelState übernehmen
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(benutzer);
            }

            // Erfolgsmeldung oder Weiterleitung
            TempData["Success"] = "Benutzer erfolgreich erstellt!";
            return RedirectToAction("Index");
        }

        // GET: Benutzer/Index
        public IActionResult Index()
        {

            return View(benutzerListe);
        }


        // GET: Benutzer/Edit/{id}
        public IActionResult Edit(int id)
        {
            var benutzer = benutzerListe.FirstOrDefault(b => b.Id == id);
            if (benutzer == null)
            {
                return NotFound();
            }
            return View(benutzer);
        }

        // POST: Benutzer/Edit/{id}
        [HttpPost]
        [ValidateAntiForgeryToken] // <--- EINTRAGEN
        public IActionResult Edit(Benutzer benutzer)
        {
            var validator = new BenutzerValidator();
            var result = validator.Validate(benutzer);

            if (!result.IsValid)
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
                return View(benutzer);
            }

            var existingBenutzer = benutzerListe.FirstOrDefault(b => b.Id == benutzer.Id);
            if (existingBenutzer != null)
            {
                existingBenutzer.Name = benutzer.Name;
                existingBenutzer.Alter = benutzer.Alter;
            }

            TempData["Success"] = "Benutzer erfolgreich bearbeitet!";
            return RedirectToAction("Index");
        }

        // GET: Benutzer/Details/{id}
        public IActionResult Details(int id)
        {
            var benutzer = benutzerListe.FirstOrDefault(b => b.Id == id);
            if (benutzer == null)
            {
                return NotFound();
            }
            return View(benutzer);
        }


        public IActionResult Delete(int id)
        {
            var benutzer = benutzerListe.FirstOrDefault(b => b.Id == id);
            if (benutzer == null)
            {
                return NotFound();
            }
            return View(benutzer);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken] // <--- EINTRAGEN
        public IActionResult DeleteConfirmed(int id)
        {
            var benutzer = benutzerListe.FirstOrDefault(b => b.Id == id);
            if (benutzer != null)
            {
                benutzerListe.Remove(benutzer);
            }
            TempData["Success"] = "Benutzer erfolgreich geloescht!";
            return RedirectToAction("Index");
        }


    }
}
