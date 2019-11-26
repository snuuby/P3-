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
    [Route("offer")]
    public class OfferController : Controller
    {
        HasserisDbContext database;
        public OfferController(HasserisDbContext database)
        {
            this.database = database;
        }
        [HttpPost]
        [Route("makenew")]
        public void MakeOffer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int customerID = temp.Customer.ID;
            //StartingAddress
            string Saddress = temp.StartingAddress;
            string SZIP = temp.StartingZIP;
            string SCity = temp.StartingCity;
            //Destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationAddress;
            string DCity = temp.DestinationCity;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);

            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            string inspectionDate = temp.InspectionDate;
            string movingDate = temp.MovingDate;
            string expirationDate = temp.ExpirationDate;
            DateTime inspection = DateTime.Parse(inspectionDate, CultureInfo.GetCultureInfo("sv-SE"));
            inspection = inspection.AddHours(-1);
            DateTime moving = DateTime.Parse(movingDate, CultureInfo.GetCultureInfo("sv-SE"));
            moving = moving.AddHours(-1);
            DateTime expiration = DateTime.Parse(expirationDate, CultureInfo.GetCultureInfo("sv-SE"));
            expiration = expiration.AddHours(-1);

            Offer offer = new Offer(tempCustomer, startingAddress, destination, inspection, moving, expiration);

            Moving tempmoving = new Moving();
            tempmoving.Phase = 2;
            tempmoving.Offer = offer;
            database.Tasks.Add(tempmoving);
            database.SaveChanges();
        }

        [HttpPost]
        [Route("makefrominspection")]
        public void MakeFromInspectionReport([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int customerID = temp.Customer.ID;
            int inspectionID = temp.InspectionReport;
            //StartingAddress
            string Saddress = temp.StartingAddress;
            string SZIP = temp.StartingZIP;
            string SCity = temp.StartingCity;
            //Destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationAddress;
            string DCity = temp.DestinationCity;

            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);

            Customer tempCustomer = database.Customers.FirstOrDefault(cus => cus.ID == customerID);
            string inspectionDate = temp.InspectionDate;
            string movingDate = temp.MovingDate;
            string expirationDate = temp.ExpirationDate;
            DateTime inspection = DateTime.Parse(inspectionDate, CultureInfo.GetCultureInfo("sv-SE"));
            inspection = inspection.AddHours(-1);
            DateTime moving = DateTime.Parse(movingDate, CultureInfo.GetCultureInfo("sv-SE"));
            moving = moving.AddHours(-1);
            DateTime expiration = DateTime.Parse(expirationDate, CultureInfo.GetCultureInfo("sv-SE"));
            expiration = expiration.AddHours(-1);

            Offer offer = new Offer(tempCustomer, startingAddress, destination, inspection, moving, expiration);

            Moving task = (Moving)database.Tasks.Where(t => t.InspectionReport.ID == inspectionID);
            task.Phase = 2;
            task.Offer = offer;
            database.Tasks.Add(task);
            database.SaveChanges();
        }
        [HttpGet]
        [Route("{id}")]
        public string GetOffer(int id)
        {
            Offer tempOffer = database.Offers.FirstOrDefault(i => i.ID == id);
            return JsonConvert.SerializeObject(tempOffer);
        }
        [HttpGet]
        [Route("getall")]
        public string GetAllOffers()
        {

            List<Offer> tempReports = database.Offers.ToList();
            return JsonConvert.SerializeObject(tempReports);
        }
    }
}
