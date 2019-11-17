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
        public Address DestinationAddress { get; set; }
        public DateTime VisitingDate { get; set; }
        public DateTime MovingDate { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int ExpectedHours { get; set; }
        public int LentBoxes { get; set; }
        public Offer()
        {

        }
    }
}
