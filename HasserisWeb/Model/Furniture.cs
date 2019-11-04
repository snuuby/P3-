using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{    
    public class Furniture
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double CubicSize { get; set; }
        public string Type { get; set; }
        public double Weight { get; set; }

        public Furniture( string name, double cubicSize, string type, double weight)
        {
            this.Name = name;
            this.CubicSize = cubicSize;
            this.Type = type;
            this.Weight = weight;
        }
        public Furniture()
        {

        }
    }
}
