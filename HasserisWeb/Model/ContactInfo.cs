using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Class with contact information to be used with Employees and Customers.
    public class ContactInfo
    {
        public string email { get; set; }
        public string phoneNumber { get; set; }

        public ContactInfo(string email, string phoneNumber)
        {
            this.email = email;
            this.phoneNumber = phoneNumber;
        }
    }
}
