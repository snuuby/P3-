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

            return JsonConvert.SerializeObject(database.Customers.OfType<Private>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList());

        }
        [HttpGet]
        [Route("public")]
        public string getPublicCustomers()
        {
            return JsonConvert.SerializeObject(database.Customers.OfType<Public>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList());
        }
        [HttpGet]
        [Route("business")]
        public string GetBusinessCustomers()
        {
            return JsonConvert.SerializeObject(database.Customers.OfType<Business>().
            Include(contact => contact.ContactInfo).
            Include(address => address.Address).
            ToList());
        }
        
        [HttpPost]
        [Route("addprivate")]
        public void AddPrivateCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            string firstname = temp.Firstname;
            string lastname = temp.Lastname;
            Private privateCustomer = new Private(firstname, lastname, livingaddress, contactinfo);
            database.Customers.Add(privateCustomer);
            database.SaveChanges();
        }
        
        [HttpPost]
        [Route("editprivate")]
        public void EditPrivateCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Private customer = (Private)database.Customers.FirstOrDefault(c => c.ID == id);
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            customer.Address = livingaddress;
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            customer.ContactInfo = contactinfo;

            customer.Firstname = temp.Firstname;
            customer.Lastname = temp.Lastname;
            database.Update(customer);
            database.SaveChanges();
        }
        [HttpPost]
        [Route("addpublic")]
        public void AddPublicCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            string name = temp.Name;
            string ean = temp.EAN;
            Public publicCustomer = new Public(livingaddress, contactinfo, name, ean);
            database.Customers.Add(publicCustomer);
            database.SaveChanges();
        }
        
        [HttpPost]
        [Route("editpublic")]
        public void EditPublicCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Public customer = (Public)database.Customers.FirstOrDefault(c => c.ID == id);
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            customer.Address = livingaddress;
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            customer.ContactInfo = contactinfo;

            customer.Name = temp.Name;
            customer.EAN = temp.EAN;
            database.Update(customer);
            database.SaveChanges();
        }
        [HttpPost]
        [Route("addbusiness")]
        public void AddBusinessCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            string name = temp.Name;
            string cvr = temp.CVR;
            Business publicCustomer = new Business(livingaddress, contactinfo, name, cvr);
            database.Customers.Add(publicCustomer);
            database.SaveChanges();
        }
        [HttpPost]
        [Route("editbusiness")]
        public void EditBusinessCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Business customer = (Business)database.Customers.FirstOrDefault(c => c.ID == id);
            string address = temp.LivingAddress;
            string zip = temp.ZIP;
            string city = temp.City;
            Address livingaddress = new Address(address, zip, city);
            customer.Address = livingaddress;
            string email = temp.Email;
            string phonenumber = temp.Phonenumber;
            ContactInfo contactinfo = new ContactInfo(email, phonenumber);
            customer.ContactInfo = contactinfo;

            customer.Name = temp.Name;
            customer.CVR = temp.CVR;
            database.Update(customer);
            database.SaveChanges();
        }
        
        [HttpGet]
        [Route("{id}")]
        public string GetSpecificCustomer(int id)
        {
            dynamic tempCustomer = database.Customers
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id);

            JObject temp = JObject.FromObject(tempCustomer);
            if (tempCustomer.GetType() == typeof(Private))
            {
                temp.Add("CustomerType", "Private");
            }
            else if (tempCustomer.GetType() == typeof(Business))
            {
                temp.Add("CustomerType", "Business");
            }
            else
            {
                temp.Add("CustomerType", "Public");
            }
            return temp.ToString();
        }
      
        [HttpPost]
        [Route("remove")]
        public void DeleteCustomer([FromBody]dynamic json)
        {
            dynamic temp = JsonConvert.DeserializeObject(json.ToString());
            int id = temp.ID;
            Customer customer = database.Customers.FirstOrDefault(c => c.ID == id);
            database.Customers.Remove(customer);
            database.SaveChanges();
        }
    }
}
