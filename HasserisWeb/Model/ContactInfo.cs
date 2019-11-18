using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Class with contact information to be used with Employees and Customers.
    public class ContactInfo 
    {
        public int ID { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
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
