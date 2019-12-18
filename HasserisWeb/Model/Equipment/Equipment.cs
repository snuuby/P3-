using System.Collections.Generic;

namespace HasserisWeb
{
    //Abstract class used for equipment. Derived class is either vehicles or work-tools.
    public abstract class Equipment
    {
        public int ID { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Name { get; set; }
        public string PhotoPath { get; set; }
        public ICollection<TaskAssignedEquipment> Tasks { get; set; } = new List<TaskAssignedEquipment>();

        protected Equipment()
        {

        }
        public Equipment(string name)
        {
            this.Name = name;
        }
    }
}
