using System;
using System.Collections.Generic;
using System.Linq;

namespace HasserisWeb
{
    //Consider if Employee should have subclasses such as Admin. If not, we should add a property to the class that 
    //specifies what type of employee it is. this property should be set in the constructor
    public class Employee
    {
        public string firstName { get; private set; }
        public string lastName { get; private set; }
        public bool isAvailable { get; private set; }
        public string appointmentIdString { get; set; }
        public List<Appointment> comingAppointments { get; private set; }
        
        public ContactInfo contactInfo { get; set; }
        public double wage { get; private set; }
        public int id { get; set; }
        public Address address { get; set; }

        public Employee(string fName, string lName, double pWage, ContactInfo contactInfo, Address address)
        {
            this.firstName = fName;
            this.lastName = lName;
            this.wage = pWage;
            this.contactInfo = contactInfo;
            this.address = address;
            this.appointmentIdString = "";
        }
        //Is called from a given appointment object
        public void AddAppointment(Appointment appointment)
        {
            comingAppointments.Add(appointment);
            appointmentIdString.Concat(appointment.id.ToString());
        }
        //Is called from a given appointment object
        public void RemoveAppointment(Appointment appointment)
        {
            comingAppointments.Remove(appointment);
            appointmentIdString.Replace(appointment.id.ToString(), "");
        }
        //I added a function in appointments to assignEmployee's so this function is useless
        /*
        public void AssignEmployee()
        {
            throw new NotImplementedException();
        }
        //Same as the text above
        public void UnassignEmployee()
        {
            this.currentAppoint = null;
            this.isAvailable = true;
        }
        */

    }
}