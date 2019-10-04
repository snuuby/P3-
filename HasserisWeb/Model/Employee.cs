using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Employee
    {
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public bool isAvailable { get; private set; }
        public Appointment currentAppoint { get; private set; }
        public double wage { get; private set; }
        public int id {get; set;}

        public Employee(string fName, string lName, double pWage)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.wage = pWage;
        }
        public void AssignEmployee()
        {
            throw new NotImplementedException();
        }
        public void UnassignEmployee()
        {
            this.currentAppoint = null;
            this.isAvailable = true;
        }


    }
}