using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace HasserisWeb
{    
    public class Furniture
    {
        public int ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double CubicSize { get; set; }
        [Required]
        public string Type { get; set; }
        [Required]
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
