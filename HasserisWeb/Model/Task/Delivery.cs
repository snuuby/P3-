using System;
using System.Collections.Generic;

namespace HasserisWeb
{
    //Delivery-type task, for sale of a product (material) in a given quantity to a customer.
    public class Delivery : Task
    {
        public string Material { get; set; }
        public int Quantity { get; set; }
        public Delivery()
        {

        }
        public Delivery(string name, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string description, string workPhoneNumber, string material, int quantity, int phase)
                : base(name, assignedCustomer, destination, income, dates, description, workPhoneNumber, phase)
        {
            this.Material = material;
            this.Quantity = quantity;
        }

    }
}
