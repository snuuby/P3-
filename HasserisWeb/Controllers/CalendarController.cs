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
    [Route("calendar")]
    public class CalendarController : Controller
    {
        [Route("all")]
        public string GetAllEvents()        
        {
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Tasks");
            return JsonConvert.SerializeObject((temp));
        }  
    }
}