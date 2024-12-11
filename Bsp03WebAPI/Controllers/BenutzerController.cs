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
