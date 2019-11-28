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
using Newtonsoft.Json.Linq;

namespace HasserisWeb.Controllers
{
    [Route("customers")]
    public class CustomerController : Controller
    {
        public class CustomerTypeClass
        {
            public string CustomerType { get; set; }
        }
        public HasserisDbContext database;
        public CustomerController(HasserisDbContext sc)
        {
            database = sc;
        }
        [HttpGet]
        [Route("all")]
        public string GetAllCustomers()
        {
                return JsonConvert.SerializeObject(database.Customers.
                Include(contact => contact.ContactInfo).
                Include(address => address.Address).
                ToList());

        }
        [HttpGet]
        [Route("private")]
        public string GetPrivateCustomers()
        {

            List<Private> privates = database.Customers.OfType<Private>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList();
            JObject jo = JObject.FromObject(privates);
            jo.Add("CustomerType", "private");
            return jo.ToString();
        }
        [HttpGet]
        [Route("public")]
        public string getPublicCustomers()
        {
            List<Public> publics = database.Customers.OfType<Public>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList();
            JObject jo = JObject.FromObject(publics);
            jo.Add("CustomerType", "public");
            return jo.ToString();
        }
        [HttpGet]
        [Route("business")]
        public string GetBusinessCustomers()
        {
            List<Business> business = database.Customers.OfType<Business>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList();
            JObject jo = JObject.FromObject(business);
            jo.Add("CustomerType", "business");
            return jo.ToString();
        }

        [Route("{id}")]
        public string GetSpecificCustomer(int id)
        {
            return JsonConvert.SerializeObject(database.Customers
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }


        // add business customer
        [HttpPost]
        [Route("addbusiness")]
        public string CreateBusinessCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Name for a business customer
            string customerName = temp.Name;
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

            Business tempBusiness = new Business(
                new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                new ContactInfo(customerEmail, customerPhoneNumber), 
                customerName, customerCVR);
            database.Customers.Add(tempBusiness);

            database.SaveChanges();

            return "Business customer added";
        }


        // add public customer
        [HttpPost]
        [Route("addpublic")]
        public string CreatePublicCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Name for the public customer
            string customerName = temp.Name;

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

            Public tempPublic = new Public(
                new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                new ContactInfo(customerEmail, customerPhoneNumber), 
                customerName, customerEAN);
            database.Customers.Add(tempPublic);

            database.SaveChanges();

            return "Public customer added";
        }


        // add private customer
        [HttpPost]
        [Route("addprivate")]
        public string CreatePrivateCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            // Firstname and lastname for a private business customer
            string customerFirstName = temp.Firstname;
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

            Private tempPrivate = new Private(customerFirstName, customerLastName,
                                                   new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                                                   new ContactInfo(customerEmail, customerPhoneNumber));
            database.Customers.Add(tempPrivate);

            database.SaveChanges();

            return "Private customer added";
        }
    }
}
