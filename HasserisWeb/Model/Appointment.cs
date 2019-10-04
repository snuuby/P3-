using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace HasserisWeb
{
    public abstract class Appointment
    {
        private int AppointmentID { get; }
        private string AppointmentName { get; }
        private bool AppointmentType { get; }
        private int AppointmentDuration { get; }
        private Employee AssignedEmployees { get; }
        private Customer AssignedCustomer { get; }
        private Address AppointmentDestination { get; }
        private double AppointmentCost { get; }
        private Equipment AssignedEquipment { get; }
        private DateTime AppointmentDate { get; }
        private string AppointmentNote { get; }
        private int WorkPhoneNumber { get; }

        public Appointment(int appointmentID, string appointmentName, bool appointmentType, int appointmentDuration, Employee assignedEmployees, Customer assignedCustomer, Address appointmentDestination, double appointmentCost, Equipment assignedEquipment, DateTime appointmentDate, string appointmentNote, int workPhoneNumber)
        {
            AppointmentID = appointmentID;
            AppointmentName = appointmentName;
            AppointmentType = appointmentType;
            AppointmentDuration = appointmentDuration;
            AssignedEmployees = assignedEmployees;
            AssignedCustomer = assignedCustomer;
            AppointmentDestination = appointmentDestination;
            AppointmentCost = appointmentCost;
            AssignedEquipment = assignedEquipment;
            AppointmentDate = appointmentDate;
            AppointmentNote = appointmentNote;
            WorkPhoneNumber = workPhoneNumber;
        }
    }
}
