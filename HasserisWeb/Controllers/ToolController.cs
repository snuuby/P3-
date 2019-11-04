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
    [Route("Tool")]
    public class ToolController : Controller
    {
        public HasserisDbContext database;
        public ToolController(HasserisDbContext sc)
        {
            database = sc;
        }
        [Route("all")]
        public string GetAllTools()        
        {

                var equipment = database.Equipment.ToList();

                var TempToolList = new List<Equipment>();
                foreach (var element in equipment)
                {
                    if (element.Type == "Tool")
                    {
                        TempToolList.Add(element);
                    }
                }
                return JsonConvert.SerializeObject(TempToolList);
            

        }  
        
    }
}