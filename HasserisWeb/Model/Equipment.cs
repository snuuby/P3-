using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public abstract class Equipment
    {
        public int ID { get; set; }
        public bool isAvailable { get; set; }
        public List<Appointment> upComingAppointments { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public abstract void AssignEquipment();
        public abstract void UnassignEquipment();
        public Equipment(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
    }
    public class Vehicle : Equipment
    {
        public string model { get; set; }
        public string regNum { get; set; }
        public Vehicle(string name, string type, string model, string regNum) : base(name, type)
        {
            this.model = model;
            this.regNum = regNum;
        }
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

        public Gear(string name, string type) : base(name, type)
        {
        }
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