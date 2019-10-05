using System;

namespace HasserisWeb
{
    public class Address
    {
        public string livingAdress { get; set; }
        public int ZIP { get; set; }
        public string city { get; set; }
        public string note { get; set; }
        public Address(string address, int zip, string city, string note)
        {
            this.livingAdress = address;
            this.ZIP = zip;
            this.city = city;
            this.note = note;
        }
    }
}