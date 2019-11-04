using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Class with contact information to be used with Employees and Customers.
    public class ContactInfo 
    {
        public int ID { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public ContactInfo(string email, string phoneNumber)
        {
            this.Email = email;
            this.PhoneNumber = phoneNumber;

        }
        public ContactInfo()
        {

        }
    }
}
