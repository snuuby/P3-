using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Business : Customer
    {
        public string EAN { get; set; }
        public string CVR { get; set; }

        public Business(string fName, string lName, string type, Address address, ContactInfo contactInfo, string EAN, string CVR )
                        : base(fName, lName, type, address, contactInfo)
        {
            this.EAN = EAN;
            this.CVR = CVR;
        }
    }
}
