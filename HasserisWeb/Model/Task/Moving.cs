using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Moving-type task, for moving furniture/other for a customer.
    public class Moving : Task
    {
        [Required]
        public Address StartingAddress { get; set; }
        public int LentBoxes { get; set; }
        public ICollection<Furniture> Furnitures { get; set; } = new List<Furniture>();
        public bool WithPacking { get; set; }
        public Moving(string name, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string description, string workPhoneNumber, Address startingAddress, int lentBoxes, bool WithPacking, int phase)
                : base(name, assignedCustomer, destination, income, dates, description, workPhoneNumber, phase)
        {
            this.StartingAddress = startingAddress;
            this.LentBoxes = lentBoxes;
            this.Customer.LentBoxes = lentBoxes;
            this.WithPacking = WithPacking;
        }
        public Moving()
        {

        }

        //Calculates the total cubic size of all furniture on a specific moving task.
        public double totalCubicSize(List<Furniture> listofFurnitures)
        {
            double totalSize = 0;
            foreach( var element in listofFurnitures)
            {
                totalSize += element.CubicSize;
            }
            return totalSize;
        }
    }
}
