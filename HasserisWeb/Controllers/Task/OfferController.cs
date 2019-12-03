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
        [Route("edit")]
        public void EditOffer([FromBody]dynamic json) 
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int offerID = temp.ID;
            Offer offer = PopulateOffer(temp);

            Moving moving = (Moving)database.Tasks.FirstOrDefault(i => i.Offer.ID == offerID);
            moving.Offer = offer;
            database.Tasks.Update(moving);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create")]
        public void MakeOffer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Offer offer = PopulateOffer(temp);
            Moving tempmoving = new Moving();
            tempmoving.Phase = 2;
            tempmoving.Offer = offer;
            database.Tasks.Add(tempmoving);
            database.SaveChanges();
        }

        [HttpPost]
        [Route("create/from/inspection")]
        public void MakeFromInspectionReport([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Offer offer = PopulateOffer(temp);
            int inspectionID = temp.InspectionReport;
            Moving task = (Moving)database.Tasks.FirstOrDefault(t => t.InspectionReport.ID == inspectionID);
            task.Phase = 2;
            task.Offer = offer;
            task.WithPacking = temp.WithPacking;
            task.Offer.wasInspection = temp.wasInspection;
            task.Offer.inspectionReport = temp.inspectionReport;
            database.Tasks.Update(task);
            database.SaveChanges();
        }
        public Offer PopulateOffer(dynamic temp) {

            //StartingAddress
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //Destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationZIP;
            string DCity = temp.DestinationCity;

            int lentBoxes = temp.Lentboxes;
            int expectedHours = temp.ExpectedHours;
            Address startingAddress = new Address(Saddress, SZIP, SCity);
            Address destination = new Address(Daddress, DZIP, DCity);
            int customerID = temp.CustomerID;
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
            string offerType = temp.OfferType;
            offer.OfferType = offerType;
            return offer;
        }
        [HttpGet]
        [Route("{id}")]
        public string GetOffer(int id)
        {
            Offer tempOffer = database.Offers.Where(i => i.ID == id)
            .Include(c => c.Customer).ThenInclude(c => c.ContactInfo).Include(s => s.StartingAddress).Include(d => d.Destination).Single();
            return JsonConvert.SerializeObject(tempOffer);
        }
        [HttpGet]
        [Route("getall")]
        public string GetAllOffers()
        {

            List<Offer> tempReports = database.Offers
            .Include(c => c.Customer).ThenInclude(c => c.ContactInfo).Include(s => s.StartingAddress).Include(d => d.Destination).ToList();
            return JsonConvert.SerializeObject(tempReports);
        }
    }
}
