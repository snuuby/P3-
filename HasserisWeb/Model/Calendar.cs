using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Calendar
    {
        public List<Appointment> appointments { get; set; } = new List<Appointment>();
        //What is this supposed to represent?
        public DateTime currentDate = DateTime.Today.Date;
        public TimeSpan currentTime = DateTime.Today.TimeOfDay;
        public DateTime selectedDate { get; set; }
        public string name { get; set; }

        public Calendar(string name)
        {
            this.name = name;
        }

        public void CheckToday()
        {
            if (appointments.Count < 1)
            {
                foreach (Appointment appoint in appointments)
                {
                    if (DateTime.Today == currentDate)
                    {
                        Console.WriteLine($"Appointment Found! Date: {appoint.dates[0]}");
                        //Show only appointments that are due today
                        //throw new NotImplementedException("To be implemented");

                    }
                }
            }
            //Implement case for no appointments here.
        }
        public void AddAppointment(Appointment appointment)
        {
            //DateTime appointdate = DateTime;
            appointment.dates[0] = currentDate;
            appointments.Add(appointment);
            
        }
        public void RemoveAppointment(Appointment appointment)
        {
            appointments.Add(appointment);
        }
    }
}
