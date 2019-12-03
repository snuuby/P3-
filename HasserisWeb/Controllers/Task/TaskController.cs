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

namespace HasserisWeb
{
    [Route("task")]
    public class TaskController : Controller
    {

        HasserisDbContext database;
        public TaskController(HasserisDbContext database) 
        {
            this.database = database;
        }
        [HttpPost]
        [Route("edit/delivery")]
        public void EditDeliveryTask([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int deliveryID = temp.ID;
            Delivery delivery = PopulateDeliveryTask(temp);

            Delivery tempDelivery = (Delivery)database.Tasks.FirstOrDefault(i => i.ID == deliveryID);
            tempDelivery = delivery;
            database.Update(tempDelivery);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create/delivery")]
        public void MakeDeliveryTask([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Delivery delivery = PopulateDeliveryTask(temp);
            database.Tasks.Add(delivery);
            database.SaveChanges();
        }
        public Delivery PopulateDeliveryTask(dynamic temp)
        {
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //destination
            string Daddress = temp.StartAddress;
            string DZIP = temp.StartZIP;
            string DCity = temp.StartCity;

            string notes = temp.Notes;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);

            int customerID = temp.Customer.ID;
            int employeeID = temp.Employee.ID;
            int carID = temp.Car.ID;
            string name = temp.Name;
            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee = database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar = database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            string delivery = temp.DeliveryDate;
            double income = temp.Income;
            int lentboxes = temp.LentBoxes;
            string material = temp.Material;
            int quantity = temp.Quantity;

            DateTime deliveryDate = DateTime.Parse(delivery, CultureInfo.GetCultureInfo("sv-SE"));
            List<DateTime> tempDates = new List<DateTime>();
            tempDates.Add(deliveryDate);
            return new Delivery(name, tempCustomer, destination, income, tempDates, notes, "565656", material, quantity, 3);
        }
        [HttpPost]
        [Route("edit/moving")]
        public void EditMovingTask([FromBody]dynamic json) 
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int movingID = temp.ID;
            Moving moving = PopulateMovingTask(temp);

            Moving tempMoving = (Moving)database.Tasks.FirstOrDefault(i => i.ID == movingID);
            tempMoving = moving;
            database.Update(tempMoving);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create/moving")]
        public void MakeMovingTask([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Moving moving = PopulateMovingTask(temp);
            database.Tasks.Add(moving);
            database.SaveChanges();
        }
        public Moving PopulateMovingTask(dynamic temp)
        {
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //destination
            string Daddress = temp.StartAddress;
            string DZIP = temp.StartZIP;
            string DCity = temp.StartCity;

            string notes = temp.Notes;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);

            int customerID = temp.Customer.ID;
            int employeeID = temp.Employee.ID;
            int carID = temp.Car.ID;
            string name = temp.Name;
            bool withPacking = temp.WithPacking;
            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee= database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar= database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            string inspection = temp.InspectionDate;
            string moving = temp.MovingDate;
            double income = temp.Income;
            int lentboxes = temp.LentBoxes;
            
            DateTime inspectionDate = DateTime.Parse(inspection, CultureInfo.GetCultureInfo("sv-SE"));
            DateTime movingDate = DateTime.Parse(moving, CultureInfo.GetCultureInfo("sv-SE"));
            List<DateTime> tempDates = new List<DateTime>();
            tempDates.Add(movingDate);
            return new Moving(name, tempCustomer, destination, income,  tempDates, notes, "606060", startingAddress, lentboxes, withPacking, 3);
        }
        [HttpGet]
        [Route("{id}")]
        public string GetTask(int id)
        {
            dynamic tempTask = database.Tasks.OfType<Moving>()
            .Include(e => e.Employees).Include(c => c.Equipment).Include(c => c.Customer).ThenInclude(c => c.ContactInfo)
            .Include(d => d.Destination).Include(s => s.StartingAddress).Where(t => t.Phase == 3).Single();
            JObject temp = JObject.FromObject(tempTask);
            if (tempTask.GetType() == typeof(Moving))
            {
                temp.Add("TaskType", "Moving");
            }
            else
            {
                temp.Add("TaskType", "Delivery");

            }
            return temp.ToString();
        }

        
        [HttpGet]
        [Route("get/moving")]
        public string GetMovingTasks()
        {
            List<Moving> moving = database.Tasks.OfType<Moving>()
            .Include(e => e.Employees).Include(c => c.Equipment).Include(c => c.Customer).ThenInclude(c => c.ContactInfo)
            .Include(d => d.Destination).Include(s => s.StartingAddress)
            .Where(t => t.Phase == 3).ToList();
            return JsonConvert.SerializeObject(moving);

        }
        [HttpGet]
        [Route("get/delivery")]
        public string GetDeliveryTasks()
        {

            List<Delivery> deliveries = database.Tasks.OfType<Delivery>()
            .Include(e => e.Employees).Include(c => c.Equipment).Include(c => c.Customer).ThenInclude(c => c.ContactInfo)
            .Include(d => d.Destination).Where(t => t.Phase == 3).ToList();
            return JsonConvert.SerializeObject(deliveries);

        }
    }
}
