using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Public-type customer class, for public work/communial.
    public class Public : Customer
    {
        public string Name { get; set; }
        public string EAN { get; set; }

        public Public(string type, Address address, ContactInfo contactInfo, string Name, string EAN)
                        : base(address, contactInfo, type)
        {
            this.Name = Name;
            this.EAN = EAN;
        }
    }
}
