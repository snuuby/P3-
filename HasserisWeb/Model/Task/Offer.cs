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
        public Customer Customer { get; set; }
        public Address StartingAddress { get; set; }
        public Address Destination { get; set; }
        public DateTime InspectionDate { get; set; }
        public DateTime MovingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ExpectedHours { get; set; }
        public int LentBoxes { get; set; }
        //With Packing, Private or Business
        public string OfferType {get; set;}
        public Offer(Customer Customer, Address StartingAddress, Address Destination, DateTime InspectionDate, DateTime MovingDate, 
                    DateTime ExpirationDate) {
                        this.Customer = Customer;
                        this.StartingAddress = StartingAddress;
                        this.InspectionDate = InspectionDate;
                        this.MovingDate = MovingDate;
                        this.ExpirationDate = ExpirationDate;
                        this.Destination = Destination;
                        
                    }
        public Offer()
        {

        }
    }
}
