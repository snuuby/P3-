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
                return JsonConvert.SerializeObject(database.Customers.ToList());
            
        }
        
        [Route("{id}")]
        public string GetSpecificCustomer(int id)
        {
            return JsonConvert.SerializeObject(database.Customers
                .Include(contact => contact.ContactInfo)
                .Include(address => address.Address)
                .FirstOrDefault(c => c.ID == id));
        }
        
        [Route("add")]
        public string CreateCustomer([FromBody]dynamic json)
        {
            dynamic eNewCustomer = JsonConvert.DeserializeObject(json.ToString());
            //Address
            string customerLivingAddress = eNewCustomer.newCustomer.LivingAddress;
            string customerZIP = eNewCustomer.newCustomer.ZIP;
            string customerCity = eNewCustomer.newCustomer.City;
            string customerNote = eNewCustomer.newCustomer.Note;
            //ContactInfo
            string customerEmail = eNewCustomer.newCustomer.Email;
            string customerPhoneNumber = eNewCustomer.newCustomer.PhoneNumber;
            //Type
            string customerType = eNewCustomer.newCustomer.Type;
            string customerTypeSpecific1 = eNewCustomer.newCustomer.TypeSpecific1;
            string customerTypeSpecific2 = eNewCustomer.newCustomer.TypeSpecific2;

            if (customerType == "Private")
            {
                Private tempPrivate = new Private(customerTypeSpecific1, customerTypeSpecific2, 
                                                   new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                                                   new ContactInfo(customerEmail, customerPhoneNumber));
                database.Customers.Add(tempPrivate);
            } else if (customerType == "Business")
            {
                Business tempBusiness = new Business(new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                                                     new ContactInfo(customerEmail, customerPhoneNumber),
                                                     customerTypeSpecific1, customerTypeSpecific2);
                database.Customers.Add(tempBusiness);
            } else if (customerType == "Public")
            {
                Public tempPublic = new Public(new Address(customerLivingAddress, customerZIP, customerCity, customerNote),
                                               new ContactInfo(customerEmail, customerPhoneNumber),
                                               customerTypeSpecific1, customerTypeSpecific2);
                database.Customers.Add(tempPublic);
            }

            database.SaveChanges();

            return "succesfully added new customer";
        }
    }
}