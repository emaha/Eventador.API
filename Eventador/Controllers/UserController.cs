﻿using Microsoft.AspNetCore.Mvc;

namespace Eventador.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("ASD");
        }
    }
}
