using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace HasserisWeb.Controllers
{
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

            var task = database.Tasks.Include(t => t.Dates).
                Include(t => t.PauseTimes).
                Include(t => t.Employees).
                Include(t => t.Equipment).FirstOrDefault(t => t.ID == id);

            database.Tasks.RemoveRange(task);

            database.Tasks.Remove(task);
            database.SaveChanges();

            return "OK - removed id#" + id;
        }

        // Method to get all deliveries from the database.
        [Route("Delivery")]
        public string GetDeliveryTasks()
        {
            return JsonConvert.SerializeObject(database.Tasks.OfType<Delivery>().Where(t => t.Phase == 3)
            .Include(i => i.InspectionReport).Include(o => o.Offer).Include(c => c.Customer)
            .Include(a => a.Destination).Include(e => e.Equipment).Include(e => e.Employees)
            .Include(d => d.Dates).Include(p => p.PauseTimes).ToList());
        }

        // Method to get all movings from the database.
        [Route("Moving")]
        public string GetMovingTasks()
        {

            return JsonConvert.SerializeObject(database.Tasks.OfType<Moving>().Where(t => t.Phase == 3)
            .Include(i => i.InspectionReport).Include(o => o.Offer).Include(c => c.Customer)
            .Include(a => a.Destination).Include(e => e.Equipment).Include(e => e.Employees)
            .Include(d => d.Dates).Include(p => p.PauseTimes).Include(f => f.Furnitures).ToList());
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

            /* When parsing the DateTime from the view component, we use the swedish culture
             because it is able convert the '/' to a DateTime object.
             And this adds 1 hour to our current timezone (DK), and we subtract 1 hour.
            */
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
                new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2, 3);

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

            int id = (int)eNewEvent.newEvent.id;
            string eventTitle = eNewEvent.newEvent.title;
            string eventDesc = eNewEvent.newEvent.desc;
            string eventStart = eNewEvent.newEvent.start;
            string eventEnd = eNewEvent.newEvent.end;

            /* When parsing the DateTime from the view component, we use the swedish culture
             because it is able convert the '/' to a DateTime object.
             And this adds 1 hour to our current timezone (DK), and we subtract 1 hour.
            */
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
            new Address("myrdal", "2", "aalborg", "test"), 1000, dates, eventDesc, "22331133", "Foam", 2, 3);

            database.Tasks.Update(delivery);
            database.SaveChanges();

            return "OK";
        }
    }
}
