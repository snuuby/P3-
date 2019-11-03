using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Vehicle : Equipment
    {
        public string Model { get; set; }
        public string RegNum { get; set; }
        public Vehicle(string name, string type, string model, string regNum) : base(name, type)
        {
            this.Model = model;
            this.RegNum = regNum;
        }

    }
}
