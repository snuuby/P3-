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
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;

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
        public string photoURL { get; set; }
        public string combo { get; set; }
    }

    public class TaskAssigned
    {
        public List<Employee> Employees;
        public List<Equipment> Equipment;
    }
    
    [Route("calendar")]
    public class CalendarController : Controller
    {
        public HasserisDbContext database;
        public CalendarController(HasserisDbContext sc)
        {
            database = sc;
        }
        [HttpPost]
        [Route("remove")]
        public string RemoveTask([FromBody]dynamic json)
        {
            dynamic deserializeObject = JsonConvert.DeserializeObject(json.ToString());

            int id = deserializeObject.eventId;

                var task = database.Tasks.Include(dates => dates.Dates).
                    Include(pauses => pauses.PauseTimes).
                    Include(employees => employees.taskAssignedEmployees).
                    Include(equipment => equipment.taskAssignedEquipment).FirstOrDefault(e => e.ID == id);


            database.Tasks.Remove(task);
            database.SaveChanges();
            

            return "andreas: " + id;
        }
        
        [Route("all")]
        public string GetAllTasks()
        {
            
            var movingList = database.Tasks.OfType<Moving>().Select(task => new
            {
                task,
                task.Dates,
                StartingAddress = ((Moving)task).StartingAddress,
                Destination = ((Moving)task).Destination,

                Employees = task.taskAssignedEmployees.Select(te => te.Employee).ToList(),
                Equipment = task.taskAssignedEquipment.Select(te => te.Equipment).ToList()
            }).ToList();
    
            var deliveryList = database.Tasks.OfType<Delivery>().Select(task => new
            {
                task,
                task.Dates,
                Employees = task.taskAssignedEmployees.Select(te => te.Employee).ToList(),
                Equipment = task.taskAssignedEquipment.Select(te => te.Equipment).ToList()
            }).ToList();


            var jMoving = JsonConvert.SerializeObject(movingList);
            var jDelivery = JsonConvert.SerializeObject(deliveryList);


//            var arrayOfObjects = JsonConvert.SerializeObject(
//                new[] {JsonConvert.DeserializeObject(jMoving), JsonConvert.DeserializeObject(jDelivery)}
//            );
//                
            return arrayOfObjects;
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
       
            DateTime date1 = DateTime.Parse(eventStart, CultureInfo.GetCultureInfo("sv-SE"));
            date1 = date1.AddHours(-1);
            DateTime date2 = DateTime.Parse(eventEnd, CultureInfo.GetCultureInfo("sv-SE"));
            date2 = date2.AddHours(-1);

            
            List<DateTime> dates = new List<DateTime>();
            dates.Add(date1);
            dates.Add(date2);
            
            
            
            
            //dynamic tempCustomer = database.Customers.FirstOrDefault(c => c.ID == eNewEvent.Customer.ID);
            Private privateCustomer = (Private)database.Customers.FirstOrDefault(c => c.ID == 1);
            Employee employee_one = database.Employees.FirstOrDefault(e => e.ID == 1);
            Employee employee_two = database.Employees.FirstOrDefault(e => e.ID == 2);

            Delivery delivery = new Delivery(eventTitle, privateCustomer,
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2);

            database.Tasks.Add(delivery);
            database.SaveChanges();
            
            return "asdqwe";

        }

        [HttpPost]
        [Route("update")]
        public string UpdateTask([FromBody]dynamic json)
        {
//            // 2019-11-03 00:00:00
//            var settings = new JsonSerializerSettings
//            {
//                DateFormatString = "yyyy-MM-ddTH:mm:ss.fff2",
//                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind
//            };
//            
//            var eDateTimes = JsonConvert.DeserializeObject(json.Dates.ToString(), );
            
            dynamic eNewEvent = JsonConvert.DeserializeObject(json.ToString());

            
            int id = (int) eNewEvent.newEvent.id;
            string eventTitle = eNewEvent.newEvent.title;
            string eventDesc = eNewEvent.newEvent.desc;
            string eventStart = eNewEvent.newEvent.start;
            string eventEnd = eNewEvent.newEvent.end;

            //eventStart = eventStart.Remove(eventStart.Length - 3);
            //eventEnd = eventEnd.Remove(eventEnd.Length - 3);

            
            // Den svenske virker men den danske kan ikke DateTime parse det vi får fra frontend: "12/31/2019 11:00:00" bliver til [31-12-2019 11:00:00] med svensk  
            DateTime date1 = DateTime.Parse(eventStart, CultureInfo.GetCultureInfo("sv-SE"));
            date1 = date1.AddHours(-1);
            DateTime date2 = DateTime.Parse(eventEnd, CultureInfo.GetCultureInfo("sv-SE"));
            date2 = date2.AddHours(-1);

            // CultureInfo dk = new CultureInfo("da-DK");
            //DateTime date1 = DateTime.ParseExact(eventStart, "yyyy-MM-dd HH:mm", dk);
            //DateTime date2 = DateTime.ParseExact(eventStart, "dd-MM-yyyy HH:mm:ss", dk);
            // 03-12-2019 00:00:00/05-12-2019 00:00:00
            // HH er 24 hours


                List<DateTime> dates = new List<DateTime>();
                dates.Add(date1);
                dates.Add(date2);

                Private privateCustomer = (Private)database.Customers.FirstOrDefault(c => c.ID == 1);
                Employee employee_one = database.Employees.FirstOrDefault(e => e.ID == 1);
                Employee employee_two = database.Employees.FirstOrDefault(e => e.ID == 2);

                Delivery delivery = new Delivery(eventTitle, privateCustomer,
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2);


            database.Tasks.Update(delivery);
            database.SaveChanges();
            

            return "asd";
        }


    }
}