using System;

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
        public int Lentboxes { get; set; }
        public bool WasInspection { get; set; }
        //ID
        public int InspectionReportID { get; set; }
        public bool Sent { get; set; }
        public bool InvoiceSent { get; set; }
        public bool WithPacking { get; set; }
        //Private, Public or Business
        public string OfferType { get; set; }
        public Offer(Customer Customer, Address StartingAddress, Address Destination, DateTime InspectionDate, DateTime MovingDate,
                    DateTime ExpirationDate)
        {
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
