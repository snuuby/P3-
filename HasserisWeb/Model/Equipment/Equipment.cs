using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Abstract class used for equipment. Derived class is either vehicles or work-tools.
    public abstract class Equipment
    {
        public ICollection<TaskAssignedEquipment> Tasks { get; set; }
        public int ID { get; set; }
        public bool IsAvailable { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public Equipment()
        {

        }
        public Equipment(string name, string type)
        {
            this.Name = name;
            this.Type = type;
            this.IsAvailable = true;
        }
        public Equipment()
        {

        }
    }
}