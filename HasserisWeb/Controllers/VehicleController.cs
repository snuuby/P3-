using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace HasserisWeb.Controllers
{
    [Route("Vehicle")]
    public class VehicleController : Controller
    {
        public HasserisDbContext database;
        public VehicleController(HasserisDbContext sc)
        {
            database = sc;
        }
        [Route("all")]
        public string GetAllVehicles()        
        {
                return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>().ToList());
        }  

        
    }
}