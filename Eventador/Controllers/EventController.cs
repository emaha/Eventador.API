using Microsoft.AspNetCore.Mvc;
using System;

namespace Eventador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EventController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        [HttpGet]
        public IActionResult Get()
        {
            var txt = Summaries[new Random().Next(Summaries.Length)];
            return Ok(txt);
        }
    }
}