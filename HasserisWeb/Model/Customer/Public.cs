using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Public : Customer
    {
        public string businessName { get; set; }
        public string EAN { get; set; }

        public Public(string fName, string lName, string type, Address address, ContactInfo contactInfo, string businessName, string EAN)
                        : base( address, contactInfo, type)
        {
            this.businessName = businessName;
            this.EAN = EAN;
        }
    }
}
