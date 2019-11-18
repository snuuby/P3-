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
        public Address DestinationAddress { get; set; }
        public string Notes { get; set; }
        public DateTime VisitingDate { get; set; }
        public DateTime MovingDate { get; set; }
        public int ExpectedHours { get; set; }
        public ICollection<InspectionAssignedEmployees> Employees { get; set; } = new List<InspectionAssignedEmployees>();
        public ICollection<InspectionAssignedEquipment> Equipment { get; set; } = new List<InspectionAssignedEquipment>();
        public int LentBoxes { get; set; }

        public InspectionReport(Customer Customer, string Name, Address StartingAddress, string Notes, 
                                DateTime VisitingDate) {
                                    this.Customer = Customer;
                                    this.Name = Name;
                                    this.StartingAddress = StartingAddress;
                                    this.Notes = Notes;
                                    this.VisitingDate = VisitingDate;
                                }
        public InspectionReport()
        {

        }
    }
}
