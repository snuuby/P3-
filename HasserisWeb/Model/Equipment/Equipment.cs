using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{
    //Abstract class used for equipment. Derived class is either vehicles or work-tools.
    public abstract class Equipment
    {
        public int ID { get; set; }
        [Required]
        public bool IsAvailable { get; set; } = true;
        [Required]
        public string Name { get; set; }
        public string PhotoPath { get; set; }

        protected Equipment()
        {

        }
        public Equipment(string name)
        {
            this.Name = name;
        }
    }
}
