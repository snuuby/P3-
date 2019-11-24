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
            Address startingAddress = new Address(address, ZIP, City, AddressNote);
            Customer tempCustomer= database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee= database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar= database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);

            DateTime date1 = DateTime.Parse(temp.Start, CultureInfo.GetCultureInfo("sv-SE"));
            date1 = date1.AddHours(-1);

            InspectionReport inspection = new InspectionReport(tempCustomer, temp.Name, startingAddress, tempEmployee, tempCar, temp.Notes, date1 );

            List<DateTime> testList_two = new List<DateTime>() { new DateTime(2019, 11, 07), new DateTime(2019, 11, 08) };

            Moving tempMoving = new Moving(temp.Name, tempCustomer, startingAddress, 700, testList_two, "Hjælp Lars med at flytte", "23131343", tempCustomer.Address, 5, true, 1);
            tempMoving.InspectionReport = inspection;
            database.Tasks.Add(tempMoving);
            database.SaveChanges();
            
        }
        
    }
}
