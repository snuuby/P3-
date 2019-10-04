using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HasserisWeb
{
    public abstract class Appointment
    {
        private int ID { get; }
        private string name { get; }
        private bool type { get; }
        private double duration { get; }
        //[0] is hours, [1] is min, [2] is seconds
        private double[] hoursMinSeconds { get; set; }
        private int numberOfEmployees { get; set; }
        //I made this to a list instead, because there might be more employees per appointment
        //The way we can save those in the database, is by having a variable called numberOfEmployees, which represents
        //the lenght of the string in the database, that writes the employeeID's. If a string in the database has 5 ID,s next to each other
        // we read then one by one and retrieve the specified employee off of that.
        private List<Employee> assignedEmployees { get; }
        private Customer assignedCustomer { get; }
        private Address destination { get; }
        private double income { get; }
        private double expenses { get; set; }
        private double balance { get; set; }
        //The same idea here as for the list of employees
        private int numberOfEquipments {get; set;}
        private List<Equipment> equipments { get; }
        private DateTime date { get; }
        //What is a note?
        private string note { get; }
        private string workPhoneNumber { get; }

        public Appointment(int ID, string name, bool type, double duration, List<Employee> assignedEmployees, Customer assignedCustomer, Address destination, double income, List<Equipment> equipments, DateTime date, string note, string workPhoneNumber)
        {
            this.ID = ID;
            this.name = name;
            this.type = type;
            //I propose we make duration the total amount of seconds the appointment took. Then in this class we can make those seconds
            //Into hours, minutes and seconds. The database will only count the duration variable, so to not save 3 columns in the database
            this.duration = duration;
            this.hoursMinSeconds = convertSecondsToTime(duration);
            this.assignedEmployees = assignedEmployees;
            this.assignedCustomer = assignedCustomer;
            this.destination = destination;
            this.income = income;
            this.equipments = equipments;
            this.numberOfEquipments = equipments.Count;
            this.date = date;
            this.note = note;
            this.workPhoneNumber = workPhoneNumber;
            this.numberOfEmployees = assignedEmployees.Count;
            this.expenses = CalculateBalance();
            this.balance = this.income - this.expenses;
        }
        //Just change it if the calculation is more complex (it probably is)
        private double CalculateBalance()
        {
            double totalCost = 0;
            double employeeSecondWage;
            foreach (Employee employee in assignedEmployees)
            {
                employeeSecondWage = (employee.wage / 60) / 60;
                totalCost += employeeSecondWage * duration;
            }
            return totalCost;
        }
        private double[] convertSecondsToTime(double seconds)
        {
            double[] hoursMinSeconds = { 0, 0, 0 };
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            hoursMinSeconds[0] = t.Hours;
            hoursMinSeconds[1] = t.Minutes;
            hoursMinSeconds[2] = t.Seconds;
            return hoursMinSeconds;
        }
    }
}
