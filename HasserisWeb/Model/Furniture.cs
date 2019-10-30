using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{    
    public class Furniture
    {
        public int id { get; set; }
        public string name { get; set; }
        public double cubicSize { get; set; }
        public string type { get; set; }
        public double weight { get; set; }

        public Furniture( string name, double cubicSize, string type, double weight)
        {
            this.name = name;
            this.cubicSize = cubicSize;
            this.type = type;
            this.weight = weight;
        }
    }
}
