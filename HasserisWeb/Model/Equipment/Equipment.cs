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
        public string name { get; set; }
        public string type { get; set; }

        public Equipment(string name, string type)
        {
            this.name = name;
            this.type = type;
        }
    }
}