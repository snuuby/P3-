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
            Offer tempOffer = new Offer();
            if (tempDelivery.Offer != null)
            {
                tempOffer = database.Offers.FirstOrDefault(o => o.ID == tempDelivery.Offer.ID);
                delivery.Offer = tempOffer;
            }
            InspectionReport inspection = new InspectionReport();
            if (tempDelivery.InspectionReport != null)
            {
                inspection = database.Inspections.FirstOrDefault(i => i.ID == tempDelivery.InspectionReport.ID);
                delivery.InspectionReport = inspection;
            }
            tempDelivery = delivery;
            tempDelivery.ID = deliveryID;
            database.Update(tempDelivery);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create/delivery")]
        public void MakeDeliveryTask([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Delivery delivery = PopulateDeliveryTask(temp);
            delivery.WasInspection = false;
            delivery.WasOffer = false;
            database.Tasks.Add(delivery);
            database.SaveChanges();
        }
        [HttpPost]
        [Route("create/delivery/from/offer")]
        public void MakeDeliveryTaskFromOffer([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int offerID = temp.ID;
            Delivery tempDelivery = (Delivery)database.Tasks.FirstOrDefault(i => i.Offer.ID == offerID);
            Delivery delivery = PopulateDeliveryTask(temp);
            tempDelivery = delivery;
            tempDelivery.Phase = 3;
            Offer offer = database.Offers.FirstOrDefault(o => o.ID == offerID);
            tempDelivery.Offer = offer;
            tempDelivery.WasInspection = false;
            tempDelivery.WasOffer = true;
            database.Tasks.Update(delivery);
            database.SaveChanges();
        }

        [HttpPost]
        [Route("create/moving/from/offer")]
        public void MakeMovingTaskFromOffer([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int movingID = temp.ID;
            Moving tempMoving = (Moving)database.Tasks.Where(i => i.Offer.ID == movingID).Include(o => o.Offer).Include(i => i.InspectionReport).Single();
            Offer tempOffer = database.Offers.FirstOrDefault(o => o.ID == tempMoving.Offer.ID);

            tempMoving = PopulateMovingTask(temp, tempMoving);


            //int inspectionID = temp.InspectionReportID;
            //tempMoving.InspectionReport = database.Inspections.FirstOrDefault(i => i.ID == inspectionID);
            //Offer offer = database.Offers.FirstOrDefault(o => o.ID == offerID);
            //tempMoving.Offer = offer;



            InspectionReport inspection = new InspectionReport();
            if (tempMoving.Offer.WasInspection == true)
            {
                tempMoving.WasInspection = true;
                inspection = database.Inspections.FirstOrDefault(i => i.ID == tempMoving.InspectionReport.ID);
                tempMoving.InspectionReport = inspection;
            }
            else
            {
                tempMoving.WasInspection = false;
            }
            tempMoving.WasOffer = true;
            database.Tasks.Update(tempMoving);
            database.SaveChanges();
        }

        [HttpPost]
        [Route("create/moving/from/inspection")]
        public void MakeMovingTaskFromInspectionReport([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int inspectionID = temp.ID;
            Moving tempMoving = (Moving)database.Tasks.FirstOrDefault(i => i.InspectionReport.ID == inspectionID);
            Moving moving = PopulateMovingTask(temp, tempMoving);
            tempMoving = moving;
            tempMoving.Phase = 3;
            InspectionReport tempInspection = database.Inspections.FirstOrDefault(i => i.ID == inspectionID);
            tempMoving.InspectionReport = tempInspection;

            //int inspectionID = temp.InspectionReportID;
            //tempMoving.InspectionReport = database.Inspections.FirstOrDefault(i => i.ID == inspectionID);
            tempMoving.WasInspection = true;
            tempMoving.WasOffer = false;
            database.Tasks.Update(tempMoving);
            database.SaveChanges();
        }

        public Delivery PopulateDeliveryTask(dynamic temp)
        {
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationZIP;
            string DCity = temp.DestinationCity;


            string notes = temp.Notes;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);

            int customerID = temp.CustomerID;
            int employeeID = temp.EmployeeID;
            int carID = temp.CarID;

            string name = temp.Name;
            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee = database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar = database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            string delivery = temp.DeliveryDate;
            int lentboxes = temp.Lentboxes;
            string material = temp.Material;
            int quantity = temp.Quantity;

            DateTime deliveryDate = DateTime.Parse(delivery, CultureInfo.GetCultureInfo("sv-SE"));
            List<DateTime> tempDates = new List<DateTime>();
            tempDates.Add(deliveryDate);
            Delivery tempDelivey = new Delivery(name + "'s opgave", tempCustomer, destination, 0, tempDates, notes, "565656", material, quantity, 3);
            if (temp.ToolID != null)
            {
                int toolID = temp.ToolID;
                Tool tempTool = database.Equipment.OfType<Tool>().FirstOrDefault(tool => tool.ID == toolID);
                tempDelivey.Equipment.Add(new TaskAssignedEquipment() { Equipment = tempTool });

            }
            tempDelivey.Equipment.Add(new TaskAssignedEquipment() { Equipment = tempCar });
            tempDelivey.Employees.Add(new TaskAssignedEmployees() { Employee = tempEmployee });
            return tempDelivey;
        }
        [HttpPost]
        [Route("edit/moving")]
        public void EditMovingTask([FromBody]dynamic json) 
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int movingID = temp.ID;
            Moving tempMoving = (Moving)database.Tasks.FirstOrDefault(i => i.ID == movingID);
            Moving moving = PopulateMovingTask(temp, tempMoving);

            InspectionReport tempInspection = new InspectionReport();
            Offer tempOffer = new Offer();

            if (tempMoving.Offer != null)
            {
                tempOffer = database.Offers.FirstOrDefault(o => o.ID == tempMoving.Offer.ID);
                moving.Offer = tempOffer;
            }
            if (tempMoving.InspectionReport != null)
            {
                tempInspection = database.Inspections.FirstOrDefault(i => i.ID == tempMoving.InspectionReport.ID);
                moving.InspectionReport = tempInspection;
            }
            tempMoving = moving;
            tempMoving.ID = movingID;

            database.Update(tempMoving);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create/moving")]
        public void MakeMovingTask([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Moving moving = PopulateMovingTask(temp, new Moving());
            moving.WasInspection = false;
            moving.WasOffer = false;
            database.Tasks.Add(moving);
            database.SaveChanges();
        }
        public Moving PopulateMovingTask(dynamic temp, Moving movingtask)
        {
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationZIP;
            string DCity = temp.DestinationCity;

            string notes = temp.Notes;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);
            movingtask.StartingAddress = startingAddress;
            movingtask.Destination = destination;
            int customerID = temp.CustomerID;
            int employeeID = temp.EmployeeID;
            int carID = temp.CarID;
            
            bool withPacking;
            if (temp.WithPacking != null)
            {
                withPacking = true;
            }
            else
            {
                withPacking = false;
            }
            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee= database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar= database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            movingtask.Customer = tempCustomer;
            
            
            Tool tempTool;
            string name;
            if (tempCustomer.GetType() == typeof(Private))
            {
                Private cus = (Private)tempCustomer;
                name = cus.Firstname + ' ' + cus.Lastname;
            }
            else if (tempCustomer.GetType() == typeof(Public))
            {
                Public cus = (Public)tempCustomer;
                name = cus.Name;
            }
            else
            {
                Business cus = (Business)tempCustomer;
                name = cus.Name;
            }
            int lentBoxes = temp.Lentboxes;
            string inspection = temp.InspectionDate;
            string moving = temp.MovingDate;

            DateTime inspectionDate = DateTime.Parse(inspection, CultureInfo.GetCultureInfo("sv-SE"));
            DateTime movingDate = DateTime.Parse(moving, CultureInfo.GetCultureInfo("sv-SE"));
            DateTime endDate = movingDate.AddHours(2);
            List<DateTime> tempDates = new List<DateTime>();
            tempDates.Add(movingDate);
            tempDates.Add(endDate);
            movingtask.Name = name + "'s opgave";
            movingtask.Income = 0;
            foreach (DateTime date in tempDates)
            {
                DateTimes tempdate = new DateTimes();
                tempdate.Date = date;
                movingtask.Dates.Add(tempdate);
            }
            movingtask.Description = notes;
            movingtask.LentBoxes = lentBoxes;
            movingtask.WithPacking = withPacking;
            movingtask.Phase = 3;


            if (temp.ToolID != null)
            {
                int toolID = temp.ToolID;
                tempTool = database.Equipment.OfType<Tool>().FirstOrDefault(tool => tool.ID == toolID);
                movingtask.Equipment.Add(new TaskAssignedEquipment() { Equipment = tempTool });
            }
            movingtask.Equipment.Add(new TaskAssignedEquipment() { Equipment = tempCar });
            movingtask.Employees.Add(new TaskAssignedEmployees() { Employee = tempEmployee });


            return movingtask;
        }
        [HttpGet]
        [Route("{id}")]
        public string GetTask(int id)
        {
            dynamic tempMov = database.Tasks.OfType<Moving>().Include(e => e.Employees).Include(c => c.Equipment).Include(c => c.Customer).ThenInclude(c => c.ContactInfo).Include(o => o.Offer)
            .Include(d => d.Destination).Include(s => s.StartingAddress).Where(t => t.Phase == 3).Include(d => d.Dates).FirstOrDefault(t => t.ID == id);

            dynamic tempDeliv = database.Tasks.OfType<Moving>().Include(e => e.Employees).Include(c => c.Equipment).Include(c => c.Customer).ThenInclude(c => c.ContactInfo).Include(o => o.Offer)
.Include(d => d.Destination).Include(s => s.StartingAddress).Where(t => t.Phase == 3).Include(d => d.Dates).FirstOrDefault(t => t.ID == id);
            if (tempMov != null)
            {
                JObject temp = JObject.FromObject(tempMov);
                if (tempMov.GetType() == typeof(Moving))
                {
                    temp.Add("TaskType", "Moving");
                }
                else
                {
                    temp.Add("TaskType", "Delivery");

                }
                return temp.ToString();
            }
            else
            {
                JObject temp = JObject.FromObject(tempDeliv);
                if (tempDeliv.GetType() == typeof(Moving))
                {
                    temp.Add("TaskType", "Moving");
                }
                else
                {
                    temp.Add("TaskType", "Delivery");

                }
                return temp.ToString();
            }
            
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
