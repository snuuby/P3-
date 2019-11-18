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
    // Is not used right now
    // Class to store the JSON data that comes from the view component.
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

    // Is not used right now
    public class TaskAssigned
    {
        public List<Employee> Employees;
        public List<Equipment> Equipment;
    }

    // Routing for the Calendar controller class fx calendar/...
    [Route("calendar")]
    public class CalendarController : Controller
    {
        public HasserisDbContext database;
        // Getting a reference to the model created by Entity Framework.
        public CalendarController(HasserisDbContext sc)
        {
            database = sc;
        }
        
        // Method to remove a task from the database.
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

            database.Tasks.RemoveRange(task);

            database.Tasks.Remove(task);
            database.SaveChanges();
            
            return "OK - removed id#" + id;
        }

        // Method to get all deliveries from the database.
        [Route("Delivery")]
        public string GetMovingTask()
        {
            var deliveryList = database.Tasks.OfType<Delivery>().Select(task => new
            {
                task,
                task.Dates,
                task.Customer,
                Employees = task.taskAssignedEmployees.Select(te => te.Employee).ToList(),
                Equipment = task.taskAssignedEquipment.Select(te => te.Equipment).ToList()
            }).ToList();

            return JsonConvert.SerializeObject(deliveryList);
        }
        
        // Method to get all movings from the database.
        [Route("Moving")]
        public string GetDeliveryTask()
        {
            var movingList = database.Tasks.OfType<Moving>().Include(f => f.Furnitures).Select(task => new
            {
                task,
                Dates = task.Dates.OrderBy(c => c.Date).ToList(),
                task.StartingAddress,
                task.Destination,
                task.Customer,
                task.Furnitures,

                Employees = task.taskAssignedEmployees.Select(te => te.Employee).ToList(),
                Equipment = task.taskAssignedEquipment.Select(te => te.Equipment).ToList()
            }).ToList();
            
            return JsonConvert.SerializeObject(movingList);
        }

        // Method to add task
        [HttpPost]
        [Route("add")]
        public string CreateTask([FromBody]dynamic json)
        {
            // Deserializing the json object received from the view.
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

            Private privateCustomer = (Private)database.Customers.FirstOrDefault(c => c.ID == 1);
            Employee employee_one = database.Employees.FirstOrDefault(e => e.ID == 1);
            Employee employee_two = database.Employees.FirstOrDefault(e => e.ID == 2);

            Delivery delivery = new Delivery(eventTitle, privateCustomer,
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2);

            database.Tasks.Add(delivery);
            database.SaveChanges();

            return "OK";
        }

        [HttpPost]
        [Route("update")]
        public string UpdateTask([FromBody]dynamic json)
        {
            // Deserializing the json object received from the view.
            dynamic eNewEvent = JsonConvert.DeserializeObject(json.ToString());

            int id = (int) eNewEvent.newEvent.id;
            string eventTitle = eNewEvent.newEvent.title;
            string eventDesc = eNewEvent.newEvent.desc;
            string eventStart = eNewEvent.newEvent.start;
            string eventEnd = eNewEvent.newEvent.end;
            
            // Den svenske virker men den danske kan ikke DateTime parse det vi får fra frontend: "12/31/2019 11:00:00" bliver til [31-12-2019 11:00:00] med svensk
            DateTime date1 = DateTime.Parse(eventStart, CultureInfo.GetCultureInfo("sv-SE"));
            date1 = date1.AddHours(-1);
            DateTime date2 = DateTime.Parse(eventEnd, CultureInfo.GetCultureInfo("sv-SE"));
            date2 = date2.AddHours(-1);
            
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
            
            return "OK";
        }
    }
}
