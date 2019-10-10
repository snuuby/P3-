using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Calendar
    {
        public List<Task> tasks { get; set; } = new List<Task>();
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
            if (tasks.Count < 1)
            {
                foreach (Task task in tasks)
                {
                    if (DateTime.Today == currentDate)
                    {
                        Console.WriteLine($"Appointment Found! Date: {task.dates[0]}");
                        //Show only appointments that are due today
                        //throw new NotImplementedException("To be implemented");

                    }
                }
            }
            //Implement case for no appointments here.
        }
        public void AddTask(Task task)
        {
            //DateTime appointdate = DateTime;
            task.dates[0] = currentDate;
            tasks.Add(task);
            
        }
        public void RemoveTask(Task task)
        {
            tasks.Add(task);
        }
    }
}
