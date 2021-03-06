﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

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
            offer.Sent = temp.Sent;
            offer.InvoiceSent = temp.InvoiceSent;
            offer.ID = offerID;
            int lentBoxes = temp.Lentboxes;
            int expectedHours = temp.ExpectedHours;
            offer.Lentboxes = lentBoxes;
            offer.ExpectedHours = expectedHours;
            offer.InspectionReportID = temp.InspectionReportID;
            offer.WithPacking = temp.WithPacking;
            offer.WasInspection = temp.WasInspection;
            offer.Lentboxes = lentBoxes;
            database.Offers.Update(offer);
            database.SaveChanges();

        }
        [HttpPost]
        [Route("create")]
        public void MakeOffer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Offer offer = PopulateOffer(temp);
            Moving task = new Moving();
            task.Phase = 2;
            task.Offer = offer;
            task.Offer.WithPacking = temp.WithPacking;
            task.Offer.WasInspection = false;
            int lentBoxes = temp.Lentboxes;
            int expectedHours = temp.ExpectedHours;
            task.Offer.Lentboxes = lentBoxes;
            task.Offer.ExpectedHours = expectedHours;
            database.Tasks.Add(task);
            database.SaveChanges();
        }

        [HttpPost]
        [Route("create/from/inspection")]
        public void MakeFromInspectionReport([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            Offer offer = PopulateOffer(temp);
            int inspectionID = temp.InspectionReportID;
            Moving task = (Moving)database.Tasks.FirstOrDefault(t => t.InspectionReport.ID == inspectionID);
            task.Phase = 2;
            task.Offer = offer;
            task.Offer.InspectionReportID = temp.InspectionReportID;
            task.Offer.WithPacking = temp.WithPacking;
            task.Offer.WasInspection = true;
            int lentBoxes = temp.Lentboxes;
            int expectedHours = temp.ExpectedHours;
            task.Offer.Lentboxes = lentBoxes;
            task.Offer.ExpectedHours = expectedHours;
            database.Tasks.Update(task);
            database.SaveChanges();
        }
        public Offer PopulateOffer(dynamic temp)
        {

            //StartingAddress
            string Saddress = temp.StartAddress;
            string SZIP = temp.StartZIP;
            string SCity = temp.StartCity;
            //Destination
            string Daddress = temp.DestinationAddress;
            string DZIP = temp.DestinationZIP;
            string DCity = temp.DestinationCity;


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
