using Microsoft.EntityFrameworkCore;
using System;
using System.ComponentModel.DataAnnotations;

namespace HasserisWeb
{
    
    public class Address
    {
        public int ID { get; set; }
        [Required]
        public string LivingAddress { get; set; }
        [Required]
        public string ZIP { get; set; }
        [Required]
        public string City { get; set; }
        [Required]
        public string Note { get; set; }
        public Address(string LivingAddress, string ZIP, string city, string note)
        {
            this.LivingAddress = LivingAddress;
            this.ZIP = ZIP;
            this.City = city;
            this.Note = note;
        }
        public Address()
        {

        }
    }
}