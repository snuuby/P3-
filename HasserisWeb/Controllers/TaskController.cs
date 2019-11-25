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
    [Route("Task")]
    public class TaskController : Controller
    {
        HasserisDbContext database;
        public TaskController(HasserisDbContext database) 
        {
            this.database = database;
        }
        [HttpPost]
        [Route("MakeInspectionReport")]
        public void MakeInspectionReport([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int customerID = temp.Customer.ID;
            int employeeID = temp.Employee.ID;
            int carID = temp.Car.ID;
            string address = temp.Address;
            string ZIP = temp.ZIP;
            string City = temp.City;
            string AddressNote = temp.AddressNote;
            string name = temp.Name;
            string notes = temp.Notes;
            Address startingAddress = new Address(address, ZIP, City, AddressNote);
            Customer tempCustomer= database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee= database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar= database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            string start = temp.Start;
            DateTime date1 = DateTime.Parse(start, CultureInfo.GetCultureInfo("sv-SE"));
            date1 = date1.AddHours(-1);

            InspectionReport inspection = new InspectionReport(tempCustomer, name, startingAddress, tempEmployee, tempCar, notes, date1 );
            Moving moving = new Moving();
            moving.Phase = 1;
            moving.InspectionReport = inspection;
            database.Tasks.Add(moving);
            database.SaveChanges();
        }

        [HttpGet]
        [Route("{id}")]
        public string GetInspectionReport(int id)
        { 
            InspectionReport tempReport = database.Inspections.FirstOrDefault(i => i.ID == id);
            return JsonConvert.SerializeObject(tempReport);
        }
        [HttpGet]
        [Route("getAllInspectionReports")]
        public string GetAllInspectionReports()
        {

            List<InspectionReport> tempReports = database.Inspections.ToList();
            return JsonConvert.SerializeObject(tempReports);
        }
    }
}
