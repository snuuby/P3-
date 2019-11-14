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
        public HasserisDbContext database;
        public ToolController(HasserisDbContext sc)
        {
            database = sc;
        }

        [Route("all")]
        public string GetAllTools()
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Tool>().ToList());
        }

        [Route("{id}")]
        public string GetSpecificTool(int id)
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Tool>()
                .FirstOrDefault(c => c.ID == id));
        }

        [Route("add")]
        public string CreateTool([FromBody]dynamic json)
        {
            dynamic eNewTool = JsonConvert.DeserializeObject(json.ToString());
            Tool tool = new Tool(eNewTool.name);

            database.Equipment.Add(tool);
            database.SaveChanges();

            return "abc";

        }
    }
}
