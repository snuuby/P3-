using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Moving-type task, for moving furniture/other for a customer.
    public class Moving : Task
    {
        public Address StartingAddress { get; }
        public int LentBoxes { get; }
        public ICollection<Furniture> Furnitures { get; }

        public Moving(string name, string type, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string description, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(name, type, assignedCustomer, destination, income, dates, description, workPhoneNumber)
        {
            this.StartingAddress = startingAddress;
            this.LentBoxes = lentBoxes;
            this.Customer.LentBoxes = lentBoxes;

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
