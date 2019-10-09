using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HasserisWeb
{
    public abstract class Task
    {
        public int id { get; set; }
        public string name { get; }
        public string type { get; }
        public string employeesIdString { get; set; }
        //I made this to a list instead, because there might be more employees per appointment
        //The way we can save those in the database, is by having a variable called numberOfEmployees, which represents
        //the lenght of the string in the database, that writes the employeeID's. If a string in the database has 5 ID,s next to each other
        // we read them one by one and retrieve the specified employee off of that.
        //Also, i removed employees from the constructor because you should be able to add employees dynamically
        public List<Employee> assignedEmployees { get; set; } = new List<Employee>();
        public Customer assignedCustomer { get; } 
        public Address destination { get; }
        public double income { get; }
        public double expenses { get; set; }
        public double balance { get; set; }
        //The same idea here as for the list of employees
        public string equipmentsIdString { get; set; }
        //You should be able to add equipment dynamically so i removed it from the constructor
        public List<Equipment> assignedEquipment { get; set; }
        public List<DateTime> dates { get; internal set; }
        //Properties for calculating the total duration of an appointment, appointmentDuration.
        //Only hours:min:sec
        private DateTime startTime { get; set; }
        private DateTime endTime { get; set; }
        private List<DateTime> pauseTimes {get; set;}
        public TimeSpan taskDuration { get; set; }

        //What is a note?
        private string note { get; }
        public string workPhoneNumber { get; }

        public Task(string name, string type, Customer assignedCustomer, Address destination, 
                            double income, List<DateTime> Ldates, string note, string workPhoneNumber)
        {
            this.name = name;
            this.type = type;
            //I propose we make duration the total amount of seconds the appointment took. Then in this class we can make those seconds
            //Into hours, minutes and seconds. The database will only count the duration variable, so to not save 3 columns in the database
            //this.duration = duration;
            //this.hoursMinSeconds = ConvertSecondsToTime(duration);
            this.assignedCustomer = assignedCustomer;
            this.destination = destination;
            this.income = income;
            this.note = note;
            this.dates = Ldates;
            this.workPhoneNumber = workPhoneNumber;
            this.balance = this.income - this.expenses;
            this.employeesIdString = "";
            this.equipmentsIdString = "";
            assignedEmployees = new List<Employee> { };
            assignedEquipment = new List<Equipment> { };
            pauseTimes = new List<DateTime>();
        }

        public void BeginTasks()
        {
            if (assignedEmployees.Count < 1)
                //throw new SystemException("No employees assigned.");
            this.startTime = DateTime.Now;
        }
        public void ResumeTasks() 
        {
            startTime = DateTime.Now;
        }
        public void PauseTasks() 
        {
            DateTime temp = DateTime.Now;
            if (pauseTimes.Count == 0) 
            {
                pauseTimes.Add(startTime);
            }
            pauseTimes.Add(temp);
            if (pauseTimes.Count == 2) 
            {
                taskDuration = temp - startTime;
            }
            if (pauseTimes.Count > 2) {
                taskDuration += temp - startTime;
            }

        }

        public void EndTasks()
        {
            this.endTime = DateTime.Now;
            // Convert time into an array; [0] = Hours, [1] = Minutes, [2] = Seconds.

            TimeSpan t = endTime - startTime;
            if (pauseTimes.Count >= 2) 
            {
                taskDuration += t;
            }
            else 
            {
                taskDuration = t;
            }

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
                totalCost += employeeSecondWage * Convert.ToInt32(Math.Round(taskDuration.TotalSeconds));
            }
            return totalCost;
        }

        //Simple function to convert the duration of the appointment into hours, minutes and seconds
        //Makes it easier to display the time.

        //Adds element to appointment (can be both equipment and employee). 
        //Also adds this appointment to either employee or equipment object so all we have to do is call this function 
        //Also adds this elements id to the id string so we can transfer it to the database to keep track of all elements in this appointment
        public void AddElementToTask(dynamic element)
        {
            if (element is Employee)
            {
                assignedEmployees.Add((Employee)element);
                Employee employee = (Employee)element;
                employeesIdString += employee.id.ToString() + "/";
                element.AddTask(this);
            }
            else if (element is Equipment)
            {
                assignedEquipment.Add((Equipment)element);
                Equipment equipment = (Equipment)element;
                equipmentsIdString += equipment.id.ToString() + "/";
                element.AddTask(this);
            }

        }

        //Remove element from appointment (can be both equipment and employee)
        //Also removes the appointment from the element, and removes the elementID in the employeesIdString and equipmentsIdString
        public void RemoveElementFromTask(dynamic element)
        {
            if (element is Employee)
            {
                foreach (Employee employee in assignedEmployees)
                {
                    if (employee.id == element.id)
                    {
                        assignedEmployees.Remove(element);
                        element.RemoveAppointment(this);
                        employeesIdString.Replace(((Employee)element).id.ToString() + "-", "");
                    }
                }
            }
            else if (element is Equipment)
            {
                foreach (Equipment equipment in assignedEquipment)
                {
                    if (equipment.id == element.id)
                    {
                        assignedEquipment.Remove(element);
                        element.RemoveAppointment(this);
                        equipmentsIdString.Replace(((Equipment)element).id.ToString() + "-", "");
                    }
                }
            }
        }
    }
}
