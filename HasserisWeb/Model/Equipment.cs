using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public abstract class Equipment
    {
        public string EquipmentID { get; set; }
        public bool IsAvailable { get; set; }
        public Appointment[] Appointments { get; set; }
        public string Name { get; set; }
        public abstract void AssignEquipment();
        public abstract void UnassignEquipment();
    }
    public class Vehicle : Equipment
    {
        public string Model { get; set; }
        public int RegNum { get; set; }

        public override void AssignEquipment()
        {
            throw new NotImplementedException();
        }

        public override void UnassignEquipment()
        {
            throw new NotImplementedException();

        }
    }
    public class Gear : Equipment
    {
        public string Type { get; set; }
        public override void AssignEquipment()
        {
            throw new NotImplementedException();
        }

        public override void UnassignEquipment()
        {
            throw new NotImplementedException();
        }
    }

}