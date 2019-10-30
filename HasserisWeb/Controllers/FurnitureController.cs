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
    [Route("furnitures")]
    public class FurnituresController : Controller
    {
        [Route("all")]
        public string GetAllFurnitures()        
        {
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Furniture");
            return JsonConvert.SerializeObject((temp));
        }  
    }
}