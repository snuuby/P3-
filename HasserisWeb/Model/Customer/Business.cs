using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Business : Customer
    {
        public int EAN { get; set; }
        public int CVR { get; set; }

        public Business(string fName, string lName, Address address, ContactInfo contactInfo)
                        : base(fName, lName, address, contactInfo)
        {

        }
    }
}
