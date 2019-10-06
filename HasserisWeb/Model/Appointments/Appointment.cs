using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HasserisWeb
{
    public abstract class Appointment
    {
        public int id { get; }
        public string name { get; }
        public string type { get; }
        public double duration { get; }
        //[0] is hours, [1] is min, [2] is seconds
        private double[] hoursMinSeconds { get; set; }
        public string employeesIdString { get; set; }
        //I made this to a list instead, because there might be more employees per appointment
        //The way we can save those in the database, is by having a variable called numberOfEmployees, which represents
        //the lenght of the string in the database, that writes the employeeID's. If a string in the database has 5 ID,s next to each other
        // we read them one by one and retrieve the specified employee off of that.
        //Also, i removed employees from the constructor because you should be able to add employees dynamically
        private List<Employee> assignedEmployees { get; }
        public Customer assignedCustomer { get; }
        public Address destination { get; }
        public double income { get; }
        public double expenses { get; set; }
        public double balance { get; set; }
        //The same idea here as for the list of employees
        public string equipmentsIdString { get; set; }
        //You should be able to add equipment dynamically so i removed it from the constructor
        private List<Equipment> assignedEquipments { get; }
        public DateTime date { get; }
        //What is a note?
        private string note { get; }
        public string workPhoneNumber { get; }

        public Appointment(int ID, string name, string type, double duration, Customer assignedCustomer, 
                          Address destination, double income, DateTime date, string note, string workPhoneNumber)
        {
            this.id = ID;
            this.name = name;
            this.type = type;
            //I propose we make duration the total amount of seconds the appointment took. Then in this class we can make those seconds
            //Into hours, minutes and seconds. The database will only count the duration variable, so to not save 3 columns in the database
            this.duration = duration;
            this.hoursMinSeconds = ConvertSecondsToTime(duration);
            this.assignedCustomer = assignedCustomer;
            this.destination = destination;
            this.income = income;
            this.date = date;
            this.note = note;
            this.workPhoneNumber = workPhoneNumber;
            this.expenses = CalculateBalance();
            this.balance = this.income - this.expenses;
            this.employeesIdString = "";
            this.equipmentsIdString = "";
        }
        //Just change it if the calculation is more complex (it probably is)
        //Maybe we have to take into account things like "feriepenge" and money spent on fuel etc..
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

        //Simple function to convert the duration of the appointment into hours, minutes and seconds
        //Makes it easier to display the time.
        private double[] ConvertSecondsToTime(double seconds)
        {
            double[] hoursMinSeconds = { 0, 0, 0 };
            TimeSpan t = TimeSpan.FromSeconds(seconds);
            hoursMinSeconds[0] = t.Hours;
            hoursMinSeconds[1] = t.Minutes;
            hoursMinSeconds[2] = t.Seconds;
            return hoursMinSeconds;
        }

        //Adds element to appointment (can be both equipment and employee). 
        //Also adds this appointment to either employee or equipment object so all we have to do is call this function 
        //Also adds this elements id to the id string so we can transfer it to the database to keep track of all elements in this appointment
        public void AddElementToAppointment(dynamic element)
        {
            if (element is Employee)
            {
                assignedEmployees.Add(element);
                Employee employee = (Employee)element;
                employeesIdString.Concat(employee.id.ToString());
                element.AddAppointment(this);
            }
            else if (element is Equipment)
            {
                assignedEquipments.Add(element);
                Equipment equipment = (Equipment)element;
                equipmentsIdString.Concat(equipment.id.ToString());
                element.AddAppointment(this);
            }
        }

        //Remove element from appointment (can be both equipment and employee)
        //Also removes the appointment from the element, and removes the elementID in the employeesIdString and equipmentsIdString
        public void RemoveElementFromAppointment(dynamic element)
        {
            if (element is Employee)
            {
                foreach (Employee employee in assignedEmployees)
                {
                    if (employee.id == element.id)
                    {
                        assignedEmployees.Remove(element);
                        element.RemoveAppointment(this);
                        employeesIdString.Replace(((Employee)element).id.ToString(), "");
                    }
                }
            }
            else if (element is Equipment)
            {
                foreach (Equipment equipment in assignedEquipments)
                {
                    if (equipment.id == element.id)
                    {
                        assignedEquipments.Remove(element);
                        element.RemoveAppointment(this);
                        equipmentsIdString.Replace(((Equipment)element).id.ToString(), "");
                    }
                }
            }
        }
    }
}
