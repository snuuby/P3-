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

        public Delivery(int ID, string name, string type, double duration, Customer assignedCustomer,
                  Address destination, double income, DateTime date, string note, string workPhoneNumber, string material, int quantity) 
                : base(ID, name, type, duration, assignedCustomer, destination, income, date, note, workPhoneNumber)
        {
            this.material = material;
            this.quantity = quantity;
        }

    }
}
