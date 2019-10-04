using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Customer
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int ID { get; set; }
        public Address address { get; set; }
        public Customer(string fname, string lname, int id, Address address)
        {
            this.firstName = fname;
            this.lastName = lname;
            this.ID = id;
            this.address = address;
        }

    }

    public class Business : Customer
    {
        public int EAN { get; set; }
        public int CVR { get; set; }
    }
}