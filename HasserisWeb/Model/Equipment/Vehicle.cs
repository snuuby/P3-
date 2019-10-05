using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Vehicle : Equipment
    {
        public string model { get; set; }
        public string regNum { get; set; }
        public Vehicle(string name, string type, string model, string regNum) : base(name, type)
        {
            this.model = model;
            this.regNum = regNum;
        }

    }
}
