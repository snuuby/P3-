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
    [Route("Tools")]
    public class ToolController : Controller
    {

        [Route("all")]
        public string GetAllTools()        
        {
<<<<<<< Updated upstream
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Equipment");
            var TempToolList = new List<Equipment>();
            foreach (var element in temp)
            {
                if (element.type == "Tool")
                {
                    TempToolList.Add(element);
                }
            }
            return JsonConvert.SerializeObject(TempToolList);
        }  
        
=======

            return JsonConvert.SerializeObject(database.Equipment.OfType<Tool>().ToList());

        }

        [Route("{id}")]
        public string GetSpecificTool(int id)
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Tool>()
                .FirstOrDefault(c => c.ID == id));
        }

>>>>>>> Stashed changes
    }
}