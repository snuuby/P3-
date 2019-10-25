using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text.Json;
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
            dynamic temp = HasserisDbContext.LoadAllElementsFromDatabase("Task");
            return JsonConvert.SerializeObject((temp));
        }  
        
        /*
        [HttpPost]
        [Route("add")]
        public JsonResult AddEvent()
        {
            return Json("asd");
        } 
        */ 

        [HttpPost]
        [Route("add")]
        public JsonElement CreateTask([FromBody]dynamic task)
        {
            /*
id	"b87898f8"
title	"asd"
allDay	true
employees	null
start	"12/6/2019, 12:00:00 AM"
end	"12/6/2019, 12:00:00 AM"
desc	"asd"
combo	""

             */
            dynamic tempstring = JsonConvert.DeserializeObject(task.ToString());

            Private privateCustomer = HasserisDbContext.LoadElementFromDatabase("Private", 1);
            Employee employee_one = HasserisDbContext.LoadElementFromDatabase("Employee", 1);
            Employee employee_two = HasserisDbContext.LoadElementFromDatabase("Employee", 2);
            
            Vehicle vehicle = new Vehicle("Stor bil", "Vehicle", "Opel", "12312123");
            HasserisDbContext.SaveElementToDatabase<Vehicle>(vehicle);

            Delivery delivery = new Delivery(tempstring.title, "Delivery", privateCustomer,
                new Address("myrdal", "2", "aalborg", tempstring.desc), 1000, new List<DateTime>() { new DateTime(2019, 12, 3), new DateTime(2019, 12, 5) }, "testnote", "22331133", "Foam", 2);
            //delivery.AddElementToTask(employee_one);
            //delivery.AddElementToTask(employee_two);
            //delivery.AddElementToTask(vehicle);
            HasserisDbContext.SaveElementToDatabase<Delivery>(delivery);
            return task;

        }
        
        

    }
}