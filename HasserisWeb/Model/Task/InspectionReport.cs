using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    // represents phase 1 in the process of an order
    public class InspectionReport
    {
        public int ID { get; set; }
        public Customer Customer { get; set; }
        public string Name { get; set; }
        public Address StartingAddress { get; set; }
        public Address Destination { get; set; }
        public string Notes { get; set; }
        public DateTime InspectionDate { get; set; }
        public DateTime MovingDate { get; set; }
        public int ExpectedHours { get; set; }
        public Employee Employee {get; set;}
        public Vehicle Car {get; set;}
        public int LentBoxes { get; set; }

        public InspectionReport(Customer Customer, Address StartingAddress, Address Destination, Employee Employee, Vehicle Car, string Notes, 
                                DateTime InspectionDate, DateTime MovingDate) {
            this.Customer = Customer;
            this.Car = Car;
            this.Employee = Employee;
            this.StartingAddress = StartingAddress;
            this.Notes = Notes;
            this.InspectionDate = InspectionDate;
            this.MovingDate = MovingDate;
            this.Destination = Destination;
        }
        public InspectionReport()
        {

        }
    }
}
