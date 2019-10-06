using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class PrivateCustomer : Customer
    {
        public PrivateCustomer(string fName, string lName, string type, Address address, ContactInfo contactInfo)
                : base(fName, lName, type, address, contactInfo)
        {

        }
    }
}
