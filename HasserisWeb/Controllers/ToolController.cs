using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;

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
        [Route("available")]
        public string GetAvailableTools()
        {
            return JsonConvert.SerializeObject(database.Equipment.OfType<Tool>().
                                            Where(tool => tool.IsAvailable).ToList());
        }
        [Route("add")]
        public void CreateTool([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Tool tool = new Tool();
            tool.Name = temp.Name;
            string available = temp.Available;
            if (available == "Yes")
            {
                tool.IsAvailable = true;
            }
            else
            {
                tool.IsAvailable = false;
            }
            database.Equipment.Add(tool);
            database.SaveChanges();


        }
        [Route("edit")]
        public void EditTool([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Tool tool = (Tool)database.Equipment.FirstOrDefault(t => t.ID == id);

            tool.Name = temp.Name;

            string available = temp.Available;
            if (available == "Yes")
            {
                tool.IsAvailable = true;
            }
            else
            {
                tool.IsAvailable = false;
            }
            database.Equipment.Update(tool);
            database.SaveChanges();


        }
    }
}
