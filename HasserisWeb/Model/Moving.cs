using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb.Model
{
    public class Moving : Appointment
    {
        private Address StartingAddress { get; }
        private int LentBoxes { get; }

        public Moving(int appointmentID, string appointmentName, bool appointmentType, int appointmentDuration, Employee assignedEmployees, Customer assignedCustomer, Address appointmentDestination, double appointmentCost, Equipment assignedEquipment, DateTime appointmentDate, string appointmentNote, int workPhoneNumber, Address startingAddress, int lentBoxes)
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
            StartingAddress = startingAddress;
            LentBoxes = lentBoxes;
        }
    }
}
