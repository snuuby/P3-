using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;

namespace HasserisWeb.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        public HasserisDbContext database;
        public CustomerController(HasserisDbContext sc)
        {
            database = sc;
        }
        
        [Route("all")]
        public string GetAllCustomers()
        {
                return JsonConvert.SerializeObject(database.Customers.
                Include(contact => contact.ContactInfo).
                Include(address => address.Address).
                ToList());
            
        }
        
        [Route("{id}")]
        public string GetSpecificCustomer(int id)
        {
            return JsonConvert.SerializeObject(database.Customers
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }
        

        // add private customer
        [HttpPost]
        [Route("addbusiness")]
        public string CreateBusinessCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Names
            string customerName = temp.Firstname;
            string customerLastName = temp.Lastname;
            
            //Address
            string customerLivingAddress = temp.Address;
            string customerZIP = temp.ZIP;
            string customerCity = temp.City;
            string customerNote = temp.Note;
            //ContactInfo
            string customerEmail = temp.Email;
            string customerPhoneNumber = temp.Phonenumber;
            
            // Business Customer exclusive
            string customerCVR = temp.CVR;
            
            //Type - is not used anymore each customer type have their own route
            string customerType = temp.Type;

            Private tempPrivate = new Private(customerName, customerLastName, 
                new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                new ContactInfo(customerEmail, customerPhoneNumber));
            database.Customers.Add(tempPrivate);
         
            database.SaveChanges();

            return "succesfully added new customer";
        }

        
        // add private customer
        [HttpPost]
        [Route("addpublic")]
        public string CreatePublicCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Names
            string customerName = temp.Firstname;
            string customerLastName = temp.Lastname;
            
            //Address
            string customerLivingAddress = temp.Address;
            string customerZIP = temp.ZIP;
            string customerCity = temp.City;
            string customerNote = temp.Note;
            //ContactInfo
            string customerEmail = temp.Email;
            string customerPhoneNumber = temp.Phonenumber;
            
            // Public Customer exclusive
            string customerEAN = temp.EAN;
            
            
            //Type - is not used anymore each customer type have their own route
            string customerType = temp.Type;

            Private tempPrivate = new Private(customerName, customerLastName, 
                new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                new ContactInfo(customerEmail, customerPhoneNumber));
            database.Customers.Add(tempPrivate);
         
            database.SaveChanges();

            return "succesfully added new customer";
        }

        
        // add private customer
        [HttpPost]
        [Route("addprivate")]
        public string CreatePrivateCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Names
            string customerName = temp.Firstname;
            string customerLastName = temp.Lastname;
            
            //Address
            string customerLivingAddress = temp.Address;
            string customerZIP = temp.ZIP;
            string customerCity = temp.City;
            string customerNote = temp.Note;
            //ContactInfo
            string customerEmail = temp.Email;
            string customerPhoneNumber = temp.Phonenumber;
            
            
            //Type - is not used anymore each customer type have their own route
            string customerType = temp.Type;

            Private tempPrivate = new Private(customerName, customerLastName, 
                                                   new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                                                   new ContactInfo(customerEmail, customerPhoneNumber));
            database.Customers.Add(tempPrivate);
         
            database.SaveChanges();

            return "succesfully added new customer";
        }
    }
}