using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    public class Vehicle : Equipment
    {
        public string Model { get; set; }
        public string RegNum { get; set; }
        public Vehicle(string name, string model, string regNum) : base(name)
        {
            this.Model = model;
            this.RegNum = regNum;
        }
        public Vehicle()
        {
        }

    }
}
