using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Moving-type task, for moving furniture/other for a customer.
    public class Moving : Task
    {
        public Address startingAddress { get; }
        public int lentBoxes { get; }
        public List<Furniture> listofFurnitures { get; }

        public Moving(string name, string type, Customer assignedCustomer,
                  Address destination, double income, List<DateTime> dates, string description, string workPhoneNumber, Address startingAddress, int lentBoxes)
                : base(name, type, assignedCustomer, destination, income, dates, description, workPhoneNumber)
        {
            this.startingAddress = startingAddress;
            this.lentBoxes = lentBoxes;
            this.assignedCustomer.lentBoxes = lentBoxes;

        }

        //Calculates the total cubic size of all furniture on a specific moving task.
        public double totalCubicSize(List<Furniture> listofFurnitures)
        {
            double totalSize = 0;
            foreach( var element in listofFurnitures)
            {
                totalSize += element.cubicSize;
            }
            return totalSize;
        }
    }
}
