using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Business : Customer
    {
        public string businessName { get; set; }
        public string CVR { get; set; }

        public Business(string fName, string lName, string type, Address address, ContactInfo contactInfo, string businessName, string CVR )
                        : base(address, contactInfo, type)
        {
            this.businessName = businessName;
            this.CVR = CVR;
        }
    }
}
