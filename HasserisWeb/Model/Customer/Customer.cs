using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Customer is an abstract class, meaning instances of customers has to be either a Private, Public or Business.
    public abstract class Customer
    {
        public int id { get; set; }
        public Address address { get; set; }
        public string type { get; set; }
        public ContactInfo contactInfo { get; set; }
        public Customer(Address address, ContactInfo contactInfo, string type)
        {
            this.type = type;
            this.address = address;
            this.contactInfo = contactInfo;
        }

    }


}