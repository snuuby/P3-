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

        [Route("all")]
        public string GetAllTools()        
        {
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
        
    }
}