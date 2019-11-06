using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Public-type customer class, for public work/communial.
    public class Public : Customer
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string EAN { get; set; }
        public Public()
        {

        }
        public Public(Address address, ContactInfo contactInfo, string Name, string EAN)
                        : base(address, contactInfo)
        {
            base.Type = "Public";
            this.Name = Name;
            this.EAN = EAN;
        }
    }
}
