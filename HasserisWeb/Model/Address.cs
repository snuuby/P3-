using System;

namespace HasserisWeb
{
    public class Address
    {
        public string livingAdress { get; set; }
        public int ZIP { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
        public Address(string address, int zip, string city, string note)
        {
            this.livingAdress = address;
            this.ZIP = zip;
            this.City = city;
            this.Note = note;
        }
    }
}