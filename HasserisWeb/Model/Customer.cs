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
        public int ID { get; set; }
        public Address Address { get; set; }
        public Customer(string fname, string lname, int id, Address address)
        {
            this.FirstName = fname;
            this.LastName = lname;
            this.ID = id;
            this.Address = address;
        }
    }

    public class Business : Customer
    {
        public int EAN { get; set; }
        public int CVR { get; set; }
    }
}