using System;
using System.Collections.Generic;
using System.Linq;

namespace HasserisWeb
{
    public class Employee
    {
        public string firstName { get;  set; }
        public string lastName { get;  set; }
        public bool isAvailable { get; private set; }
        //Bool given to admin-type employees. To require this with certain methods
        public bool isAdmin { get; private set; } 
        public ContactInfo contactInfo { get; set; }
        public double wage { get; private set; }
        public int id { get; set; }
        public Address address { get; set; }
        public string type { get; set; }

        // Test constructor Cholle
        public Employee()
        {
            
        }
        
        public Employee(string fName, string lName, string type, double pWage, ContactInfo contactInfo, Address address)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.wage = pWage;
            this.isAdmin = false;
            this.contactInfo = contactInfo;
            this.address = address;
            this.type = type;

        }
    }
}