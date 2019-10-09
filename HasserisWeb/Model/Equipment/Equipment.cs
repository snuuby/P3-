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
        public List<Task> comingTasks{ get; set; } = new List<Task>();
        public string taskIdString { get; set; }
        public string name { get; set; }
        public string type { get; set; }

        public Equipment(string name, string type)
        {
            this.name = name;
            this.type = type;
            this.taskIdString = "";
        }
        //is called from the appointment object
        public void AddTask(Task task)
        {
            comingTasks.Add(task);
            taskIdString += task.id.ToString() + "/";
        }
        //is called from the appointment object
        public void RemoveAppointment(Task task)
        {
            comingTasks.Remove(task);
            taskIdString.Replace(task.id.ToString() + "/", "");
        }
    }
}