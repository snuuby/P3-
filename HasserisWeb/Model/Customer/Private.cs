using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Private-type customer class, for individuals/families.
    public class Private : Customer
    {
        public string Firstname { get; set; }
        public string Lastname { get; set; }

        public Private(string Firstname, string Lastname, string type, Address address, ContactInfo contactInfo)
                : base(address, contactInfo, type)
        {
            this.Firstname = Firstname;
            this.Lastname = Lastname;
        }
    }
}
