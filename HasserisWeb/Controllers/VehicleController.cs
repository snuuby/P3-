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
    [Route("Vehicles")]
    public class VehicleController : Controller
    {

        [Route("all")]
        public string GetAllTools()        
        {
<<<<<<< Updated upstream
<<<<<<< Updated upstream
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Equipment");
            var TempVehicleList = new List<Equipment>();
            foreach (var element in temp)
            {
                if (element.type == "Vehicle")
                {
                    TempVehicleList.Add(element);
                }
            }
            return JsonConvert.SerializeObject(TempVehicleList);
        }  
        
=======
=======
>>>>>>> Stashed changes
                return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>().ToList());
        }

        [Route("{id}")]
        public string GetSpecificVehicle(int id)
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Vehicle>()
                .FirstOrDefault(c => c.ID == id));
        }
<<<<<<< Updated upstream
>>>>>>> Stashed changes
=======
>>>>>>> Stashed changes
    }
}