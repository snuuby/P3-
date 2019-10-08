using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Customer is an abstract class, meaning instances of customers has to be either a Private or Business.
    public abstract class Customer
    {

        public int id { get; set; }
        public Address address { get; set; }
        public ContactInfo contactInfo { get; set; }
        public Customer(Address address, ContactInfo contactInfo)
        {
            this.address = address;
            this.contactInfo = contactInfo;
            this.type = type;
            HasserisDbContext.SaveElementToDatabase<Customer>(this);
        }

    }


}