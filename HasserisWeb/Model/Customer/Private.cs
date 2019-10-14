using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Private-type customer class, for individuals/families.
    public class Private : Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }

        public Private(string fName, string lName, string type, Address address, ContactInfo contactInfo)
                : base(address, contactInfo, type)
        {
            this.firstName = fName;
            this.lastName = lName;
        }
    }
}
