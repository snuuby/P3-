using System;
using System.Collections.Generic;
using System.Globalization;
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
    public class NewEvent
    {
        public string id { get; set; }
        public string title { get; set; }
        public bool allDay { get; set; }
        public object employees { get; set; }
        public string start { get; set; }
        public string end { get; set; }
        public string desc { get; set; }
        public string combo { get; set; }
    }
    
    
    [Route("calendar")]
    public class CalendarController : Controller
    {
        [HttpPost]
        [Route("remove")]
        public string RemoveTask([FromBody]dynamic json)
        {
            dynamic deserializeObject = JsonConvert.DeserializeObject(json.ToString());

            int id = deserializeObject.eventId;
            
            HasserisDbContext.DeleteElementFromDatabase<Task>("Task", id);

            return "andreas: " + id;
        }
        
        [Route("all")]
        public string GetAllTasks()        
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
        public string CreateTask([FromBody]dynamic json)
        {
            // {"newEvent":{"id":"88a1297a","title":"ewr","allDay":true,"employees":null,"start":"12/11/2019, 12:00:00 AM","end":"12/11/2019, 12:00:00 AM","desc":"wer","combo":""}}
            dynamic eNewEvent = JsonConvert.DeserializeObject(json.ToString());
            string eventTitle = eNewEvent.newEvent.title;
            string eventDesc = eNewEvent.newEvent.desc;
            string eventStart = eNewEvent.newEvent.start;
            string eventEnd = eNewEvent.newEvent.end;
       
            DateTime date1 = DateTime.Parse(eventStart);
            DateTime date2 = DateTime.Parse(eventEnd);
            
            List<DateTime> dates = new List<DateTime>();
            dates.Add(date1);
            dates.Add(date2);

            
            Private privateCustomer = HasserisDbContext.LoadElementFromDatabase("Private", 1);
            Employee employee_one = HasserisDbContext.LoadElementFromDatabase("Employee", 1);
            Employee employee_two = HasserisDbContext.LoadElementFromDatabase("Employee", 2);
            
            Vehicle vehicle = new Vehicle("Stor bil", "Vehicle", "Opel", "12312123");
            HasserisDbContext.SaveElementToDatabase<Vehicle>(vehicle);

            Delivery delivery = new Delivery(eventTitle, "Delivery", privateCustomer,
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2);
            //delivery.AddElementToTask(employee_one);
            //delivery.AddElementToTask(employee_two);
            //delivery.AddElementToTask(vehicle);
            HasserisDbContext.SaveElementToDatabase<Delivery>(delivery);
            return "din mor";

        }

        [HttpPost]
        [Route("update")]
        public string UpdateTask([FromBody]dynamic json)
        {

            dynamic eNewEvent = JsonConvert.DeserializeObject(json.ToString());

            string eventTitle = eNewEvent.newEvent.title;
            string eventDesc = eNewEvent.newEvent.desc;
            string eventStart = eNewEvent.newEvent.start;
            string eventEnd = eNewEvent.newEvent.end;

            //eventStart = eventStart.Remove(eventStart.Length - 3);
            //eventEnd = eventEnd.Remove(eventEnd.Length - 3);

            
            DateTime date1 = DateTime.Parse(eventStart);
            DateTime date2 = DateTime.Parse(eventEnd);
            
            
            // CultureInfo dk = new CultureInfo("da-DK");
            //DateTime date1 = DateTime.ParseExact(eventStart, "yyyy-MM-dd HH:mm", dk);
            //DateTime date2 = DateTime.ParseExact(eventStart, "dd-MM-yyyy HH:mm:ss", dk);
            // 03-12-2019 00:00:00/05-12-2019 00:00:00
            // HH er 24 hours

            
            List<DateTime> dates = new List<DateTime>();
            dates.Add(date1);
            dates.Add(date2);
            
            Private privateCustomer = HasserisDbContext.LoadElementFromDatabase("Private", 1);
            Employee employee_one = HasserisDbContext.LoadElementFromDatabase("Employee", 1);
            Employee employee_two = HasserisDbContext.LoadElementFromDatabase("Employee", 2);

            
            Delivery delivery = new Delivery(eventTitle, "Delivery", privateCustomer,
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2);
            
            HasserisDbContext.UpdateElementInDatabase<Task>(delivery);
            return "hej";
        }


    }
}