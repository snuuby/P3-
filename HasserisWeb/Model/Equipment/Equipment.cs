using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public abstract class Equipment
    {
        public int id { get; set; }
        public bool isAvailable { get; set; }
        public List<Appointment> comingAppointments { get; set; } = new List<Appointment>();
        public string appointmentIdString { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        //these arent needed as the equipment is assigned or unassigned from the appointment class
        /*
        public abstract void AssignEquipment();
        public abstract void UnassignEquipment(); */
        public Equipment(string name, string type)
        {
            this.name = name;
            this.type = type;
            this.appointmentIdString = "";
        }
        //is called from the appointment object
        public void AddAppointment(Appointment appointment)
        {
            comingAppointments.Add(appointment);
            appointmentIdString += appointment.id.ToString() + "/";
            HasserisDbContext.ModifySpecificElementInDatabase<Equipment>(this);
        }
        //is called from the appointment object
        public void RemoveAppointment(Appointment appointment)
        {
            comingAppointments.Remove(appointment);
            appointmentIdString.Replace(appointment.id.ToString() + "/", "");
            HasserisDbContext.ModifySpecificElementInDatabase<Equipment>(this);
        }
    }
}