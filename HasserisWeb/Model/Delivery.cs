using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb.Model
{
    public class Delivery : Appointment
    {
        private string Material { get; }
        private int Quantity { get; }

        public Delivery(int appointmentID, string appointmentName, bool appointmentType, int appointmentDuration, Employee assignedEmployees, Customer assignedCustomer, Address appointmentDestination, double appointmentCost, Equipment assignedEquipment, DateTime appointmentDate, string appointmentNote, int workPhoneNumber, string material, int quantity)
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
            Material = material;
            Quantity = quantity;
        }

    }
}
