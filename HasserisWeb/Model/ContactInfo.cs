using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class ContactInfo
    {
        public string Email { get; set; }
        public string phoneNumber { get; set; }

        public ContactInfo(string email, string phoneNumber)
        {
            this.Email = email;
            this.phoneNumber = phoneNumber;
        }
    }
}
