using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Calender
    {
        private List<Appointment> appointments { get; }
        //What is this supposed to represent?
        private DateTime currentDate { get; }

        public Calender()
        {

        }
        public void AddAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }
        public void RemoveAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }
    }
}
