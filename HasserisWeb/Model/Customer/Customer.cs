using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace HasserisWeb
{
    //Customer is an abstract class, meaning instances of customers has to be either a Private, Public or Business.
    public abstract class Customer
    {
        public int ID { get; set; }
        public Address Address { get; set; }
        public string Type { get; set; }
        public ContactInfo ContactInfo { get; set; }
        public int LentBoxes { get; set; }
        public Customer(Address Address, ContactInfo ContactInfo, string type)
        {
            this.Type = type;
            this.Address = Address;
            this.ContactInfo = ContactInfo;


        }

    }


}