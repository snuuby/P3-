using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace HasserisWeb.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MitComponentController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<DateTime> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new DateTime
                {
                })
                .ToArray();
            
        }
    }
}