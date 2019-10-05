using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb.Model
{
    public class Moving : Appointment
    {
        private Address startingAddress { get; }
        private int lentBoxes { get; }

        public Moving(int ID, string name, bool type, double duration, Customer assignedCustomer,
                  Address destination, double income, DateTime date, string note, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(ID, name, type, duration, assignedCustomer, destination, income, date, note, workPhoneNumber)
        {
            this.startingAddress = startingAddress;
            this.lentBoxes = lentBoxes;
        }
    }
}
