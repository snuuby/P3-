﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace HasserisWeb
{
    //Customer is an abstract class, meaning instances of customers has to be either a Private, Public or Business.
    public abstract class Customer
    {

        protected Customer()
        {

        }
        public int ID { get; set; }
        public virtual Address Address { get; set; }
        public virtual ContactInfo ContactInfo { get; set; }
        public int LentBoxes { get; set; }
        public Customer(Address Address, ContactInfo ContactInfo)
        {
            this.Address = Address;
            this.ContactInfo = ContactInfo;


        }


    }


}