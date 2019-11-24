using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    // represents phase 2 in the process of an order
    public class Offer
    {
        public int ID { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Address StartingAddress { get; set; }
        public virtual Address DestinationAddress { get; set; }
        public DateTime VisitingDate { get; set; }
        public DateTime MovingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ExpectedHours { get; set; }
        public int LentBoxes { get; set; }
        public Offer(Customer Customer, Address StartingAddress, DateTime VisitingDate, DateTime MovingDate, 
                    DateTime ExpirationDate, int ExpectedHours, int LentBoxes) {
                        this.Customer = Customer;
                        this.StartingAddress = StartingAddress;
                        this.VisitingDate = VisitingDate;
                        this.MovingDate = MovingDate;
                        this.ExpirationDate = ExpirationDate;
                        this.ExpectedHours = ExpectedHours;
                        this.LentBoxes = LentBoxes;
                    }
        public Offer()
        {

        }
    }
}
