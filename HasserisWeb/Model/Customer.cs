using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public abstract class Customer
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ID { get; private set; }
        private Address Address { get; set; }
    }

    public class Business : Customer
    {
        public int EAN { get; set; }
        public int CVR { get; set; }
    }

    internal class Address
    {
        public int ZIP { get; set; }
        public string City { get; set; }
        public string Note { get; set; }
    }
}