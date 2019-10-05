using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public abstract class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int ID { get; set; }
        public Address address { get; set; }
        public ContactInfo contactInfo { get; set; }
        public Customer(string fname, string lname, Address address, ContactInfo contactInfo)
        {
            this.firstName = fname;
            this.lastName = lname;
            this.address = address;
            this.contactInfo = contactInfo;
        }

    }


}