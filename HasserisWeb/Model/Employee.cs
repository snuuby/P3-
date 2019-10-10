using System;
using System.Collections.Generic;
using System.Linq;

namespace HasserisWeb
{
    //Consider if Employee should have subclasses such as Admin. If not, we should add a property to the class that 
    //specifies what type of employee it is. this property should be set in the constructor
    public class Employee
    {
        public string firstName { get;  set; }
        public string lastName { get;  set; }
        public bool isAvailable { get; private set; }
        //Bool given to admin-type employees. To require this with certain methods
        public bool isAdmin { get; private set; }
        public string taskIdString { get; set; }
        public List<Task> comingTasks { get; private set; } = new List<Task>();
        
        public ContactInfo contactInfo { get; set; }
        public double wage { get; private set; }
        public int id { get; set; }
        public Address address { get; set; }
        public string type { get; set; }

        // Test constructor Cholle
        public Employee()
        {
            
        }
        
        public Employee(string fName, string lName, string type, double pWage, ContactInfo contactInfo, Address address)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.wage = pWage;
            this.isAdmin = false;
            this.contactInfo = contactInfo;
            this.address = address;
            this.taskIdString = "";
            this.type = type;

        }
        //Is called from a given appointment object
        public void AddTask(Task task)
        {
            comingTasks.Add(task);
            taskIdString += task.id.ToString() + "/";
        }
        //Is called from a given appointment object
        public void RemoveAppointment(Task appointment)
        {
            comingTasks.Remove(appointment);
            taskIdString = taskIdString.Replace(appointment.id.ToString() + "/", "");
        }

    }
}