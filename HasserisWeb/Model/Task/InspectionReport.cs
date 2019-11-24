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
        public virtual Customer Customer { get; set; }
        public string Name { get; set; }
        public virtual Address StartingAddress { get; set; }
        public virtual Address DestinationAddress { get; set; }
        public string Notes { get; set; }
        public DateTime VisitingDate { get; set; }
        public DateTime MovingDate { get; set; }
        public int ExpectedHours { get; set; }
        public virtual Employee Employee {get; set;}
        public virtual Vehicle Car {get; set;}
        public int LentBoxes { get; set; }

        public InspectionReport(Customer Customer, string Name, Address StartingAddress, Employee Employee, Vehicle Car, string Notes, 
                                DateTime VisitingDate) {
                                    this.Customer = Customer;
                                    this.Name = Name;
                                    this.Car = Car;
                                    this.Employee = Employee;
                                    this.StartingAddress = StartingAddress;
                                    this.Notes = Notes;
                                    this.VisitingDate = VisitingDate;
                                }
        public InspectionReport()
        {

        }
    }
}
