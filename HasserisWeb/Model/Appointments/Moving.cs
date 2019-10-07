using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Moving : Appointment
    {
        public Address startingAddress { get; }
        public int lentBoxes { get; }

        public Moving(string name, string type, Customer assignedCustomer,
                  Address destination, double income, DateTime date, string note, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(name, type, assignedCustomer, destination, income, date, note, workPhoneNumber)
        {
            this.startingAddress = startingAddress;
            this.lentBoxes = lentBoxes;
        }
    }
}
