﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Delivery-type task, for sale of a product (material) in a given quantity to a customer.
    public class Delivery : Task
    {
        [Required]
        public string Material { get; set; }
        [Required]
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
