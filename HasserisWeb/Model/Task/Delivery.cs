using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Delivery-type task, for sale of a product (material) in a given quantity to a customer.
    public class Delivery : Task
    {
        public string material { get; }
        public int quantity { get; }
        public Delivery()
        {

        }
        
        public Delivery(string name, string type, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string description, string workPhoneNumber, string material, int quantity) 
                : base(name, type, assignedCustomer, destination, income, dates, description, workPhoneNumber)
        {
            this.material = material;
            this.quantity = quantity;
        }

    }
}
