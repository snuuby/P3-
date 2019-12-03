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
    [Route("inspection")]
    public class InspectionController : Controller
    {

        HasserisDbContext database;
        public InspectionController(HasserisDbContext database) 
        {
            this.database = database;
        }

        [HttpPost]
        [Route("edit")]
        public void EditInspectionReport([FromBody]dynamic json) 
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int inspectionID = temp.ID;
            InspectionReport inspectionReport = PopulateInspectionReport(temp);

            Moving moving = (Moving)database.Tasks.FirstOrDefault(i => i.InspectionReport.ID == inspectionID);
            moving.InspectionReport = inspectionReport;
            database.Update(moving);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("make")]
        public void MakeInspectionReport([FromBody]dynamic json)
        {

            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            InspectionReport inspectionReport = PopulateInspectionReport(temp);
            Moving tempmoving = new Moving();
            tempmoving.Phase = 1;
            tempmoving.InspectionReport = inspectionReport;
            database.Tasks.Add(tempmoving);
            database.SaveChanges();
        }
        public InspectionReport PopulateInspectionReport(dynamic temp)
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
            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            Employee tempEmployee= database.Employees.FirstOrDefault(emp => emp.ID == employeeID);
            Vehicle tempCar= database.Equipment.OfType<Vehicle>().FirstOrDefault(car => car.ID == carID);
            string inspection = temp.InspectionDate;
            string moving = temp.MovingDate;
            DateTime inspectionDate = DateTime.Parse(inspection, CultureInfo.GetCultureInfo("sv-SE"));
            DateTime movingDate = DateTime.Parse(moving, CultureInfo.GetCultureInfo("sv-SE"));
            return new InspectionReport(tempCustomer, startingAddress, destination, tempEmployee, tempCar, notes, inspectionDate, movingDate );
        }
        [HttpGet]
        [Route("{id}")]
        public string GetInspectionReport(int id)
        { 
            InspectionReport tempReport = database.Inspections.Where(i => i.ID == id)
            .Include(e => e.Employee).Include(c => c.Car).Include(c => c.Customer).ThenInclude(c => c.ContactInfo)
            .Include(s => s.StartingAddress).Include(d => d.Destination).Single();
            return JsonConvert.SerializeObject(tempReport);
        }
        [HttpGet]
        [Route("getall")]
        public string GetAllInspectionReports()
        {

            List<InspectionReport> tempReports = database.Inspections
            .Include(e => e.Employee).Include(c => c.Car).Include(c => c.Customer).ThenInclude(c => c.ContactInfo)
            .Include(s => s.StartingAddress).Include(d => d.Destination).ToList();
            return JsonConvert.SerializeObject(tempReports);
        }
    }
}
