using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Private-type customer class, for individuals/families.
    public class Private : Customer
    {
        [Required]
        public string Firstname { get; set; }
        [Required]
        public string Lastname { get; set; }
        public Private()
        {

        }
        public Private(string Firstname, string Lastname, Address address, ContactInfo contactInfo)
                : base(address, contactInfo)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
        }
    }
}
