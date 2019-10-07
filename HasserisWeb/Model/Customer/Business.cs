using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Business : Customer
    {
        private string companyName;
        public string EAN { get; set; }
        public string CVR { get; set; }

        public Business(Address address, ContactInfo contactInfo, string name, string EAN, string CVR )
                        : base( address, contactInfo)
        {
            this.companyName = name;
            this.EAN = EAN;
            this.CVR = CVR;
        }
    }
}
