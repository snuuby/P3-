using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Moving-type task, for moving furniture/other for a customer.
    public class Moving : Task
    {
        public Address startingAddress { get; }
        public int lentBoxes { get; }

        public Moving(string name, string type, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string note, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(name, type, assignedCustomer, destination, income, dates, note, workPhoneNumber)
        {
            this.startingAddress = startingAddress;
            this.lentBoxes = lentBoxes;
        }
    }
}
