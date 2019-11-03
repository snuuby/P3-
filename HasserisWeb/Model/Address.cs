using Microsoft.EntityFrameworkCore;
using System;

namespace HasserisWeb
{
    
    [Owned]
    public class Address
    {
        public string LivingAddress { get; set; }
        public string ZIP { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public Address(string LivingAddress, string ZIP, string city, string note)
        {
            this.LivingAddress = LivingAddress;
            this.ZIP = ZIP;
            this.City = city;
            this.Note = note;
        }
    }
}