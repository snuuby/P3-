using System;

namespace HasserisWeb
{
    public class Address
    {
        public string livingAddress { get; set; }
        public string ZIP { get; set; }
        public string city { get; set; }
        public string note { get; set; }
        public Address(string address, string zip, string city, string note)
        {
            this.livingAddress = address;
            this.ZIP = zip;
            this.city = city;
            this.note = note;
        }
    }
}