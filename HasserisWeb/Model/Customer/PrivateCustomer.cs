using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Private : Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Private(string fName, string lName, Address address, ContactInfo contactInfo)
                : base(address, contactInfo)
        {
            this.firstName = fName;
            this.lastName = lName;
        }
    }
}
