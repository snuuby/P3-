using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Employee
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public bool IsAvailable { get; private set; }
        public Appointment CurrentAppoint { get; private set; }
        public double Wage { get; private set; }

        public Employee(string FName, string LName, double PWage)
        {
            this.FirstName = FName;
            this.LastName = LName;
            this.Wage = PWage;
        }
        public void AssignEmployee()
        {
            throw new NotImplementedException();
        }
        public void UnassignEmployee()
        {
            this.CurrentAppoint = null;
            this.IsAvailable = true;
        }

    }
}