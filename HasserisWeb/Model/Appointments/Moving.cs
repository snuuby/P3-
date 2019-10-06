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

        public Moving(int ID, string name, string type, double duration, Customer assignedCustomer,
                  Address destination, double income, DateTime date, string note, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(ID, name, type, duration, assignedCustomer, destination, income, date, note, workPhoneNumber)
        {
            this.startingAddress = startingAddress;
            this.lentBoxes = lentBoxes;
        }
    }
}
