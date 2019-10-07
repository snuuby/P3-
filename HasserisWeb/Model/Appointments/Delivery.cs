using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Delivery : Appointment
    {
        public string material { get; }
        public int quantity { get; }

        public Delivery(string name, string type, Customer assignedCustomer,
                  Address destination, double income, DateTime date, string note, string workPhoneNumber, string material, int quantity) 
                : base(name, type, assignedCustomer, destination, income, date, note, workPhoneNumber)
        {
            this.material = material;
            this.quantity = quantity;
        }

    }
}
