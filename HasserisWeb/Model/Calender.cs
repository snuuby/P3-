using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb.Model
{
    public class Calender
    {
        private Appointment CalenderAppointments { get; }
        private DateTime CurrentDate { get; }

        public Calender(Appointment calenderAppointments, DateTime currentDate)
        {
            CalenderAppointments = calenderAppointments;
            CurrentDate = currentDate;
        }
    }
}
